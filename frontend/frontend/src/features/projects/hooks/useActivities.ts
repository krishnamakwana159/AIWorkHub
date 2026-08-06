import { useQuery } from "@tanstack/react-query";

import { getActivities } from "../api/activityApi";

export function useActivities(entityId: string) {
    return useQuery({
        queryKey: ["activities", entityId],
        queryFn: () => getActivities(entityId),
        enabled: !!entityId
    });
}
