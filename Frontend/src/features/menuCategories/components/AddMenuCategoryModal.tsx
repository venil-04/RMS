import { useState, useEffect } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  Typography,
  Grid,
  IconButton,
  Snackbar,
  Alert,
  Switch,
  FormControlLabel,
} from "@mui/material";
import { Close as CloseIcon } from "@mui/icons-material";
import { useSelector } from "react-redux";
import type { RootState } from "../../../store";
import {
  type MenuCategory,
  type MenuCategoryFormValues,
  type CreateMenuCategoryRequest,
  type UpdateMenuCategoryRequest,
} from "../types/menuCategoryTypes";
import { menuCategoryService } from "../services/menuCategoryService";

interface AddMenuCategoryModalProps {
  open: boolean;
  onClose: () => void;
  onCategorySaved?: () => void;
  category?: MenuCategory | null;
}

const defaultFormValues: MenuCategoryFormValues = {
  categoryName: "",
  description: "",
  displayOrder: 0,
  isActive: true,
};

export const AddMenuCategoryModal = ({
  open,
  onClose,
  onCategorySaved,
  category,
}: AddMenuCategoryModalProps) => {
  const [formValues, setFormValues] =
    useState<MenuCategoryFormValues>(defaultFormValues);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const [successMsg, setSuccessMsg] = useState("");

  const authUser = useSelector((state: RootState) => state.auth.user);
  const isEditMode = !!category;

  useEffect(() => {
    if (open) {
      if (category) {
        setFormValues({
          categoryId: category.categoryId,
          categoryName: category.categoryName,
          description: category.description || "",
          displayOrder: category.displayOrder,
          isActive: category.isActive,
        });
      } else {
        setFormValues(defaultFormValues);
      }
      setError("");
    }
  }, [open, category]);

  const handleClose = () => {
    setFormValues(defaultFormValues);
    setError("");
    onClose();
  };

  const handleChange = (field: keyof MenuCategoryFormValues, value: any) => {
    setFormValues((prev) => ({ ...prev, [field]: value }));
  };

  const validate = (): boolean => {
    if (!formValues.categoryName.trim()) {
      setError("Category Name is required");
      return false;
    }
    if (formValues.displayOrder < 0) {
      setError("Display Order must be a positive number");
      return false;
    }
    return true;
  };

  const handleSave = async () => {
    setError("");

    if (!validate()) {
      return;
    }

    setLoading(true);

    try {
      const restaurantId = authUser?.restaurantId || 1;

      if (isEditMode) {
        const request: UpdateMenuCategoryRequest = {
          categoryId: formValues.categoryId as number,
          categoryName: formValues.categoryName.trim(),
          description: formValues.description.trim() || undefined,
          displayOrder: Number(formValues.displayOrder),
          isActive: formValues.isActive,
        };
        await menuCategoryService.update(request);
      } else {
        const request: CreateMenuCategoryRequest = {
          restaurantId,
          categoryName: formValues.categoryName.trim(),
          description: formValues.description.trim() || undefined,
          displayOrder: Number(formValues.displayOrder),
          isActive: formValues.isActive,
        };
        await menuCategoryService.create(request);
      }

      setSuccessMsg(
        `Category ${isEditMode ? "updated" : "created"} successfully`
      );
      setTimeout(() => {
        setSuccessMsg("");
        if (onCategorySaved) {
          onCategorySaved();
        }
        handleClose();
      }, 1500);
    } catch (err: any) {
      setError(err.message || "Failed to save category");
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <Dialog
        open={open}
        onClose={handleClose}
        maxWidth="sm"
        fullWidth
        slotProps={{
          paper: { sx: { borderRadius: 3, bgcolor: "#ffffff" } },
        }}
      >
        <DialogTitle
          sx={{
            px: 3,
            py: 2,
            borderBottom: "1px solid #efeded",
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
          }}
        >
          <Typography variant="h6" sx={{ fontWeight: 600, color: "#1b1c1c" }}>
            {isEditMode ? "Edit Category" : "Add New Category"}
          </Typography>
          <IconButton
            size="small"
            onClick={handleClose}
            sx={{ color: "#554434" }}
            disabled={loading}
          >
            <CloseIcon />
          </IconButton>
        </DialogTitle>

        <DialogContent sx={{ px: 3, py: 2.5, mt: 1 }}>
          {error && (
            <Typography
              color="error"
              variant="body2"
              sx={{ mb: 2, fontWeight: 500 }}
            >
              {error}
            </Typography>
          )}

          <Grid container spacing={2}>
            <Grid size={12}>
              <FormControlLabel
                control={
                  <Switch
                    checked={formValues.isActive}
                    onChange={(e) => handleChange("isActive", e.target.checked)}
                    color="primary"
                  />
                }
                label={
                  <Typography
                    variant="body2"
                    sx={{
                      fontWeight: 600,
                      color: formValues.isActive ? "#43a047" : "#e53935",
                    }}
                  >
                    {formValues.isActive ? "Active Category" : "Inactive Category"}
                  </Typography>
                }
              />
            </Grid>

            <Grid size={12}>
              <Typography
                variant="caption"
                sx={{ color: "#554434", fontWeight: 600, mb: 1, display: "block" }}
              >
                Category Name *
              </Typography>
              <TextField
                fullWidth
                size="small"
                value={formValues.categoryName}
                onChange={(e) => handleChange("categoryName", e.target.value)}
                sx={{ bgcolor: "#fbf9f9", borderRadius: 2 }}
              />
            </Grid>

            <Grid size={12}>
              <Typography
                variant="caption"
                sx={{ color: "#554434", fontWeight: 600, mb: 1, display: "block" }}
              >
                Description
              </Typography>
              <TextField
                fullWidth
                size="small"
                multiline
                rows={3}
                value={formValues.description}
                onChange={(e) => handleChange("description", e.target.value)}
                sx={{ bgcolor: "#fbf9f9", borderRadius: 2 }}
              />
            </Grid>

            <Grid size={12}>
              <Typography
                variant="caption"
                sx={{ color: "#554434", fontWeight: 600, mb: 1, display: "block" }}
              >
                Display Order *
              </Typography>
              <TextField
                fullWidth
                size="small"
                type="number"
                value={formValues.displayOrder}
                onChange={(e) => handleChange("displayOrder", e.target.value)}
                sx={{ bgcolor: "#fbf9f9", borderRadius: 2 }}
              />
            </Grid>
          </Grid>
        </DialogContent>

        <DialogActions
          sx={{
            p: 2,
            px: 3,
            borderTop: "1px solid #efeded",
            bgcolor: "#f5f3f3",
          }}
        >
          <Button
            onClick={handleClose}
            disabled={loading}
            sx={{ color: "#1b1c1c", textTransform: "none", fontWeight: 600 }}
          >
            Cancel
          </Button>
          <Button
            variant="contained"
            onClick={handleSave}
            disabled={loading}
            sx={{
              bgcolor: "#ff9800",
              color: "#653900",
              "&:hover": { bgcolor: "#8b5000", color: "#ffffff" },
              textTransform: "none",
              fontWeight: 600,
              boxShadow: "none",
            }}
          >
            {loading
              ? "Saving..."
              : isEditMode
              ? "Update Category"
              : "Save Category"}
          </Button>
        </DialogActions>
      </Dialog>

      <Snackbar
        open={!!successMsg}
        autoHideDuration={3000}
        onClose={() => setSuccessMsg("")}
        anchorOrigin={{ vertical: "top", horizontal: "center" }}
      >
        <Alert severity="success" sx={{ width: "100%" }}>
          {successMsg}
        </Alert>
      </Snackbar>
    </>
  );
};
