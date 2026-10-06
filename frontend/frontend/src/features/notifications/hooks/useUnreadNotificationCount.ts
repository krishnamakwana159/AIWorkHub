import { useQuery } from "@tanstack/react-query";

import { getUnreadNotificationCount } from "../api/notificationsApi";

export function useUnreadNotificationCount() {
    return useQuery({
        queryKey: ["notifications-unread-count"],
        queryFn: getUnreadNotificationCount,
        refetchInterval: 30000
    });
}
