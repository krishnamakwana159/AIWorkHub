import apiClient from "../../../shared/api/apiClient";

import type {
    DashboardAnalyticsResponse
} from "../types/DashboardAnalyticsResponse";

export async function getDashboardAnalytics() {

    const response =
        await apiClient.get<DashboardAnalyticsResponse>(
            "/reports/dashboard-analytics"
        );

    return response.data;

}
