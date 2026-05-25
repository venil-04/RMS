export interface UserFormValues {
  userId?: number;
  roleId: number | "";
  firstName: string;
  lastName?: string;
  email: string;
  mobileNumber?: string;
  password?: string;
  confirmPassword?: string;
  isActive?: boolean;
}

export interface CreateUserRequest {
  restaurantId: number;
  roleId: number;
  firstName: string;
  lastName?: string | null;
  email: string;
  mobileNumber?: string | null;
  isActive?: boolean;
  password?: string;
}

export interface UpdateUserRequest {
  userId: number;
  restaurantId: number;
  roleId: number;
  firstName: string;
  lastName?: string | null;
  email: string;
  mobileNumber?: string | null;
  isActive?: boolean;
}

export interface RoleOption {
  roleId: number;
  roleName: string;
}

export interface UserListItemResponse {
  userId: number;
  restaurantId: number;
  roleId: number;
  roleName: string;
  firstName: string;
  lastName?: string | null;
  fullName: string;
  email: string;
  mobileNumber?: string | null;
  isActive: boolean;
  createdAt: string;
}

export interface PagedResponse<T> {
  items: T[];
  totalRecords: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

export interface GetUsersRequest {
  search?: string;
  roleId?: number;
  isActive?: boolean;
  pageNumber: number;
  pageSize: number;
}
