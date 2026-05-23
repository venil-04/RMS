import React, { useState } from 'react';
import {
  Table, TableBody, TableCell, TableContainer, TableHead, TableRow,
  Box, Typography, Avatar, IconButton, Chip, Paper, CircularProgress,
  TablePagination, Menu, MenuItem, ListItemIcon
} from '@mui/material';
import {
  MoreVert as MoreVertIcon,
  Edit as EditIcon,
  Delete as DeleteIcon
} from '@mui/icons-material';
import { type UserListItemResponse } from '../types/userTypes';

interface UserTableProps {
  users: UserListItemResponse[];
  loading: boolean;
  pageNumber: number;
  pageSize: number;
  totalRecords: number;
  onPageChange: (newPage: number) => void;
  onPageSizeChange: (newSize: number) => void;
  onEdit: (user: UserListItemResponse) => void;
  onDelete: (user: UserListItemResponse) => void;
}

export const UserTable = ({
  users, loading, pageNumber, pageSize, totalRecords, onPageChange, onPageSizeChange,
  onEdit, onDelete
}: UserTableProps) => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selectedUser, setSelectedUser] = useState<UserListItemResponse | null>(null);
  const openMenu = Boolean(anchorEl);

  const handleMenuClick = (event: React.MouseEvent<HTMLElement>, user: UserListItemResponse) => {
    setAnchorEl(event.currentTarget);
    setSelectedUser(user);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
    setSelectedUser(null);
  };

  const handleAction = (action: 'edit' | 'delete') => {
    if (selectedUser) {
      if (action === 'edit') onEdit(selectedUser);
      if (action === 'delete') onDelete(selectedUser);
    }
    handleMenuClose();
  };

  const getAvatarColor = (role: string) => {
    switch (role) {
      case 'Admin': return { bg: '#dce1fe', color: '#151b2f' };
      case 'Manager': return { bg: '#ffdcbe', color: '#2c1600' };
      case 'Chef': return { bg: '#dce1fe', color: '#151b2f' };
      case 'Waiter': return { bg: '#e3e2e2', color: '#554434' };
      case 'Cashier': return { bg: '#ffdcbe', color: '#2c1600' };
      default: return { bg: '#e3e2e2', color: '#554434' };
    }
  };

  return (
    <TableContainer component={Paper} elevation={0} sx={{ borderRadius: 3, border: '1px solid #efeded' }}>
      <Table size="small" sx={{ minWidth: 650, '& .MuiTableCell-root': { py: 1.5 } }} aria-label="users table">
        <TableHead sx={{ bgcolor: '#f5f3f3' }}>
          <TableRow>
            <TableCell sx={{ fontWeight: 600, color: '#554434' }}>User</TableCell>
            <TableCell sx={{ fontWeight: 600, color: '#554434' }}>Mobile Number</TableCell>
            <TableCell sx={{ fontWeight: 600, color: '#554434' }}>Role</TableCell>
            <TableCell sx={{ fontWeight: 600, color: '#554434' }}>Status</TableCell>
            <TableCell sx={{ fontWeight: 600, color: '#554434' }}>Created Date</TableCell>
            <TableCell align="right" sx={{ fontWeight: 600, color: '#554434' }}>Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {loading ? (
            <TableRow>
              <TableCell colSpan={6} align="center" sx={{ py: 6 }}>
                <CircularProgress size={32} sx={{ color: '#ff9800' }} />
              </TableCell>
            </TableRow>
          ) : users.length === 0 ? (
            <TableRow>
              <TableCell colSpan={6} align="center" sx={{ py: 6, color: '#554434' }}>
                No users found.
              </TableCell>
            </TableRow>
          ) : (
            users.map((user) => {
              const avatarStyle = getAvatarColor(user.roleName);
              return (
                <TableRow
                  key={user.userId}
                  sx={{ '&:last-child td, &:last-child th': { border: 0 }, '&:hover': { bgcolor: '#f5f3f3' } }}
                >
                  <TableCell component="th" scope="row">
                    <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
                      <Avatar sx={{ bgcolor: avatarStyle.bg, color: avatarStyle.color, width: 32, height: 32, fontWeight: 600 }}>
                        {user.firstName[0]}
                      </Avatar>
                      <Box>
                        <Typography variant="body2" sx={{ fontWeight: 600, color: '#1b1c1c' }}>
                          {user.fullName}
                        </Typography>
                        <Typography variant="caption" sx={{ color: '#554434' }}>
                          {user.email}
                        </Typography>
                      </Box>
                    </Box>
                  </TableCell>
                  <TableCell sx={{ color: '#1b1c1c' }}>{user.mobileNumber || '-'}</TableCell>
                  <TableCell sx={{ color: '#1b1c1c' }}>{user.roleName}</TableCell>
                  <TableCell>
                    <Chip
                      label={user.isActive ? 'Active' : 'Inactive'}
                      size="small"
                      sx={{
                        bgcolor: user.isActive ? '#63c664' : '#e3e2e2',
                        color: user.isActive ? '#005012' : '#554434',
                        fontWeight: 500,
                        height: 24,
                      }}
                    />
                  </TableCell>
                  <TableCell sx={{ color: '#554434' }}>
                    {new Date(user.createdAt).toLocaleDateString()}
                  </TableCell>
                  <TableCell align="right">
                    <IconButton size="small" sx={{ color: '#554434' }} onClick={(e) => handleMenuClick(e, user)}>
                      <MoreVertIcon fontSize="small" />
                    </IconButton>
                  </TableCell>
                </TableRow>
              );
            })
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
              overflow: 'visible',
              filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.1))',
              mt: 1.5,
              borderRadius: 2,
              border: '1px solid #efeded',
              '& .MuiMenuItem-root': {
                px: 2,
                py: 1,
                fontSize: 14,
                color: '#1b1c1c',
                fontWeight: 500
              }
            }
          }
        }}
        transformOrigin={{ horizontal: 'right', vertical: 'top' }}
        anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
      >
        <MenuItem onClick={() => handleAction('edit')}>
          <ListItemIcon sx={{ minWidth: 28 }}>
            <EditIcon fontSize="small" sx={{ color: '#554434' }} />
          </ListItemIcon>
          Edit User
        </MenuItem>

        <MenuItem onClick={() => handleAction('delete')}>
          <ListItemIcon sx={{ minWidth: 28 }}>
            <DeleteIcon fontSize="small" sx={{ color: '#e53935' }} />
          </ListItemIcon>
          <Typography variant="inherit" sx={{ color: '#e53935' }}>
            Delete User
          </Typography>
        </MenuItem>
      </Menu>

      <TablePagination
        component="div"
        count={totalRecords}
        page={pageNumber - 1}
        onPageChange={(e, newPage) => onPageChange(newPage + 1)}
        rowsPerPage={pageSize}
        onRowsPerPageChange={(e) => onPageSizeChange(parseInt(e.target.value, 10))}
        rowsPerPageOptions={[5, 10, 25, 50]}
        sx={{ borderTop: '1px solid #efeded', bgcolor: '#f5f3f3' }}
      />
    </TableContainer>
  );
};
