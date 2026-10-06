import { useQuery } from "@tanstack/react-query";

import { getUserProductivityReport } from "../api/reportsApi";

export function useUserProductivityReport(userId?: string) {
    return useQuery({
        queryKey: ["user-productivity-report", userId],
        queryFn: () => getUserProductivityReport(userId as string),
        enabled: !!userId
    });
}
