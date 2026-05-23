import axiosInstance from '../../../api/axios';
import { type LoginRequest, type LoginResponse } from '../types/authTypes';

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
  errors: any;
}

export const loginAPI = async (credentials: LoginRequest): Promise<LoginResponse> => {
  const response = await axiosInstance.post<ApiResponse<LoginResponse>>('/auth/login', credentials);
  
  if (!response.data.success) {
    throw new Error(response.data.message || 'Login failed');
  }
  
  return response.data.data;
};
