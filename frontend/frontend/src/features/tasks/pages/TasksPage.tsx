import { useNavigate } from "react-router-dom";

import { Chip, Link, Typography } from "@mui/material";

import PageContainer from "@/components/common/PageContainer";
import PageTitle from "@/components/common/PageTitle";
import AppLoader from "@/components/ui/AppLoader";
import EmptyState from "@/components/ui/EmptyState";
import DataTable from "@/components/ui/DataTable";

import { TaskPriorityInfo, WorkTaskStatusInfo } from "@/shared/constants/task";

import { useMyTasks } from "../hooks/useMyTasks";
import type { MyTask } from "../types/myTask";

export default function TasksPage() {
    const navigate = useNavigate();
    const { data: tasks = [], isPending, isError } = useMyTasks();

    return (
        <PageContainer>
            <PageTitle
                title="Tasks"
                subtitle="Every task across your projects"
            />

            {isPending && <AppLoader />}

            {isError && (
                <EmptyState message="Unable to load your tasks." />
            )}

            {!isPending && !isError && tasks.length === 0 && (
                <EmptyState message="No tasks yet. Add one from within a project." />
            )}

            {!isPending && !isError && tasks.length > 0 && (
                <DataTable<MyTask>
                    rows={tasks}
                    columns={[
                        {
                            header: "Task",
                            render: (task) => (
                                <Link
                                    component="button"
                                    onClick={() => navigate(`/tasks/${task.id}`)}
                                    sx={{ textAlign: "left" }}
                                >
                                    {task.title}
                                </Link>
                            )
                        },
                        {
                            header: "Project",
                            render: (task) => (
                                <Link
                                    component="button"
                                    onClick={() => navigate(`/projects/${task.projectId}`)}
                                    sx={{ textAlign: "left" }}
                                >
                                    {task.projectName}
                                </Link>
                            )
                        },
                        {
                            header: "Status",
                            render: (task) => (
                                <Chip
                                    size="small"
                                    label={WorkTaskStatusInfo[task.status].label}
                                    color={WorkTaskStatusInfo[task.status].color}
                                />
                            )
                        },
                        {
                            header: "Priority",
                            render: (task) => (
                                <Chip
                                    size="small"
                                    label={TaskPriorityInfo[task.priority].label}
                                    color={TaskPriorityInfo[task.priority].color}
                                />
                            )
                        },
                        {
                            header: "Assignee",
                            render: (task) => (
                                <Typography variant="body2">
                                    {task.assigneeName ?? "Unassigned"}
                                </Typography>
                            )
                        },
                        {
                            header: "Due Date",
                            render: (task) => (
                                <Typography variant="body2">
                                    {task.dueDateUtc
                                        ? new Date(task.dueDateUtc).toLocaleDateString()
                                        : "—"}
                                </Typography>
                            )
                        }
                    ]}
                />
            )}
        </PageContainer>
    );
}
