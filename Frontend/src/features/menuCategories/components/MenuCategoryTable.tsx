import React, { useState } from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
  IconButton,
  Chip,
  Paper,
  CircularProgress,
  TablePagination,
  Menu,
  MenuItem,
  ListItemIcon,
} from "@mui/material";
import {
  MoreVert as MoreVertIcon,
  Edit as EditIcon,
  Delete as DeleteIcon,
} from "@mui/icons-material";
import { type MenuCategory } from "../types/menuCategoryTypes";

interface MenuCategoryTableProps {
  categories: MenuCategory[];
  loading: boolean;
  pageNumber: number;
  pageSize: number;
  totalRecords: number;
  onPageChange: (newPage: number) => void;
  onPageSizeChange: (newSize: number) => void;
  onEdit: (category: MenuCategory) => void;
  onDelete: (category: MenuCategory) => void;
}

export const MenuCategoryTable = ({
  categories,
  loading,
  pageNumber,
  pageSize,
  totalRecords,
  onPageChange,
  onPageSizeChange,
  onEdit,
  onDelete,
}: MenuCategoryTableProps) => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selectedCategory, setSelectedCategory] = useState<MenuCategory | null>(
    null
  );
  const openMenu = Boolean(anchorEl);

  const handleMenuClick = (
    event: React.MouseEvent<HTMLElement>,
    category: MenuCategory
  ) => {
    setAnchorEl(event.currentTarget);
    setSelectedCategory(category);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
    setSelectedCategory(null);
  };

  const handleAction = (action: "edit" | "delete") => {
    if (selectedCategory) {
      if (action === "edit") onEdit(selectedCategory);
      if (action === "delete") onDelete(selectedCategory);
    }
    handleMenuClose();
  };

  return (
    <TableContainer
      component={Paper}
      elevation={0}
      sx={{ borderRadius: 3, border: "1px solid #efeded" }}
    >
      <Table
        size="small"
        sx={{ minWidth: 650, "& .MuiTableCell-root": { py: 1.5 } }}
        aria-label="menu categories table"
      >
        <TableHead sx={{ bgcolor: "#f5f3f3" }}>
          <TableRow>
            <TableCell sx={{ fontWeight: 600, color: "#554434" }}>
              Category Name
            </TableCell>
            <TableCell sx={{ fontWeight: 600, color: "#554434" }}>
              Description
            </TableCell>
            <TableCell
              align="center"
              sx={{ fontWeight: 600, color: "#554434" }}
            >
              Display Order
            </TableCell>
            <TableCell sx={{ fontWeight: 600, color: "#554434" }}>
              Status
            </TableCell>
            <TableCell
              align="right"
              sx={{ fontWeight: 600, color: "#554434" }}
            >
              Actions
            </TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {loading ? (
            <TableRow>
              <TableCell colSpan={5} align="center" sx={{ py: 6 }}>
                <CircularProgress size={32} sx={{ color: "#ff9800" }} />
              </TableCell>
            </TableRow>
          ) : categories.length === 0 ? (
            <TableRow>
              <TableCell colSpan={5} align="center" sx={{ py: 6, color: "#554434" }}>
                No categories found.
              </TableCell>
            </TableRow>
          ) : (
            categories.map((category) => (
              <TableRow
                key={category.categoryId}
                sx={{
                  "&:last-child td, &:last-child th": { border: 0 },
                  "&:hover": { bgcolor: "#f5f3f3" },
                }}
              >
                <TableCell
                  component="th"
                  scope="row"
                  sx={{ fontWeight: 600, color: "#1b1c1c" }}
                >
                  {category.categoryName}
                </TableCell>
                <TableCell sx={{ color: "#554434", maxW: 300 }}>
                  <Typography
                    variant="body2"
                    noWrap
                    title={category.description}
                    sx={{ maxWidth: 300, overflow: "hidden", textOverflow: "ellipsis" }}
                  >
                    {category.description || "-"}
                  </Typography>
                </TableCell>
                <TableCell align="center" sx={{ color: "#1b1c1c" }}>
                  {category.displayOrder}
                </TableCell>
                <TableCell>
                  <Chip
                    label={category.isActive ? "Active" : "Inactive"}
                    size="small"
                    sx={{
                      bgcolor: category.isActive ? "#63c664" : "#e3e2e2",
                      color: category.isActive ? "#005012" : "#554434",
                      fontWeight: 500,
                      height: 24,
                    }}
                  />
                </TableCell>
                <TableCell align="right">
                  <IconButton
                    size="small"
                    sx={{ color: "#554434" }}
                    onClick={(e) => handleMenuClick(e, category)}
                  >
                    <MoreVertIcon fontSize="small" />
                  </IconButton>
                </TableCell>
              </TableRow>
            ))
          )}
        </TableBody>
      </Table>

      <Menu
        anchorEl={anchorEl}
        open={openMenu}
        onClose={handleMenuClose}
        slotProps={{
          paper: {
            elevation: 0,
            sx: {
              overflow: "visible",
              filter: "drop-shadow(0px 2px 8px rgba(0,0,0,0.1))",
              mt: 1.5,
              borderRadius: 2,
              border: "1px solid #efeded",
              "& .MuiMenuItem-root": {
                px: 2,
                py: 1,
                fontSize: 14,
                color: "#1b1c1c",
                fontWeight: 500,
              },
            },
          },
        }}
        transformOrigin={{ horizontal: "right", vertical: "top" }}
        anchorOrigin={{ horizontal: "right", vertical: "bottom" }}
      >
        <MenuItem onClick={() => handleAction("edit")}>
          <ListItemIcon sx={{ minWidth: 28 }}>
            <EditIcon fontSize="small" sx={{ color: "#554434" }} />
          </ListItemIcon>
          Edit Category
        </MenuItem>

        <MenuItem onClick={() => handleAction("delete")}>
          <ListItemIcon sx={{ minWidth: 28 }}>
            <DeleteIcon fontSize="small" sx={{ color: "#e53935" }} />
          </ListItemIcon>
          <Typography variant="inherit" sx={{ color: "#e53935" }}>
            Delete Category
          </Typography>
        </MenuItem>
      </Menu>

      <TablePagination
        component="div"
        count={totalRecords}
        page={pageNumber - 1}
        onPageChange={(_, newPage) => onPageChange(newPage + 1)}
        rowsPerPage={pageSize}
        onRowsPerPageChange={(e) =>
          onPageSizeChange(parseInt(e.target.value, 10))
        }
        rowsPerPageOptions={[5, 10, 25, 50]}
        sx={{ borderTop: "1px solid #efeded", bgcolor: "#f5f3f3" }}
      />
    </TableContainer>
  );
};
