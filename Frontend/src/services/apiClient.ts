import axios from "axios";
import { appConfig } from "../config/appConfig";

export const apiClient = axios.create({
  baseURL: appConfig.apiBaseUrl,
  headers: {
    "Content-Type": "application/json",
  },
});