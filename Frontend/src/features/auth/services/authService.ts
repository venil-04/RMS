import axiosInstance from '../../../api/axios';
import { type LoginRequest, type LoginResponse } from '../types/authTypes';

export const loginAPI = async (credentials: LoginRequest): Promise<LoginResponse> => {
  const response = await axiosInstance.post<LoginResponse>('/auth/login', credentials);
  return response.data;
};
