import axiosInstance from '../api/axios';
import { type ApiResponse } from '../features/auth/services/authService';
import { type RoleOption } from '../features/users/types/userTypes';

export const getRoles = async (): Promise<RoleOption[]> => {
  const response = await axiosInstance.get<ApiResponse<RoleOption[]>>('/Lookup/roles');

  if (!response.data.success) {
    throw new Error(response.data.message || 'Failed to fetch roles');
  }

  return response.data.data;
};
