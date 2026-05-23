import React, { useState, useEffect } from 'react';
import {
  Box, TextField, Select, MenuItem, InputAdornment, Button, Typography, Grid
} from '@mui/material';
import { Search as SearchIcon } from '@mui/icons-material';
import { type RoleOption } from '../types/userTypes';

interface UserFiltersProps {
  onFilterChange: (search: string, roleId?: number, isActive?: boolean) => void;
  search: string;
  roleId?: number;
  isActive?: boolean;
  roles: RoleOption[];
}

export const UserFilters = ({ onFilterChange, search, roleId, isActive, roles }: UserFiltersProps) => {
  const [localSearch, setLocalSearch] = useState(search);
  const [localRole, setLocalRole] = useState<string>(roleId ? roleId.toString() : 'All Roles');
  const [localStatus, setLocalStatus] = useState<string>(isActive === undefined ? 'All Status' : (isActive ? 'Active' : 'Inactive'));

  useEffect(() => {
    setLocalSearch(search);
    setLocalRole(roleId ? roleId.toString() : 'All Roles');
    setLocalStatus(isActive === undefined ? 'All Status' : (isActive ? 'Active' : 'Inactive'));
  }, [search, roleId, isActive]);

  const applyFilters = (s: string, r: string, st: string) => {
    const parsedRoleId = r === 'All Roles' ? undefined : parseInt(r, 10);
    const parsedIsActive = st === 'All Status' ? undefined : (st === 'Active' ? true : false);
    onFilterChange(s, parsedRoleId, parsedIsActive);
  };

  const handleRoleChange = (e: any) => {
    setLocalRole(e.target.value);
    applyFilters(localSearch, e.target.value, localStatus);
  };

  const handleStatusChange = (e: any) => {
    setLocalStatus(e.target.value);
    applyFilters(localSearch, localRole, e.target.value);
  };

  const handleSearchChange = (e: any) => {
    setLocalSearch(e.target.value);
  };

  const handleSearchKeyPress = (e: React.KeyboardEvent) => {
    if (e.key === 'Enter') {
      applyFilters(localSearch, localRole, localStatus);
    }
  };

  const handleClear = () => {
    setLocalSearch('');
    setLocalRole('All Roles');
    setLocalStatus('All Status');
    onFilterChange('', undefined, undefined);
  };

  return (
    <Box sx={{
      bgcolor: '#ffffff',
      borderRadius: 3,
      p: 2,
      mb: 2.5,
      border: '1px solid #efeded',
      boxShadow: 'none'
    }}>
      <Grid container spacing={2} sx={{ alignItems: 'flex-end' }}>
        <Grid size={{ xs: 12, md: 3 }}>
          <Typography variant="caption" sx={{ color: '#554434', fontWeight: 600, mb: 1, display: 'block' }}>
            Search Users
          </Typography>
          <TextField
            fullWidth
            placeholder="Name, email, mobile..."
            size="small"
            value={localSearch}
            onChange={handleSearchChange}
            onKeyPress={handleSearchKeyPress}
            onBlur={() => applyFilters(localSearch, localRole, localStatus)}
            slotProps={{
              input: {
                startAdornment: (
                  <InputAdornment position="start">
                    <SearchIcon fontSize="small" sx={{ color: '#554434' }} />
                  </InputAdornment>
                ),
                sx: { bgcolor: '#fbf9f9', borderRadius: 2 }
              }
            }}
          />
        </Grid>

        <Grid size={{ xs: 12, md: 3 }}>
          <Typography variant="caption" sx={{ color: '#554434', fontWeight: 600, mb: 1, display: 'block' }}>
            Role
          </Typography>
          <Select
            fullWidth
            size="small"
            value={localRole}
            onChange={handleRoleChange}
            sx={{ bgcolor: '#fbf9f9', borderRadius: 2 }}
          >
            <MenuItem value="All Roles">All Roles</MenuItem>
            {roles.map(role => (
              <MenuItem key={role.roleId} value={role.roleId.toString()}>{role.roleName}</MenuItem>
            ))}
          </Select>
        </Grid>

        <Grid size={{ xs: 12, md: 3 }}>
          <Typography variant="caption" sx={{ color: '#554434', fontWeight: 600, mb: 1, display: 'block' }}>
            Status
          </Typography>
          <Select
            fullWidth
            size="small"
            value={localStatus}
            onChange={handleStatusChange}
            sx={{ bgcolor: '#fbf9f9', borderRadius: 2 }}
          >
            <MenuItem value="All Status">All Status</MenuItem>
            <MenuItem value="Active">Active</MenuItem>
            <MenuItem value="Inactive">Inactive</MenuItem>
          </Select>
        </Grid>

        <Grid size={{ xs: 12, md: 3 }}>
          <Typography variant="caption" sx={{ mb: 1, display: 'block', visibility: 'hidden' }}>
            Placeholder
          </Typography>
          <Button
            variant="outlined"
            fullWidth
            onClick={handleClear}
            sx={{
              color: '#8b5000',
              borderColor: '#dbc2ad',
              borderRadius: 2,
              textTransform: 'none',
              fontWeight: 600,
              height: '40px'
            }}
          >
            Clear Filters
          </Button>
        </Grid>
      </Grid>
    </Box>
  );
};
