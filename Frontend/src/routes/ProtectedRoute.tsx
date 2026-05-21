import { Navigate, Outlet } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { type RootState } from '../store';
import { getDefaultRouteForRole } from '../utils/roleUtils';

interface ProtectedRouteProps {
  allowedRoles?: string[];
}

export const ProtectedRoute = ({ allowedRoles }: ProtectedRouteProps) => {
  const { isAuthenticated, user } = useSelector((state: RootState) => state.auth);

  if (!isAuthenticated || !user) {
    return <Navigate to="/login" replace />;
  }

  if (allowedRoles && allowedRoles.length > 0) {
    const roleName = user.roleName.toLowerCase();
    const hasAccess = allowedRoles.some(role => roleName.includes(role.toLowerCase()));

    if (!hasAccess) {
      return <Navigate to={getDefaultRouteForRole(user.roleName)} replace />;
    }
  }

  return <Outlet />;
};
