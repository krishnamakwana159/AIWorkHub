import apiClient from "@/shared/api/apiClient";
import { unwrapApiResult, type ApiResult } from "@/shared/api/apiResult";

import type { Notification } from "../types/notification";

export async function getNotifications() {
    const { data } =
        await apiClient.get<Notification[] | ApiResult<Notification[]>>(
            "/notifications"
        );

    return unwrapApiResult(data);
}

export async function getUnreadNotificationCount() {
    const { data } =
        await apiClient.get<number | ApiResult<number>>(
            "/notifications/unread-count"
        );

    return unwrapApiResult(data);
}

export async function markNotificationAsRead(id: string) {
    await apiClient.put(`/notifications/${id}/read`);
}

export async function markAllNotificationsAsRead() {
    await apiClient.put("/notifications/read-all");
}

export async function deleteNotification(id: string) {
    await apiClient.delete(`/notifications/${id}`);
}
