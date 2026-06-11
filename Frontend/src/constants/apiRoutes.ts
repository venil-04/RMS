export const apiRoutes = {
    auth: {
      login: "/auth/login",
      register: "/auth/register",
    },
  
    menuCategories: {
      getAll: "/MenuCategory",
      getById: (id: number) => `/MenuCategory/${id}`,
      create: "/MenuCategory/Create",
      update: "/MenuCategory/Update",
      delete: (id: number) => `/MenuCategory/${id}`,
    },
  };