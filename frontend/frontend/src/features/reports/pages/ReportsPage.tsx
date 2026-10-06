import { useState } from "react";

import {
    Button,
    Card,
    CardContent,
    Divider,
    Grid,
    MenuItem,
    Stack,
    TextField,
    Typography
} from "@mui/material";
import DownloadIcon from "@mui/icons-material/Download";

import PageContainer from "@/components/common/PageContainer";
import PageTitle from "@/components/common/PageTitle";
import AppLoader from "@/components/ui/AppLoader";
import EmptyState from "@/components/ui/EmptyState";

import MonthlyTrendChart from "@/components/charts/MonthlyTrendChart";
import StatusDonutChart from "@/components/charts/StatusDonutChart";

import { useDashboardAnalytics } from "@/features/dashboard/hooks/useDashboardAnalytics";
import { useProjects } from "@/features/projects/hooks/useProjects";
import { useUserLookup } from "@/features/projects/hooks/useUserLookup";

import ReportStatsGrid from "../components/ReportStatsGrid";
import { useProjectReport } from "../hooks/useProjectReport";
import { useUserProductivityReport } from "../hooks/useUserProductivityReport";
import { exportDashboardCsv, exportDashboardExcel } from "../api/reportsApi";

const STATUS_COLORS = {
    todo: "#94a3b8",
    inProgress: "#2563eb",
    review: "#f59e0b",
    done: "#16a34a"
};

