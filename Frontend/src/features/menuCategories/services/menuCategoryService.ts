import { apiClient } from "../../../services/apiClient";
import { apiRoutes } from "../../../constants/apiRoutes";
import { type MenuCategory } from "../types/menuCategoryTypes";

export const menuCategoryService = {
  getAll: async (): Promise<MenuCategory[]> => {
    const response = await apiClient.get<MenuCategory[]>(
      apiRoutes.menuCategories.getAll
    );

    return response.data;
  },
};