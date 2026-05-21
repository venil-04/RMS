export interface LoginRequest {
  email: string;
  password?: string;
}

export interface User {
  userId: number;
  restaurantId: number;
  roleId: number;
  roleName: string;
  firstName: string;
  lastName?: string;
  email: string;
}

export interface LoginResponse {
  accessToken: string;
  user: User;
}
