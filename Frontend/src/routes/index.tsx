import { Routes, Route, Navigate } from 'react-router-dom';
import Login from '../features/auth/pages/Login';
import Dashboard from '../pages/Dashboard';
import UserManagement from '../features/users/pages/UserManagement';
import MenuCategoryListPage from '../features/menuCategories/pages/MenuCategoryListPage';
import { ProtectedRoute } from './ProtectedRoute';
import { PublicRoute } from './PublicRoute';
import { MainLayout } from '../components/layout/MainLayout';
import { Typography, Box } from '@mui/material';

const Placeholder = ({ title }: { title: string }) => (
  <Box><Typography variant="h4">{title}</Typography></Box>
);

export const AppRoutes = () => {
  return (
    <Routes>
      <Route element={<PublicRoute />}>
        <Route path="/login" element={<Login />} />
      </Route>

      <Route element={<ProtectedRoute />}>
        <Route element={<MainLayout />}>
          <Route path="/" element={<Navigate to="/users" replace />} />
          <Route path="/dashboard" element={<Dashboard />} />
          <Route path="/users" element={<UserManagement />} />
          <Route path="/menu-categories" element={<MenuCategoryListPage />} />
          
          <Route path="/super-admin/dashboard" element={<Placeholder title="Super Admin Dashboard" />} />
          <Route path="/admin/dashboard" element={<Placeholder title="Admin Dashboard" />} />
          <Route path="/manager/dashboard" element={<Placeholder title="Manager Dashboard" />} />
          <Route path="/chef/orders" element={<Placeholder title="Chef Orders" />} />
          <Route path="/waiter/tables" element={<Placeholder title="Waiter Tables" />} />
          <Route path="/cashier/billing" element={<Placeholder title="Cashier Billing" />} />
        </Route>
      </Route>

      <Route path="*" element={<Navigate to="/login" replace />} />
    </Routes>
  );
};
