import axios from 'axios';
import { getAccessToken } from '../utils/storage';
import { showApiError } from '../components/common/GlobalErrorModal';

const axiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_URL || 'http://localhost:5000/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

axiosInstance.interceptors.request.use(
  (config) => {
    const token = getAccessToken();
    if (token && config.headers) {
      config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axiosInstance.interceptors.response.use(
  (response) => {
    // If the server returns a 200 OK but success is false in the payload
    if (response.data && response.data.success === false) {
      showApiError(
        response.data.message || 'An unexpected error occurred.',
        response.data.errors || undefined
      );
      return Promise.reject(new Error(response.data.message || 'API Error'));
    }
    return response;
  },
  (error) => {
    // Handle HTTP errors (e.g. 400 Bad Request, 500 Internal Server Error)
    if (error.response && error.response.data) {
      const data = error.response.data;
      
      // Check if it's our custom ApiResponse format
      if (data.message !== undefined) {
        showApiError(data.message, data.errors || undefined);
        return Promise.reject(new Error(data.message));
      } else {
        // Fallback for standard .NET or unexpected errors
        showApiError(error.message || 'A network or server error occurred.');
      }
    } else {
      // Handle cases where there is no response (e.g. CORS, offline)
      showApiError(error.message || 'Network error. Please check your connection.');
    }
    
    return Promise.reject(error);
  }
);

export default axiosInstance;
