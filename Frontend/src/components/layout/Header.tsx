import React from 'react';
import { 
  AppBar, Toolbar, IconButton, InputBase, Box, Avatar, useTheme, Typography 
} from '@mui/material';
import { Search as SearchIcon, Notifications as NotificationsIcon, Menu as MenuIcon, Logout as LogoutIcon } from '@mui/icons-material';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { type RootState } from '../../store';
import { logout } from '../../features/auth/authSlice';

const drawerWidth = 240;

export const Header = () => {
  const theme = useTheme();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const user = useSelector((state: RootState) => state.auth.user);

  const handleLogout = () => {
    dispatch(logout());
    navigate('/login');
  };

  return (
    <AppBar 
      position="fixed" 
      elevation={0}
      sx={{ 
        width: { md: `calc(100% - ${drawerWidth}px)` }, 
        ml: { md: `${drawerWidth}px` },
        backgroundColor: '#ffffff',
        borderBottom: '1px solid #efeded',
        color: '#1b1c1c',
      }}
    >
      <Toolbar sx={{ justifyContent: 'space-between', minHeight: '56px !important' }}>
        <Box sx={{ display: 'flex', alignItems: 'center', flex: 1 }}>
          <IconButton
            color="inherit"
            edge="start"
            sx={{ mr: 2, display: { md: 'none' } }}
          >
            <MenuIcon />
          </IconButton>
          
          <Box 
            sx={{ 
              display: { xs: 'none', md: 'flex' }, 
              alignItems: 'center', 
              backgroundColor: '#f5f3f3', 
              borderRadius: '9999px',
              px: 2,
              py: 0.5,
              width: '100%',
              maxWidth: '400px'
            }}
          >
            <SearchIcon sx={{ color: '#554434', mr: 1, fontSize: 20 }} />
            <InputBase
              placeholder="Search DineMaster..."
              sx={{ ml: 1, flex: 1 }}
            />
          </Box>
        </Box>

        <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
          <IconButton sx={{ color: '#554434' }}>
            <NotificationsIcon />
          </IconButton>
          <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
            <Avatar 
              sx={{ 
                bgcolor: '#8b5000', 
                width: 32, 
                height: 32, 
                fontWeight: 600
              }}
            >
              {user?.fullName?.[0] || 'U'}
            </Avatar>
            <Typography variant="body2" sx={{ display: { xs: 'none', sm: 'block' }, fontWeight: 600 }}>
              {user?.fullName}
            </Typography>
          </Box>
          <IconButton sx={{ color: '#554434' }} onClick={handleLogout} title="Logout">
            <LogoutIcon />
          </IconButton>
        </Box>
      </Toolbar>
    </AppBar>
  );
};
