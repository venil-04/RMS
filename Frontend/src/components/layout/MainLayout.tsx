import { Box } from '@mui/material';
import { Outlet } from 'react-router-dom';
import { Sidebar } from './Sidebar';
import { Header } from './Header';

export const MainLayout = () => {
  return (
    <Box sx={{ display: 'flex', minHeight: '100vh', backgroundColor: '#fbf9f9' }}>
      <Header />
      <Sidebar />
      <Box 
        component="main" 
        sx={{ 
          flexGrow: 1, 
          p: { xs: 2, md: 3 }, 
          mt: '56px',
          width: { md: `calc(100% - 240px)` },
          overflowX: 'hidden'
        }}
      >
        <Outlet />
      </Box>
    </Box>
  );
};
