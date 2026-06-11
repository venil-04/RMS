import React, { useState } from 'react';
import {
  Drawer, List, ListItem, ListItemButton, ListItemIcon, ListItemText,
  Box, Typography, Avatar, Collapse
} from '@mui/material';
import { useLocation, useNavigate } from 'react-router-dom';
import DashboardIcon from '@mui/icons-material/Dashboard';
import LayersIcon from '@mui/icons-material/Layers';
import ReceiptLongIcon from '@mui/icons-material/ReceiptLong';
import RestaurantIcon from '@mui/icons-material/Restaurant';
import PaymentsIcon from '@mui/icons-material/Payments';
import MenuBookIcon from '@mui/icons-material/MenuBook';
import SettingsIcon from '@mui/icons-material/Settings';
import PeopleIcon from '@mui/icons-material/People';
import ExpandLessIcon from '@mui/icons-material/ExpandLess';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

const drawerWidth = 240;

interface SubMenuItem {
  text: string;
  path: string;
}

interface MenuItemType {
  text: string;
  icon: React.ReactNode;
  path: string;
  subItems?: SubMenuItem[];
}

const menuItems: MenuItemType[] = [
  { text: 'Dashboard', icon: <DashboardIcon />, path: '/dashboard' },
  { text: 'Users', icon: <PeopleIcon />, path: '/users' },
  { text: 'Live Floor', icon: <LayersIcon />, path: '/live-floor' },
  { text: 'Orders', icon: <ReceiptLongIcon />, path: '/orders' },
  { text: 'Kitchen', icon: <RestaurantIcon />, path: '/kitchen' },
  { text: 'Billing', icon: <PaymentsIcon />, path: '/billing' },
  {
    text: 'Menu',
    icon: <MenuBookIcon />,
    path: '/menu',
    subItems: [
      { text: 'Categories', path: '/menu-categories' },
      { text: 'Items', path: '/menu-items' }
    ]
  },
  { text: 'Settings', icon: <SettingsIcon />, path: '/settings' },
];

export const Sidebar = () => {
  const location = useLocation();
  const navigate = useNavigate();

  const [menuOpen, setMenuOpen] = useState(() => {
    return location.pathname.startsWith('/menu-categories') || location.pathname.startsWith('/menu-items');
  });

  const handleMenuClick = (item: MenuItemType) => {
    if (item.subItems) {
      setMenuOpen(!menuOpen);
    } else {
      navigate(item.path);
    }
  };

  return (
    <Drawer
      variant="permanent"
      sx={{
        width: drawerWidth,
        flexShrink: 0,
        display: { xs: 'none', md: 'block' },
        '& .MuiDrawer-paper': {
          width: drawerWidth,
          boxSizing: 'border-box',
          backgroundColor: '#151b2f', // from on-secondary-fixed
          color: '#ffffff',
          borderRight: 'none',
        },
      }}
    >
      <Box sx={{ p: 2.5, borderBottom: '1px solid rgba(255, 255, 255, 0.1)' }}>
        <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
          <Avatar sx={{ bgcolor: '#ff9800', color: '#653900', fontWeight: 'bold' }}>D</Avatar>
          <Box>
            <Typography variant="h6" sx={{ fontWeight: 700, color: '#ffffff' }}>
              DineMaster Pro
            </Typography>
            <Typography variant="body2" sx={{ color: '#c0c5e1' }}>
              Administrator
            </Typography>
          </Box>
        </Box>
      </Box>

      <List sx={{ px: 1.5, py: 1.5, display: 'flex', flexDirection: 'column', gap: 0.5 }}>
        {menuItems.map((item) => {
          const hasSubItems = !!item.subItems;
          const active = hasSubItems
            ? item.subItems?.some((sub) => location.pathname.startsWith(sub.path))
            : location.pathname.startsWith(item.path);

          return (
            <React.Fragment key={item.text}>
              <ListItem disablePadding>
                <ListItemButton
                  onClick={() => handleMenuClick(item)}
                  sx={{
                    borderRadius: 1,
                    py: 1,
                    backgroundColor: active ? 'rgba(255, 255, 255, 0.1)' : 'transparent',
                    borderLeft: active ? '4px solid #8b5000' : '4px solid transparent',
                    '&:hover': {
                      backgroundColor: 'rgba(255, 255, 255, 0.05)',
                    },
                  }}
                >
                  <ListItemIcon sx={{ color: active ? '#ffffff' : '#c0c5e1', minWidth: 40 }}>
                    {item.icon}
                  </ListItemIcon>
                  <ListItemText
                    primary={item.text}
                    slotProps={{
                      primary: {
                        sx: {
                          fontWeight: active ? 600 : 500,
                          color: active ? '#ffffff' : '#c0c5e1'
                        }
                      }
                    }}
                  />
                  {hasSubItems && (
                    menuOpen ? <ExpandLessIcon sx={{ color: '#c0c5e1', fontSize: 20 }} /> : <ExpandMoreIcon sx={{ color: '#c0c5e1', fontSize: 20 }} />
                  )}
                </ListItemButton>
              </ListItem>

              {hasSubItems && (
                <Collapse in={menuOpen} timeout="auto" unmountOnExit>
                  <List component="div" disablePadding sx={{ display: 'flex', flexDirection: 'column', gap: 0.5, mt: 0.5 }}>
                    {item.subItems?.map((subItem) => {
                      const subActive = location.pathname.startsWith(subItem.path);
                      return (
                        <ListItem key={subItem.text} disablePadding>
                          <ListItemButton
                            onClick={() => navigate(subItem.path)}
                            sx={{
                              borderRadius: 1,
                              py: 0.75,
                              pl: 6,
                              backgroundColor: subActive ? 'rgba(255, 255, 255, 0.08)' : 'transparent',
                              '&:hover': {
                                backgroundColor: 'rgba(255, 255, 255, 0.05)',
                              },
                            }}
                          >
                            <ListItemText
                              primary={subItem.text}
                              slotProps={{
                                primary: {
                                  sx: {
                                    fontWeight: subActive ? 600 : 500,
                                    color: subActive ? '#ffffff' : '#c0c5e1',
                                    fontSize: '0.875rem',
                                  }
                                }
                              }}
                            />
                          </ListItemButton>
                        </ListItem>
                      );
                    })}
                  </List>
                </Collapse>
              )}
            </React.Fragment>
          );
        })}
      </List>
    </Drawer>
  );
};
