export interface LoginRequest {
  email: string;
  password?: string;
}

export interface User {
  userId: number;
  restaurantId: number;
  roleId: number;
  roleName: string;
  fullName: string;
  email: string;
}

export interface LoginResponse {
  accessToken: string;
  user: User;
}
