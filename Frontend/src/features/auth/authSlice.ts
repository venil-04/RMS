import { createSlice, type PayloadAction } from '@reduxjs/toolkit';
import type { User } from './types/authTypes';
import { getAccessToken, getLoggedInUser, setAccessToken, setLoggedInUser, clearAuthStorage } from '../../utils/storage';

interface AuthState {
  isAuthenticated: boolean;
  user: User | null;
  token: string | null;
}

const initialState: AuthState = {
  isAuthenticated: !!getAccessToken(),
  user: getLoggedInUser<User>(),
  token: getAccessToken(),
};

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    loginSuccess: (state, action: PayloadAction<{ user: User; token: string }>) => {
      state.isAuthenticated = true;
      state.user = action.payload.user;
      state.token = action.payload.token;

      setAccessToken(action.payload.token);
      setLoggedInUser(action.payload.user);
    },
    logout: (state) => {
      state.isAuthenticated = false;
      state.user = null;
      state.token = null;
      clearAuthStorage();
    },
  },
});

export const { loginSuccess, logout } = authSlice.actions;
export default authSlice.reducer;
