const TOKEN_KEY = 'rms_access_token';
const USER_KEY = 'rms_user';

export const getAccessToken = (): string | null => {
  return localStorage.getItem(TOKEN_KEY);
};

export const setAccessToken = (token: string): void => {
  localStorage.setItem(TOKEN_KEY, token);
};

export const removeAccessToken = (): void => {
  localStorage.removeItem(TOKEN_KEY);
};

export const getLoggedInUser = <T = any>(): T | null => {
  const user = localStorage.getItem(USER_KEY);
  if (!user) return null;
  try {
    return JSON.parse(user) as T;
  } catch {
    return null;
  }
};

export const setLoggedInUser = (user: any): void => {
  localStorage.setItem(USER_KEY, JSON.stringify(user));
};

export const removeLoggedInUser = (): void => {
  localStorage.removeItem(USER_KEY);
};

export const clearAuthStorage = (): void => {
  removeAccessToken();
  removeLoggedInUser();
};
