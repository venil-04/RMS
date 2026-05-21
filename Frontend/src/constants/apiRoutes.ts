export const apiRoutes = {
    auth: {
      login: "/auth/login",
      register: "/auth/register",
    },
  
    menuCategories: {
      getAll: "/MenuCategories",
      getById: (id: number) => `/menu-categories/${id}`,
      create: "/menu-categories",
      update: (id: number) => `/menu-categories/${id}`,
      delete: (id: number) => `/menu-categories/${id}`,
    },
  };