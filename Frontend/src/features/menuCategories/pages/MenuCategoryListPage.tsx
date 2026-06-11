import { useState, useEffect, useCallback } from "react";
import { Box, Typography, Button, Snackbar, Alert } from "@mui/material";
import { Add as AddIcon } from "@mui/icons-material";
import { MenuCategoryFilters } from "../components/MenuCategoryFilters";
import { MenuCategoryTable } from "../components/MenuCategoryTable";
import { AddMenuCategoryModal } from "../components/AddMenuCategoryModal";
import { ConfirmationModal } from "../../../components/common/ConfirmationModal";
import { menuCategoryService } from "../services/menuCategoryService";
import { type MenuCategory } from "../types/menuCategoryTypes";

export default function MenuCategoryListPage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedCategoryForEdit, setSelectedCategoryForEdit] =
    useState<MenuCategory | null>(null);

  const [menuCategories, setMenuCategories] = useState<MenuCategory[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [successMsg, setSuccessMsg] = useState("");

  // Pagination
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);

  // Filters
  const [search, setSearch] = useState("");
  const [isActive, setIsActive] = useState<boolean | undefined>(undefined);

  // Confirmation Modal State
  const [confirmModalOpen, setConfirmModalOpen] = useState(false);
  const [targetCategory, setTargetCategory] = useState<MenuCategory | null>(
    null
  );
  const [actionLoading, setActionLoading] = useState(false);

  const loadMenuCategories = useCallback(async () => {
    try {
      setLoading(true);
      setError("");
      const data = await menuCategoryService.getAll();
      // Sort categories by displayOrder ascending
      const sortedData = [...data].sort((a, b) => a.displayOrder - b.displayOrder);
      setMenuCategories(sortedData);
    } catch (err: any) {
      setError(err.message || "Failed to load menu categories");
      setMenuCategories([]);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    loadMenuCategories();
  }, [loadMenuCategories]);

  const handleFilterChange = (newSearch: string, newIsActive?: boolean) => {
    setSearch(newSearch);
    setIsActive(newIsActive);
    setPageNumber(1);
  };

  const handleAddClick = () => {
    setSelectedCategoryForEdit(null);
    setIsModalOpen(true);
  };

  const handleEdit = (category: MenuCategory) => {
    setSelectedCategoryForEdit(category);
    setIsModalOpen(true);
  };

  const handleDeleteClick = (category: MenuCategory) => {
    setTargetCategory(category);
    setConfirmModalOpen(true);
  };

  const handleConfirmDelete = async () => {
    if (!targetCategory) return;

    setActionLoading(true);
    try {
      await menuCategoryService.delete(targetCategory.categoryId);
      setSuccessMsg("Menu category deleted successfully");
      setConfirmModalOpen(false);
      setTargetCategory(null);
      loadMenuCategories();
    } catch (err: any) {
      setError(err.message || "Failed to delete category");
    } finally {
      setActionLoading(false);
    }
  };

  // Perform filtering in-memory
  const filteredCategories = menuCategories.filter((cat) => {
    const matchesSearch =
      search.trim() === "" ||
      cat.categoryName.toLowerCase().includes(search.toLowerCase()) ||
      (cat.description &&
        cat.description.toLowerCase().includes(search.toLowerCase()));

    const matchesStatus =
      isActive === undefined ? true : cat.isActive === isActive;

    return matchesSearch && matchesStatus;
  });

  // Perform pagination in-memory
  const paginatedCategories = filteredCategories.slice(
    (pageNumber - 1) * pageSize,
    pageNumber * pageSize
  );

  return (
    <Box sx={{ pb: 4 }}>
      {/* Page Header */}
      <Box
        sx={{
          display: "flex",
          flexDirection: { xs: "column", sm: "row" },
          justifyContent: "space-between",
          alignItems: { sm: "center" },
          gap: 2,
          mb: 2.5,
        }}
      >
        <Box>
          <Typography variant="h5" sx={{ fontWeight: 600, color: "#1b1c1c" }}>
            Category Management
          </Typography>
          <Typography variant="body2" sx={{ color: "#554434", mt: 0.5 }}>
            Organize and manage menu sections
          </Typography>
        </Box>
        <Button
          variant="contained"
          startIcon={<AddIcon />}
          onClick={handleAddClick}
          sx={{
            bgcolor: "#ff9800",
            color: "#653900",
            "&:hover": { bgcolor: "#8b5000", color: "#ffffff" },
            textTransform: "none",
            fontWeight: 600,
            boxShadow: "none",
            alignSelf: { xs: "flex-start", sm: "auto" },
          }}
        >
          Add Category
        </Button>
      </Box>

      {/* Filters */}
      <MenuCategoryFilters
        onFilterChange={handleFilterChange}
        search={search}
        isActive={isActive}
      />

      {/* Table */}
      <MenuCategoryTable
        categories={paginatedCategories}
        loading={loading}
        pageNumber={pageNumber}
        pageSize={pageSize}
        totalRecords={filteredCategories.length}
        onPageChange={(newPage) => setPageNumber(newPage)}
        onPageSizeChange={(newSize) => {
          setPageSize(newSize);
          setPageNumber(1);
        }}
        onEdit={handleEdit}
        onDelete={handleDeleteClick}
      />

      {/* Modals */}
      <AddMenuCategoryModal
        open={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onCategorySaved={() => loadMenuCategories()}
        category={selectedCategoryForEdit}
      />

      <ConfirmationModal
        open={confirmModalOpen}
        title="Delete Menu Category"
        message={`Are you sure you want to delete ${targetCategory?.categoryName}? This action cannot be undone.`}
        confirmText="Delete"
        onConfirm={handleConfirmDelete}
        onCancel={() => setConfirmModalOpen(false)}
        loading={actionLoading}
      />

      <Snackbar
        open={!!error || !!successMsg}
        autoHideDuration={4000}
        onClose={() => {
          setError("");
          setSuccessMsg("");
        }}
        anchorOrigin={{ vertical: "top", horizontal: "center" }}
      >
        <Alert severity={error ? "error" : "success"} sx={{ width: "100%" }}>
          {error || successMsg}
        </Alert>
      </Snackbar>
    </Box>
  );
}