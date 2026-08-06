import { useMutation, useQueryClient } from "@tanstack/react-query";

import {
    deleteNotification,
    markAllNotificationsAsRead,
    markNotificationAsRead
} from "../api/notificationsApi";

export function useMarkNotificationAsRead() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: markNotificationAsRead,
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ["notifications"] });
        }
    });
}

export function useMarkAllNotificationsAsRead() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: markAllNotificationsAsRead,
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ["notifications"] });
        }
    });
}

export function useDeleteNotification() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: deleteNotification,
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ["notifications"] });
        }
    });
}
