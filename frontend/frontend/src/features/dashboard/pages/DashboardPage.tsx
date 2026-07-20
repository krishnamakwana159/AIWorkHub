import Grid from "@mui/material/Grid";

import PageContainer from "../../../components/common/PageContainer";
import PageTitle from "../../../components/common/PageTitle";

import StatCard from "../../../components/ui/StatCard";
import AppLoader from "../../../components/ui/AppLoader";
import EmptyState from "../../../components/ui/EmptyState";

import { useDashboardAnalytics } from "../hooks/useDashboardAnalytics";

export default function DashboardPage() {

    const {
        data,
        isPending,
        isError
    } = useDashboardAnalytics();

    if (isPending) {
        return (
            <PageContainer>
                <AppLoader />
            </PageContainer>
        );
    }

    if (isError || !data) {
        return (
            <PageContainer>
                <EmptyState
                    message="Unable to load dashboard analytics."
                />
            </PageContainer>
        );
    }

    const overview = data.value.overview;

    return (
        <PageContainer>

            <PageTitle
                title="Dashboard"
                subtitle="Welcome to AIWorkHub"
            />

            <Grid
                container
                spacing={3}
                sx={{ mt: 1 }}
            >
                <Grid size={{ xs: 12, sm: 6, lg: 3 }}>
                    <StatCard
                        title="Projects"
                        value={overview.totalProjects}
                    />
                </Grid>

                <Grid size={{ xs: 12, sm: 6, lg: 3 }}>
                    <StatCard
                        title="Users"
                        value={overview.totalUsers}
                    />
                </Grid>

                <Grid size={{ xs: 12, sm: 6, lg: 3 }}>
                    <StatCard
                        title="Tasks"
                        value={overview.totalTasks}
                    />
                </Grid>

                <Grid size={{ xs: 12, sm: 6, lg: 3 }}>
                    <StatCard
                        title="Completed"
                        value={overview.completedTasks}
                    />
                </Grid>

                <Grid size={{ xs: 12, sm: 6, lg: 3 }}>
                    <StatCard
                        title="Active Tasks"
                        value={overview.activeTasks}
                    />
                </Grid>

                <Grid size={{ xs: 12, sm: 6, lg: 3 }}>
                    <StatCard
                        title="Overdue Tasks"
                        value={overview.overdueTasks}
                    />
                </Grid>

                <Grid size={{ xs: 12, sm: 6, lg: 3 }}>
                    <StatCard
                        title="Estimated Hours"
                        value={overview.estimatedHours}
                    />
                </Grid>

                <Grid size={{ xs: 12, sm: 6, lg: 3 }}>
                    <StatCard
                        title="Completion"
                        value={`${overview.completionPercentage}%`}
                    />
                </Grid>
            </Grid>

        </PageContainer>
    );
}