export default function ReportsPage() {
    const {
        data: analytics,
        isPending: analyticsPending,
        isError: analyticsError
    } = useDashboardAnalytics();

    const { data: projects = [] } = useProjects({ page: 1, pageSize: 100 });
    const { data: users = [] } = useUserLookup();

    const [selectedProjectId, setSelectedProjectId] = useState("");
    const [selectedUserId, setSelectedUserId] = useState("");

    const { data: projectReport, isPending: projectReportPending } =
        useProjectReport(selectedProjectId || undefined);

    const { data: userReport, isPending: userReportPending } =
        useUserProductivityReport(selectedUserId || undefined);

    return (
        <PageContainer>
            <Stack
                direction="row"
                sx={{ justifyContent: "space-between", alignItems: "center", flexWrap: "wrap", gap: 2 }}
            >
                <PageTitle
                    title="Reports"
                    subtitle="Project analytics and reports"
                />

                <Stack direction="row" spacing={1}>
                    <Button
                        variant="outlined"
                        startIcon={<DownloadIcon />}
                        onClick={() => exportDashboardExcel()}
                    >
                        Export Excel
                    </Button>

                    <Button
                        variant="outlined"
                        startIcon={<DownloadIcon />}
                        onClick={() => exportDashboardCsv()}
                    >
                        Export CSV
                    </Button>
                </Stack>
            </Stack>

            <Stack spacing={4} sx={{ mt: 3 }}>
                <Card>
                    <CardContent>
                        <Typography variant="h6" gutterBottom>
                            Overview
                        </Typography>

                        <Divider sx={{ mb: 3 }} />

                        {analyticsPending && <AppLoader />}

                        {analyticsError && (
                            <EmptyState message="Unable to load dashboard analytics." />
                        )}

                        {analytics && (
                            <Stack spacing={3}>
                                <ReportStatsGrid
                                    stats={[
                                        { label: "Projects", value: analytics.overview.totalProjects },
                                        { label: "Tasks", value: analytics.overview.totalTasks },
                                        { label: "Completed Tasks", value: analytics.overview.completedTasks },
                                        { label: "Active Tasks", value: analytics.overview.activeTasks },
                                        { label: "Overdue Tasks", value: analytics.overview.overdueTasks },
                                        {
                                            label: "Completion",
                                            value: `${analytics.overview.completionPercentage}%`
                                        },
                                        { label: "Estimated Hours", value: analytics.overview.estimatedHours },
                                        { label: "Actual Hours", value: analytics.overview.actualHours }
                                    ]}
                                />
                            </Stack>
                        )}
                    </CardContent>
                </Card>

                {analytics && <MonthlyTrendChart data={analytics.monthlyTrend} />}

                <Card>
                    <CardContent>
                        <Stack
                            direction="row"
                            sx={{ justifyContent: "space-between", alignItems: "center", mb: 2 }}
                        >
                            <Typography variant="h6">Project Report</Typography>

                            <TextField
                                select
                                size="small"
                                label="Project"
                                value={selectedProjectId}
                                onChange={(e) => setSelectedProjectId(e.target.value)}
                                sx={{ minWidth: 220 }}
                            >
                                {projects.map((project) => (
                                    <MenuItem key={project.id} value={project.id}>
                                        {project.name}
                                    </MenuItem>
                                ))}
                            </TextField>
                        </Stack>

                        <Divider sx={{ mb: 3 }} />

                        {!selectedProjectId && (
                            <EmptyState message="Select a project to see its report." />
                        )}

                        {selectedProjectId && projectReportPending && <AppLoader />}

                        {selectedProjectId && projectReport && (
                            <Grid container spacing={3}>
                                <Grid size={{ xs: 12, md: 7 }}>
                                    <ReportStatsGrid
                                        stats={[
                                            { label: "Total Tasks", value: projectReport.totalTasks },
                                            { label: "Overdue", value: projectReport.overdueTasks },
                                            {
                                                label: "Completion",
                                                value: `${projectReport.completionPercentage}%`
                                            },
                                            { label: "Estimated Hours", value: projectReport.estimatedHours },
                                            { label: "Actual Hours", value: projectReport.actualHours }
                                        ]}
                                    />
                                </Grid>

                                <Grid size={{ xs: 12, md: 5 }}>
                                    <StatusDonutChart
                                        title="Task Status Breakdown"
                                        data={[
                                            { name: "To Do", value: projectReport.todoTasks, color: STATUS_COLORS.todo },
                                            { name: "In Progress", value: projectReport.inProgressTasks, color: STATUS_COLORS.inProgress },
                                            { name: "In Review", value: projectReport.reviewTasks, color: STATUS_COLORS.review },
                                            { name: "Completed", value: projectReport.doneTasks, color: STATUS_COLORS.done }
                                        ]}
                                    />
                                </Grid>
                            </Grid>
                        )}
                    </CardContent>
                </Card>

                <Card>
                    <CardContent>
                        <Stack
                            direction="row"
                            sx={{ justifyContent: "space-between", alignItems: "center", mb: 2 }}
                        >
                            <Typography variant="h6">User Productivity</Typography>

                            <TextField
                                select
                                size="small"
                                label="User"
                                value={selectedUserId}
                                onChange={(e) => setSelectedUserId(e.target.value)}
                                sx={{ minWidth: 220 }}
                            >
                                {users.map((user) => (
                                    <MenuItem key={user.id} value={user.id}>
                                        {user.fullName}
                                    </MenuItem>
                                ))}
                            </TextField>
                        </Stack>

                        <Divider sx={{ mb: 3 }} />

                        {!selectedUserId && (
                            <EmptyState message="Select a user to see their productivity report." />
                        )}

                        {selectedUserId && userReportPending && <AppLoader />}

                        {selectedUserId && userReport && (
                            <Grid container spacing={3}>
                                <Grid size={{ xs: 12, md: 7 }}>
                                    <ReportStatsGrid
                                        stats={[
                                            { label: "Assigned Tasks", value: userReport.assignedTasks },
                                            { label: "Overdue", value: userReport.overdueTasks },
                                            {
                                                label: "Completion Rate",
                                                value: `${userReport.completionRate}%`
                                            },
                                            {
                                                label: "Productivity Score",
                                                value: userReport.productivityScore
                                            },
                                            { label: "Estimated Hours", value: userReport.estimatedHours },
                                            { label: "Actual Hours", value: userReport.actualHours }
                                        ]}
                                    />
                                </Grid>

                                <Grid size={{ xs: 12, md: 5 }}>
                                    <StatusDonutChart
                                        title="Task Status Breakdown"
                                        data={[
                                            { name: "To Do", value: userReport.todoTasks, color: STATUS_COLORS.todo },
                                            { name: "In Progress", value: userReport.inProgressTasks, color: STATUS_COLORS.inProgress },
                                            { name: "In Review", value: userReport.reviewTasks, color: STATUS_COLORS.review },
                                            { name: "Completed", value: userReport.completedTasks, color: STATUS_COLORS.done }
                                        ]}
                                    />
                                </Grid>
                            </Grid>
                        )}
                    </CardContent>
                </Card>
            </Stack>
        </PageContainer>
    );
}
