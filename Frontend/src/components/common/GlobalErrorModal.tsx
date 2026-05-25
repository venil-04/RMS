import React, { useState, useEffect } from 'react';
import {
  Dialog, DialogTitle, DialogContent, DialogActions,
  Button, Typography, Box, List, ListItem, ListItemIcon, ListItemText
} from '@mui/material';
import { ErrorOutlined as ErrorIcon, Circle as CircleIcon } from '@mui/icons-material';

interface ApiErrorPayload {
  message: string;
  errors?: string[];
}

export const GlobalErrorModal = () => {
  const [open, setOpen] = useState(false);
  const [payload, setPayload] = useState<ApiErrorPayload | null>(null);

  useEffect(() => {
    const handleApiError = (event: Event) => {
      const customEvent = event as CustomEvent<ApiErrorPayload>;
      setPayload(customEvent.detail);
      setOpen(true);
    };

    window.addEventListener('api-error', handleApiError);
    return () => window.removeEventListener('api-error', handleApiError);
  }, []);

  const handleClose = () => {
    setOpen(false);
    // Don't clear payload immediately so exit animation looks smooth
    setTimeout(() => setPayload(null), 300);
  };

  if (!payload) return null;

  return (
    <Dialog open={open} onClose={handleClose} maxWidth="xs" fullWidth sx={{ zIndex: 9999 }} slotProps={{ paper: { sx: { borderRadius: 3, p: 1, textAlign: 'center' } } }}>
      <DialogTitle sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: 1, color: '#d32f2f', fontWeight: 600, pb: 1 }}>
        <ErrorIcon color="error" sx={{ fontSize: 48 }} />
        Action Failed
      </DialogTitle>
      <DialogContent sx={{ pb: 2 }}>
        <Typography variant="body2" sx={{ color: '#554434', fontWeight: 500, mb: payload.errors && payload.errors.length > 0 ? 2 : 0 }}>
          {payload.message}
        </Typography>
        {payload.errors && payload.errors.length > 0 && (
          <Box sx={{ bgcolor: '#ffebee', p: 1.5, borderRadius: 2, textAlign: 'left' }}>
            <List dense disablePadding>
              {payload.errors.map((err, idx) => (
                <ListItem key={idx} disablePadding sx={{ alignItems: 'flex-start', mb: 0.5 }}>
                  <ListItemIcon sx={{ minWidth: 20, mt: 0.5 }}>
                    <CircleIcon sx={{ fontSize: 6, color: '#d32f2f' }} />
                  </ListItemIcon>
                  <ListItemText primary={<Typography variant="caption" sx={{ color: '#c62828', fontWeight: 500 }}>{err}</Typography>} />
                </ListItem>
              ))}
            </List>
          </Box>
        )}
      </DialogContent>
      <DialogActions sx={{ justifyContent: 'center', pt: 0, pb: 2 }}>
        <Button onClick={handleClose} variant="contained" color="error" size="small" sx={{ textTransform: 'none', fontWeight: 600, borderRadius: 2, boxShadow: 'none', px: 4 }}>
          Close
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export const showApiError = (message: string, errors?: string[]) => {
  window.dispatchEvent(new CustomEvent('api-error', { detail: { message, errors } }));
};
