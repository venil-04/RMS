import axiosInstance from '../../../api/axios';
import { type ApiResponse } from '../../auth/services/authService';
import { type CreateUserRequest, type UpdateUserRequest, type GetUsersRequest, type PagedResponse, type UserListItemResponse } from '../types/userTypes';

export const createUser = async (request: CreateUserRequest): Promise<void> => {
  const response = await axiosInstance.post<ApiResponse<void>>('/user/CreateUser', request);

  if (!response.data.success) {
    throw new Error(response.data.message || 'Failed to create user');
  }
};

export const updateUser = async (request: UpdateUserRequest): Promise<void> => {
  const response = await axiosInstance.post<ApiResponse<void>>('/user/UpdateUser', request);

  if (!response.data.success) {
    throw new Error(response.data.message || 'Failed to update user');
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

