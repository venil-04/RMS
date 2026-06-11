import React, { useState, useEffect } from "react";
import {
  Box,
  TextField,
  Select,
  MenuItem,
  InputAdornment,
  Button,
  Typography,
  Grid,
} from "@mui/material";
import { Search as SearchIcon } from "@mui/icons-material";

interface MenuCategoryFiltersProps {
  onFilterChange: (search: string, isActive?: boolean) => void;
  search: string;
  isActive?: boolean;
}

export const MenuCategoryFilters = ({
  onFilterChange,
  search,
  isActive,
}: MenuCategoryFiltersProps) => {
  const [localSearch, setLocalSearch] = useState(search);
  const [localStatus, setLocalStatus] = useState<string>(
    isActive === undefined ? "All Status" : isActive ? "Active" : "Inactive"
  );

  useEffect(() => {
    setLocalSearch(search);
    setLocalStatus(
      isActive === undefined ? "All Status" : isActive ? "Active" : "Inactive"
    );
  }, [search, isActive]);

  const applyFilters = (s: string, st: string) => {
    const parsedIsActive =
      st === "All Status" ? undefined : st === "Active" ? true : false;
    onFilterChange(s, parsedIsActive);
  };

  const handleStatusChange = (e: any) => {
    setLocalStatus(e.target.value);
    applyFilters(localSearch, e.target.value);
  };

  const handleSearchChange = (e: any) => {
    setLocalSearch(e.target.value);
  };

  const handleSearchKeyPress = (e: React.KeyboardEvent) => {
    if (e.key === "Enter") {
      applyFilters(localSearch, localStatus);
    }
  };

  const handleClear = () => {
    setLocalSearch("");
    setLocalStatus("All Status");
    onFilterChange("", undefined);
  };

  return (
    <Box
      sx={{
        bgcolor: "#ffffff",
        borderRadius: 3,
        p: 2,
        mb: 2.5,
        border: "1px solid #efeded",
        boxShadow: "none",
      }}
    >
      <Grid container spacing={2} sx={{ alignItems: "flex-end" }}>
        <Grid size={{ xs: 12, sm: 6, md: 5 }}>
          <Typography
            variant="caption"
            sx={{ color: "#554434", fontWeight: 600, mb: 1, display: "block" }}
          >
            Search Categories
          </Typography>
          <TextField
            fullWidth
            placeholder="Search by name..."
            size="small"
            value={localSearch}
            onChange={handleSearchChange}
            onKeyPress={handleSearchKeyPress}
            onBlur={() => applyFilters(localSearch, localStatus)}
            slotProps={{
              input: {
                startAdornment: (
                  <InputAdornment position="start">
                    <SearchIcon fontSize="small" sx={{ color: "#554434" }} />
                  </InputAdornment>
                ),
                sx: { bgcolor: "#fbf9f9", borderRadius: 2 },
              },
            }}
          />
        </Grid>

        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
          <Typography
            variant="caption"
            sx={{ color: "#554434", fontWeight: 600, mb: 1, display: "block" }}
          >
            Status
          </Typography>
          <Select
            fullWidth
            size="small"
            value={localStatus}
            onChange={handleStatusChange}
            sx={{ bgcolor: "#fbf9f9", borderRadius: 2 }}
          >
            <MenuItem value="All Status">All Statuses</MenuItem>
            <MenuItem value="Active">Active</MenuItem>
            <MenuItem value="Inactive">Inactive</MenuItem>
          </Select>
        </Grid>

        <Grid size={{ xs: 12, sm: 12, md: 3 }}>
          <Button
            variant="outlined"
            fullWidth
            onClick={handleClear}
            sx={{
              color: "#8b5000",
              borderColor: "#dbc2ad",
              borderRadius: 2,
              textTransform: "none",
              fontWeight: 600,
              height: "40px",
            }}
          >
            Clear Filters
          </Button>
        </Grid>
      </Grid>
    </Box>
  );
};
