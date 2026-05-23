import { useState, useEffect } from 'react';
import {
  Dialog, DialogTitle, DialogContent, DialogActions,
  Button, TextField, Select, MenuItem, Typography, Grid,
  IconButton, Snackbar, Alert, Switch, FormControlLabel
} from '@mui/material';
import { Close as CloseIcon } from '@mui/icons-material';
import { useSelector } from 'react-redux';
import type { RootState } from '../../../store';
import type { UserFormValues, UpsertUserRequest, RoleOption, UserListItemResponse } from '../types/userTypes';
import { upsertUser } from '../services/userService';

interface AddUserModalProps {
  open: boolean;
  onClose: () => void;
  onUserAdded?: () => void;
  user?: UserListItemResponse | null;
  roles: RoleOption[];
}

const defaultFormValues: UserFormValues = {
  roleId: '',
  firstName: '',
  lastName: '',
  email: '',
  mobileNumber: '',
  password: '',
  confirmPassword: '',
  isActive: true
};

export const AddUserModal = ({ open, onClose, onUserAdded, user, roles }: AddUserModalProps) => {
  const [formValues, setFormValues] = useState<UserFormValues>(defaultFormValues);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const [successMsg, setSuccessMsg] = useState('');

  const authUser = useSelector((state: RootState) => state.auth.user);
  const isEditMode = !!user;

  useEffect(() => {
    if (open) {
      if (user) {
        setFormValues({
          userId: user.userId,
          roleId: user.roleId,
          firstName: user.firstName,
          lastName: user.lastName || '',
          email: user.email,
          mobileNumber: user.mobileNumber || '',
          password: '',
          confirmPassword: '',
          isActive: user.isActive
        });
      } else {
        setFormValues(defaultFormValues);
      }
      setError('');
    }
  }, [open, user]);

  const handleClose = () => {
    setFormValues(defaultFormValues);
    setError('');
    onClose();
  };

  const handleChange = (field: keyof UserFormValues, value: any) => {
    setFormValues(prev => ({ ...prev, [field]: value }));
  };

  const validate = (): boolean => {
    if (!formValues.firstName.trim()) {
      setError('First Name is required'); return false;
    }
    if (!formValues.email.trim()) {
      setError('Email is required'); return false;
    }
    if (!/\S+@\S+\.\S+/.test(formValues.email)) {
      setError('Invalid email format'); return false;
    }
    if (formValues.roleId === '') {
      setError('Role is required'); return false;
    }

    // Validate passwords only in Add Mode
    if (!isEditMode) {
      if (!formValues.password) {
        setError('Password is required'); return false;
      }
      if (formValues.password.length < 6) {
        setError('Password must be at least 6 characters'); return false;
      }
      if (formValues.password !== formValues.confirmPassword) {
        setError('Passwords do not match'); return false;
      }
    }

    return true;
  };

  const handleSave = async () => {
    setError('');

    if (!validate()) {
      return;
    }

    setLoading(true);

    try {
      const restaurantId = authUser?.restaurantId || 1;

      const request: UpsertUserRequest = {
        userId: formValues.userId,
        restaurantId,
        roleId: formValues.roleId as number,
        firstName: formValues.firstName.trim(),
        lastName: formValues.lastName?.trim() || null,
        email: formValues.email.trim(),
        mobileNumber: formValues.mobileNumber?.trim() || null,
      };

      if (!isEditMode) {
        request.password = formValues.password;
      } else {
        (request as any).isActive = formValues.isActive;
      }

      await upsertUser(request);

      setSuccessMsg(`User ${isEditMode ? 'updated' : 'created'} successfully`);
      setTimeout(() => {
        setSuccessMsg('');
        if (onUserAdded) {
          onUserAdded();
        }
        handleClose();
      }, 1500);

    } catch (err: any) {
      setError(err.message || 'Failed to save user');
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
          paper: { sx: { borderRadius: 3, bgcolor: '#ffffff' } }
        }}
      >
        <DialogTitle sx={{ px: 3, py: 2, borderBottom: '1px solid #efeded', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
          <Typography variant="h6" sx={{ fontWeight: 600, color: '#1b1c1c' }}>
            {isEditMode ? 'Edit User' : 'Add New User'}
          </Typography>
          <IconButton size="small" onClick={handleClose} sx={{ color: '#554434' }} disabled={loading}>
            <CloseIcon />
          </IconButton>
        </DialogTitle>

        <DialogContent sx={{ px: 3, py: 2.5, mt: 1 }}>
          {error && (
            <Typography color="error" variant="body2" sx={{ mb: 2, fontWeight: 500 }}>
              {error}
            </Typography>
          )}

          <Grid container spacing={2}>
            {isEditMode && (
              <Grid size={12}>
                <FormControlLabel
                  control={
                    <Switch
                      checked={formValues.isActive}
                      onChange={(e) => handleChange('isActive', e.target.checked)}
                      color="primary"
                    />
                  }
                  label={
                    <Typography variant="body2" sx={{ fontWeight: 600, color: formValues.isActive ? '#43a047' : '#e53935' }}>
                      {formValues.isActive ? 'Active User' : 'Inactive User'}
                    </Typography>
                  }
                />
              </Grid>
            )}

            <Grid size={{ xs: 12, sm: 6 }}>
              <Typography variant="caption" sx={{ color: '#554434', fontWeight: 600, mb: 1, display: 'block' }}>
                First Name *
              </Typography>
              <TextField
                fullWidth
                size="small"
                value={formValues.firstName}
                onChange={e => handleChange('firstName', e.target.value)}
                sx={{ bgcolor: '#fbf9f9', borderRadius: 2 }}
              />
            </Grid>
            <Grid size={{ xs: 12, sm: 6 }}>
              <Typography variant="caption" sx={{ color: '#554434', fontWeight: 600, mb: 1, display: 'block' }}>
                Last Name
              </Typography>
              <TextField
                fullWidth
                size="small"
                value={formValues.lastName}
                onChange={e => handleChange('lastName', e.target.value)}
                sx={{ bgcolor: '#fbf9f9', borderRadius: 2 }}
              />
            </Grid>
            <Grid size={12}>
              <Typography variant="caption" sx={{ color: '#554434', fontWeight: 600, mb: 1, display: 'block' }}>
                Email Address *
              </Typography>
              <TextField
                fullWidth
                size="small"
                type="email"
                value={formValues.email}
                onChange={e => handleChange('email', e.target.value)}
                sx={{ bgcolor: '#fbf9f9', borderRadius: 2 }}
              />
            </Grid>
            <Grid size={12}>
              <Typography variant="caption" sx={{ color: '#554434', fontWeight: 600, mb: 1, display: 'block' }}>
                Mobile Number
              </Typography>
              <TextField
                fullWidth
                size="small"
                type="tel"
                value={formValues.mobileNumber}
                onChange={e => handleChange('mobileNumber', e.target.value)}
                sx={{ bgcolor: '#fbf9f9', borderRadius: 2 }}
              />
            </Grid>
            <Grid size={12}>
              <Typography variant="caption" sx={{ color: '#554434', fontWeight: 600, mb: 1, display: 'block' }}>
                Role *
              </Typography>
              <Select
                fullWidth
                size="small"
                value={formValues.roleId}
                onChange={e => handleChange('roleId', e.target.value)}
                displayEmpty
                sx={{ bgcolor: '#fbf9f9', borderRadius: 2 }}
              >
                <MenuItem value="" disabled>Select Role...</MenuItem>
                {roles.map(role => (
                  <MenuItem key={role.roleId} value={role.roleId}>{role.roleName}</MenuItem>
                ))}
              </Select>
            </Grid>

            {!isEditMode && (
              <>
                <Grid size={{ xs: 12, sm: 6 }}>
                  <Typography variant="caption" sx={{ color: '#554434', fontWeight: 600, mb: 1, display: 'block' }}>
                    Password *
                  </Typography>
                  <TextField
                    fullWidth
                    size="small"
                    type="password"
                    value={formValues.password}
                    onChange={(e) => handleChange('password', e.target.value)}
                    sx={{ bgcolor: '#fbf9f9', borderRadius: 2 }}
                  />
                </Grid>
                <Grid size={{ xs: 12, sm: 6 }}>
                  <Typography variant="caption" sx={{ color: '#554434', fontWeight: 600, mb: 1, display: 'block' }}>
                    Confirm Password *
                  </Typography>
                  <TextField
                    fullWidth
                    size="small"
                    type="password"
                    value={formValues.confirmPassword}
                    onChange={(e) => handleChange('confirmPassword', e.target.value)}
                    error={!!error && formValues.password !== formValues.confirmPassword}
                    sx={{ bgcolor: '#fbf9f9', borderRadius: 2 }}
                  />
                </Grid>
              </>
            )}
          </Grid>
        </DialogContent>

        <DialogActions sx={{ p: 2, px: 3, borderTop: '1px solid #efeded', bgcolor: '#f5f3f3' }}>
          <Button
            onClick={handleClose}
            disabled={loading}
            sx={{ color: '#1b1c1c', textTransform: 'none', fontWeight: 600 }}
          >
            Cancel
          </Button>
          <Button
            variant="contained"
            onClick={handleSave}
            disabled={loading}
            sx={{
              bgcolor: '#ff9800',
              color: '#653900',
              '&:hover': { bgcolor: '#8b5000', color: '#ffffff' },
              textTransform: 'none',
              fontWeight: 600,
              boxShadow: 'none'
            }}
          >
            {loading ? 'Saving...' : (isEditMode ? 'Update User' : 'Save User')}
          </Button>
        </DialogActions>
      </Dialog>

      <Snackbar
        open={!!successMsg}
        autoHideDuration={3000}
        onClose={() => setSuccessMsg('')}
        anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
      >
        <Alert severity="success" sx={{ width: '100%' }}>
          {successMsg}
        </Alert>
      </Snackbar>
    </>
  );
};
