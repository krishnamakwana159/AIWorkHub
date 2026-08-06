import apiClient from "@/shared/api/apiClient";

import type { Activity } from "../types/activity";

export async function getActivities(entityId: string) {
    const { data } =
        await apiClient.get<Activity[]>(
            `/activities/${entityId}`
        );

    return data;
}
