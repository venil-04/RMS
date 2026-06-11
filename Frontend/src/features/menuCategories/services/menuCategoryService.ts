import axiosInstance from "../../../api/axios";
import { type ApiResponse } from "../../auth/services/authService";
import { 
  type MenuCategory, 
  type CreateMenuCategoryRequest, 
  type UpdateMenuCategoryRequest 
} from "../types/menuCategoryTypes";

export const menuCategoryService = {
  getAll: async (): Promise<MenuCategory[]> => {
    const response = await axiosInstance.get<ApiResponse<MenuCategory[]>>(
      "/MenuCategory"
    );
    return response.data.data;
  },

  getById: async (id: number): Promise<MenuCategory> => {
    const response = await axiosInstance.get<ApiResponse<MenuCategory>>(
      `/MenuCategory/${id}`
    );
    return response.data.data;
  },

  create: async (request: CreateMenuCategoryRequest): Promise<MenuCategory> => {
    const response = await axiosInstance.post<ApiResponse<MenuCategory>>(
      "/MenuCategory/Create",
      request
    );
    return response.data.data;
  },

  update: async (request: UpdateMenuCategoryRequest): Promise<MenuCategory> => {
    const response = await axiosInstance.put<ApiResponse<MenuCategory>>(
      "/MenuCategory/Update",
      request
    );
    return response.data.data;
  },

  delete: async (id: number): Promise<void> => {
    const response = await axiosInstance.delete<ApiResponse<boolean>>(
      `/MenuCategory/${id}`
    );
    if (!response.data.success) {
      throw new Error(response.data.message || "Failed to delete menu category");
    }
  },
};