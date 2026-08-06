import apiClient from "../../../shared/api/apiClient";
import { unwrapApiResult, type ApiResult } from "@/shared/api/apiResult";

import type {
    DashboardAnalyticsResponse
} from "../types/DashboardAnalyticsResponse";

export async function getDashboardAnalytics(): Promise<DashboardAnalyticsResponse> {

    const response =
        await apiClient.get<DashboardAnalyticsResponse | ApiResult<DashboardAnalyticsResponse>>(
            "/reports/dashboard-analytics"
        );

    return unwrapApiResult(response.data);

}
