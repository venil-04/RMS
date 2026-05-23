import { createTheme, type ThemeOptions } from '@mui/material/styles';

const proServeThemeOptions: ThemeOptions = {
  palette: {
    mode: 'light',
    primary: {
      main: '#ff9800',
      contrastText: '#ffffff',
    },
    secondary: {
      main: '#1a2035', // Deep Navy
      contrastText: '#ffffff',
    },
    error: {
      main: '#ba1a1a',
      contrastText: '#ffffff',
    },
    success: {
      main: '#4caf50',
      contrastText: '#ffffff',
    },
    background: {
      default: '#fbf9f9',
      paper: '#ffffff',
    },
    text: {
      primary: '#1b1c1c',
      secondary: '#554434',
    },
    divider: '#dbdad9',
  },
  typography: {
    fontFamily: '"Inter", "Roboto", "Helvetica", "Arial", sans-serif',
    h1: {
      fontSize: '40px',
      fontWeight: 700,
      lineHeight: '48px',
      letterSpacing: '-0.02em',
    },
    h2: {
      fontSize: '32px',
      fontWeight: 600,
      lineHeight: '40px',
      letterSpacing: '-0.01em',
    },
    h3: {
      fontSize: '24px',
      fontWeight: 600,
      lineHeight: '32px',
    },
    h5: {
      fontSize: '20px',
      fontWeight: 600,
    },
    h6: {
      fontSize: '16px',
      fontWeight: 600,
    },
    body1: {
      fontSize: '14px',
      fontWeight: 400,
    },
    body2: {
      fontSize: '12px',
      fontWeight: 400,
    },
    button: {
      fontSize: '14px',
      textTransform: 'none',
      fontWeight: 600,
    },
    caption: {
      fontSize: '10px',
    },
  },
  shape: {
    borderRadius: 8,
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: 8,
          boxShadow: 'none',
          padding: '8px 16px',
          '&:hover': {
            boxShadow: 'none',
          },
        }
      },
    },
    MuiInputBase: {
      styleOverrides: {
        root: {
          fontSize: '14px', // TextField inputs
        },
        input: {
          '&:-webkit-autofill': {
            WebkitBoxShadow: '0 0 0 100px #ffffff inset',
            WebkitTextFillColor: '#1b1c1c',
          },
        },
      },
    },
    MuiInputLabel: {
      styleOverrides: {
        root: {
          fontSize: '12px',
        },
      },
    },
    MuiTextField: {
      styleOverrides: {
        root: {
          '& .MuiOutlinedInput-root': {
            borderRadius: 8,
            backgroundColor: '#ffffff',
            '& fieldset': {
              borderColor: '#dbdad9',
            },
            '&:hover fieldset': {
              borderColor: '#1a2035',
            },
            '&.Mui-focused fieldset': {
              borderWidth: '2px',
              borderColor: '#ff9800',
            },
          },
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          borderRadius: 16,
          boxShadow: '0px 4px 20px rgba(0, 0, 0, 0.05)',
          border: '1px solid #efeded',
        },
      },
    },
    MuiTableCell: {
      styleOverrides: {
        root: {
          fontSize: '12px',
          padding: '8px 16px', // Compact table row height
        },
        head: {
          fontWeight: 600,
        },
      },
    },
    MuiDialogTitle: {
      styleOverrides: {
        root: {
          fontSize: '16px',
          fontWeight: 600,
          padding: '16px 24px',
        },
      },
    },
    MuiDialogContent: {
      styleOverrides: {
        root: {
          fontSize: '14px',
          padding: '24px',
        },
      },
    },
    MuiDialogActions: {
      styleOverrides: {
        root: {
          padding: '16px 24px',
        },
      },
    },
    MuiChip: {
      styleOverrides: {
        root: {
          fontSize: '10px',
          height: '24px',
          fontWeight: 500,
        },
      },
    },
  },
};

export const theme = createTheme(proServeThemeOptions);
