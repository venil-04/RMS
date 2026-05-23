import axiosInstance from '../../../api/axios';
import { type ApiResponse } from '../../auth/services/authService';
import { type UpsertUserRequest, type GetUsersRequest, type PagedResponse, type UserListItemResponse } from '../types/userTypes';

export const upsertUser = async (request: UpsertUserRequest): Promise<void> => {
  const response = await axiosInstance.post<ApiResponse<void>>('/user/upsertUser', request);

  if (!response.data.success) {
    throw new Error(response.data.message || 'Failed to save user');
  }
};

export const getUsers = async (request: GetUsersRequest): Promise<PagedResponse<UserListItemResponse>> => {
  const response = await axiosInstance.post<ApiResponse<PagedResponse<UserListItemResponse>>>('/user/getUsers', request);

  if (!response.data.success) {
    throw new Error(response.data.message || 'Failed to fetch users');
  }

  return response.data.data;
};

export const deleteUser = async (userId: number): Promise<void> => {
  const response = await axiosInstance.post<ApiResponse<void>>('/user/deleteUser', { userId });
  if (!response.data.success) {
    throw new Error(response.data.message || 'Failed to delete user');
  }
};

