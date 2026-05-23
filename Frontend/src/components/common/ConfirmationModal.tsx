import {
  Dialog, DialogTitle, DialogContent, DialogActions,
  Button, Typography, IconButton
} from '@mui/material';
import { Close as CloseIcon } from '@mui/icons-material';

interface ConfirmationModalProps {
  open: boolean;
  title: string;
  message: string;
  confirmText?: string;
  cancelText?: string;
  onConfirm: () => void;
  onCancel: () => void;
  loading?: boolean;
}

export const ConfirmationModal = ({
  open,
  title,
  message,
  confirmText = 'Confirm',
  cancelText = 'Cancel',
  onConfirm,
  onCancel,
  loading = false
}: ConfirmationModalProps) => {
  return (
    <Dialog
      open={open}
      onClose={loading ? undefined : onCancel}
      maxWidth="xs"
      fullWidth
      slotProps={{
        paper: { sx: { borderRadius: 3, bgcolor: '#ffffff' } }
      }}
    >
      <DialogTitle sx={{ px: 3, py: 2, borderBottom: '1px solid #efeded', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <Typography variant="h6" sx={{ fontWeight: 600, color: '#1b1c1c' }}>
          {title}
        </Typography>
        <IconButton size="small" onClick={onCancel} sx={{ color: '#554434' }} disabled={loading}>
          <CloseIcon />
        </IconButton>
      </DialogTitle>

      <DialogContent sx={{ px: 3, py: 3 }}>
        <Typography variant="body1" sx={{ color: '#1b1c1c' }}>
          {message}
        </Typography>
      </DialogContent>

      <DialogActions sx={{ p: 2, px: 3, borderTop: '1px solid #efeded', bgcolor: '#f5f3f3' }}>
        <Button
          onClick={onCancel}
          disabled={loading}
          sx={{ color: '#1b1c1c', textTransform: 'none', fontWeight: 600 }}
        >
          {cancelText}
        </Button>
        <Button
          variant="contained"
          onClick={onConfirm}
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
          {loading ? 'Processing...' : confirmText}
        </Button>
      </DialogActions>
    </Dialog>
  );
};
