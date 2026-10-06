import apiClient from "@/shared/api/apiClient";

import type { ProjectReport, UserProductivityReport } from "../types/report";

export async function getProjectReport(projectId: string) {
    const { data } = await apiClient.get<ProjectReport>(
        `/reports/project/${projectId}`
    );

    return data;
}

export async function getUserProductivityReport(userId: string) {
    const { data } = await apiClient.get<UserProductivityReport>(
        `/reports/user/${userId}`
    );

    return data;
}

async function downloadFile(url: string, fileName: string) {
    const response = await apiClient.get(url, { responseType: "blob" });

    const objectUrl = window.URL.createObjectURL(response.data);
    const a = document.createElement("a");
    a.href = objectUrl;
    a.download = fileName;
    a.click();
    window.URL.revokeObjectURL(objectUrl);
}

export async function exportDashboardExcel() {
    await downloadFile(
        "/reports/dashboard-analytics/export/excel",
        `DashboardAnalytics-${Date.now()}.xlsx`
    );
}

export async function exportDashboardCsv() {
    await downloadFile(
        "/reports/dashboard-analytics/export/csv",
        `DashboardAnalytics-${Date.now()}.csv`
    );
}
