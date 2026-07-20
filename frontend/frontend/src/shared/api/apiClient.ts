import axios from "axios";
import { toast } from "sonner";

import { API_URL } from "../constants/app";
import { storage } from "../utils/storage";
import { getApiError } from "./apiError";

const apiClient = axios.create({

    baseURL: API_URL,
    timeout: 30000

});

apiClient.interceptors.request.use(config => {
    const token = storage.getAccessToken();

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
});

apiClient.interceptors.response.use(
    response => response,
    error => {
        if (error.response?.status === 401) {
            storage.clear();
        }
        toast.error(getApiError(error));
        return Promise.reject(error);
    }

);

export default apiClient;
