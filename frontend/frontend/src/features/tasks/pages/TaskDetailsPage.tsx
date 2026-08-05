import { useParams } from "react-router-dom";
import Grid from "@mui/material/Grid";

import PageContainer from "@/components/common/PageContainer";

import AppLoader from "@/components/ui/AppLoader";
import EmptyState from "@/components/ui/EmptyState";

import TaskHeader from "../components/TaskHeader";
import TaskOverviewCard from "../components/TaskOverviewCard";
import TaskStatisticsCard from "../components/TaskStatisticsCard";
import TaskDialog from "../components/TaskDialog";

import { useTask } from "../hooks/useTask";
import { useState } from "react";
import CommentsSection from "../components/CommentsSection";
import AttachmentsSection from "../components/AttachmentsSection";

export default function TaskDetailsPage() {

    const { id } = useParams();
    const [editOpen, setEditOpen] =
        useState(false);

    const {
        data: task,
        isPending,
        isError
    } = useTask(id!);

    if (isPending)
        return <AppLoader />;

    if (isError || !task)
        return (
            <EmptyState
                message="Task not found."
            />
        );

    return (
        <PageContainer>
            <TaskHeader
                task={task}
                onEdit={() =>
                    setEditOpen(true)
                }
            />
            <Grid
                container
                spacing={3}
            >
                <Grid size={12}>
                    <TaskOverviewCard
                        task={task}
                    />
                </Grid>

                <Grid size={12}>
                    <TaskStatisticsCard
                        task={task}
                    />
                </Grid>

                <Grid size={12}>
                    <CommentsSection
                        taskId={task.id}
                    />
                </Grid>

                <Grid size={12}>
                    <AttachmentsSection
                        taskId={task.id}
                    />
                </Grid>
            </Grid>

            <TaskDialog
                open={editOpen}
                projectId={task.projectId}
                task={task}
                onClose={() =>
                    setEditOpen(false)
                }
            />
        </PageContainer>
    );
}
