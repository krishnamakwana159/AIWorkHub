import { useQuery } from "@tanstack/react-query";

import { getDashboardAnalytics } from "../api/dashboardApi";

export function useDashboardAnalytics() {

    return useQuery({

        queryKey: ["dashboard-analytics"],

        queryFn: getDashboardAnalytics,

        staleTime: 1000 * 60 * 5

    });

}
