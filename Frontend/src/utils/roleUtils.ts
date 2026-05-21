export const getDefaultRouteForRole = (roleName: string): string => {
  if (!roleName) return '/dashboard';

  const normalizedRole = roleName.toLowerCase();
  
  if (normalizedRole.includes('super admin')) {
    return '/super-admin/dashboard';
  }
  if (normalizedRole.includes('admin')) {
    return '/admin/dashboard';
  }
  if (normalizedRole.includes('manager')) {
    return '/manager/dashboard';
  }
  if (normalizedRole.includes('chef')) {
    return '/chef/orders';
  }
  if (normalizedRole.includes('waiter')) {
    return '/waiter/tables';
  }
  if (normalizedRole.includes('cashier')) {
    return '/cashier/billing';
  }
  
  return '/dashboard'; // Fallback
};
