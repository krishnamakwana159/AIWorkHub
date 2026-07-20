import axios from "axios";

import { API_URL } from "@/shared/constants/app";
import { storage } from "@/shared/utils/storage";

const api = axios.create({
    baseURL: API_URL,
    timeout: 30000
});

api.interceptors.request.use(config => {
    const token = storage.getAccessToken();

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;

});

export default api;
