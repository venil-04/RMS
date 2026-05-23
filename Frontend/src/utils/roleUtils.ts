export const getDefaultRouteForRole = (roleName: string): string => {
  if (!roleName) return '/users';

  const normalizedRole = roleName.toLowerCase();
  
  // Temporarily returning /users for all roles to match expected flow
  return '/users';
};
