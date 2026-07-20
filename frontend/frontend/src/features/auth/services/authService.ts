import apiClient from "../../../shared/api/apiClient";

import type { LoginRequest } from "../types/LoginRequest";
import type { AuthResponse } from "../types/AuthResponse";

class AuthService {

    async login(request: LoginRequest) {
        const response =
            await apiClient.post<AuthResponse>(
                "/auth/login",
                request
            );

        return response.data;
    }

}

export default new AuthService();
