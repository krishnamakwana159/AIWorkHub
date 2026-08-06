import apiClient from "@/shared/api/apiClient";

export interface UserLookup {
    id: string;
    fullName: string;
    email: string;
}

export async function getUserLookup() {
    const { data } =
        await apiClient.get<UserLookup[]>(
            "/users/lookup"
        );

    return data;
}
