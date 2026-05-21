import { Navigate, Outlet } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { type RootState } from '../store';
import { getDefaultRouteForRole } from '../utils/roleUtils';

export const PublicRoute = () => {
  const { isAuthenticated, user } = useSelector((state: RootState) => state.auth);

  if (isAuthenticated && user) {
    return <Navigate to={getDefaultRouteForRole(user.roleName)} replace />;
  }

  return <Outlet />;
};
