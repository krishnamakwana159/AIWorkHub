export interface UserProfile {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    createdAtUtc: string;
}

export interface UpdateProfileRequest {
    firstName: string;
    lastName: string;
}

export interface ChangePasswordRequest {
    currentPassword: string;
    newPassword: string;
    confirmNewPassword: string;
}
