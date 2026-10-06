import apiClient from "@/shared/api/apiClient";

import type {
    ChangePasswordRequest,
    UpdateProfileRequest,
    UserProfile
} from "../types/profile";

export async function getMyProfile() {
    const { data } = await apiClient.get<UserProfile>("/users/me");

    return data;
}

export async function updateMyProfile(request: UpdateProfileRequest) {
    const { data } = await apiClient.put<UserProfile>("/users/me", request);

    return data;
}

export async function changeMyPassword(request: ChangePasswordRequest) {
    await apiClient.put("/users/me/password", request);
}
