import { useState, useEffect, useCallback } from 'react';
import { Box, Typography, Button, Snackbar, Alert } from '@mui/material';
import { Add as AddIcon } from '@mui/icons-material';
import { UserFilters } from '../components/UserFilters';
import { UserTable } from '../components/UserTable';
import { AddUserModal } from '../components/AddUserModal';
import { ConfirmationModal } from '../../../components/common/ConfirmationModal';
import { getUsers, deleteUser } from '../services/userService';
import { getRoles } from '../../../services/lookupService';
import { type UserListItemResponse, type GetUsersRequest, type RoleOption } from '../types/userTypes';

const UserManagement = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedUserForEdit, setSelectedUserForEdit] = useState<UserListItemResponse | null>(null);

  const [users, setUsers] = useState<UserListItemResponse[]>([]);
  const [roles, setRoles] = useState<RoleOption[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [successMsg, setSuccessMsg] = useState('');

  // Pagination
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalRecords, setTotalRecords] = useState(0);

  // Filters
  const [search, setSearch] = useState('');
  const [roleId, setRoleId] = useState<number | undefined>(undefined);
  const [isActive, setIsActive] = useState<boolean | undefined>(undefined);

  // Confirmation Modal State
  const [confirmModalOpen, setConfirmModalOpen] = useState(false);
  const [targetUser, setTargetUser] = useState<UserListItemResponse | null>(null);
  const [actionLoading, setActionLoading] = useState(false);

  const fetchRolesData = useCallback(async () => {
    try {
      const data = await getRoles();
      setRoles(data);
    } catch (err: any) {
      console.error('Failed to fetch roles:', err);
    }
  }, []);

  const fetchUsersData = useCallback(async () => {
    setLoading(true);
    setError('');
    try {
      const request: GetUsersRequest = {
        search: search || undefined,
        roleId,
        isActive,
        pageNumber,
        pageSize
      };
      const response = await getUsers(request);
      setUsers(response.items);
      setTotalRecords(response.totalRecords);
    } catch (err: any) {
      setError(err.message || 'Failed to fetch users');
      setUsers([]);
      setTotalRecords(0);
    } finally {
      setLoading(false);
    }
  }, [search, roleId, isActive, pageNumber, pageSize]);

  useEffect(() => {
    fetchRolesData();
  }, [fetchRolesData]);

  useEffect(() => {
    fetchUsersData();
  }, [fetchUsersData]);

  const handleFilterChange = (newSearch: string, newRoleId?: number, newIsActive?: boolean) => {
    setSearch(newSearch);
    setRoleId(newRoleId);
    setIsActive(newIsActive);
    setPageNumber(1);
  };

  const handleAddClick = () => {
    setSelectedUserForEdit(null);
    setIsModalOpen(true);
  };

  const handleEdit = (user: UserListItemResponse) => {
    setSelectedUserForEdit(user);
    setIsModalOpen(true);
  };

  const handleDelete = (user: UserListItemResponse) => {
    setTargetUser(user);
    setConfirmModalOpen(true);
  };

  const handleConfirmAction = async () => {
    if (!targetUser) return;

    setActionLoading(true);
    try {
      await deleteUser(targetUser.userId);
      setSuccessMsg('User deleted successfully');
      setConfirmModalOpen(false);
      fetchUsersData();
    } catch (err: any) {
      setError(err.message || 'Failed to delete user');
    } finally {
      setActionLoading(false);
    }
  };

  return (
    <Box sx={{ pb: 4 }}>
      {/* Page Header */}
      <Box sx={{ display: 'flex', flexDirection: { xs: 'column', sm: 'row' }, justifyContent: 'space-between', alignItems: { sm: 'center' }, gap: 2, mb: 2.5 }}>
        <Box>
          <Typography variant="h5" sx={{ fontWeight: 600, color: '#1b1c1c' }}>
            User Management
          </Typography>
          <Typography variant="body2" sx={{ color: '#554434', mt: 0.5 }}>
            Manage restaurant staff, roles, and account access
          </Typography>
        </Box>
        <Button
          variant="contained"
          startIcon={<AddIcon />}
          onClick={handleAddClick}
          sx={{
            bgcolor: '#ff9800',
            color: '#653900',
            '&:hover': { bgcolor: '#8b5000', color: '#ffffff' },
            textTransform: 'none',
            fontWeight: 600,
            boxShadow: 'none',
            alignSelf: { xs: 'flex-start', sm: 'auto' }
          }}
        >
          Add User
        </Button>
      </Box>

      {/* Filters */}
      <UserFilters
        onFilterChange={handleFilterChange}
        search={search}
        roleId={roleId}
        isActive={isActive}
        roles={roles}
      />

      {/* Table */}
      <UserTable
        users={users}
        loading={loading}
        pageNumber={pageNumber}
        pageSize={pageSize}
        totalRecords={totalRecords}
        onPageChange={(newPage) => setPageNumber(newPage)}
        onPageSizeChange={(newSize) => { setPageSize(newSize); setPageNumber(1); }}
        onEdit={handleEdit}
        onDelete={handleDelete}
      />

      {/* Modals */}
      <AddUserModal
        open={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onUserAdded={() => fetchUsersData()}
        user={selectedUserForEdit}
        roles={roles}
      />

      <ConfirmationModal
        open={confirmModalOpen}
        title="Delete User"
        message={`Are you sure you want to delete ${targetUser?.fullName}? This action cannot be undone.`}
        confirmText="Delete"
        onConfirm={handleConfirmAction}
        onCancel={() => setConfirmModalOpen(false)}
        loading={actionLoading}
      />

      <Snackbar
        open={!!error || !!successMsg}
        autoHideDuration={4000}
        onClose={() => { setError(''); setSuccessMsg(''); }}
        anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
      >
        <Alert severity={error ? "error" : "success"} sx={{ width: '100%' }}>
          {error || successMsg}
        </Alert>
      </Snackbar>
    </Box>
  );
};

export default UserManagement;
