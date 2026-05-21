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
    body1: {
      fontSize: '18px',
      fontWeight: 400,
      lineHeight: '28px',
    },
    body2: {
      fontSize: '16px',
      fontWeight: 400,
      lineHeight: '24px',
    },
    button: {
      textTransform: 'none',
      fontWeight: 600,
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
          padding: '10px 24px',
          '&:hover': {
            boxShadow: 'none',
          },
        }
      },
    },
    MuiInputBase: {
      styleOverrides: {
        input: {
          '&:-webkit-autofill': {
            WebkitBoxShadow: '0 0 0 100px #ffffff inset',
            WebkitTextFillColor: '#1b1c1c',
          },
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
  },
};

export const theme = createTheme(proServeThemeOptions);
