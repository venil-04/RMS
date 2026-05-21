import React from 'react';
import { Box, Typography, Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const Dashboard: React.FC = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    navigate('/login');
  };

  return (
    <Box sx={{ p: 4 }}>
      <Typography variant="h1" gutterBottom>
        Dashboard
      </Typography>
      <Typography variant="body1" sx={{ mb: 4 }}>
        Welcome to DineMaster Pro Dashboard!
      </Typography>
      <Button variant="contained" color="secondary" onClick={handleLogout}>
        Logout
      </Button>
    </Box>
  );
};

export default Dashboard;
