import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import Button from "@mui/material/Button";
import Grid from "@mui/material/Grid";

import PageContainer from "@/components/common/PageContainer";
import PageTitle from "@/components/common/PageTitle";

import AppLoader from "@/components/ui/AppLoader";
import EmptyState from "@/components/ui/EmptyState";

import TaskDialog from "../components/TaskDialog";
import TaskTable from "../components/TaskTable";
import DeleteTaskDialog from "../components/DeleteTaskDialog";

import { useTasks } from "../hooks/useTasks";
import { useDeleteTask } from "../hooks/useDeleteTask";

import type { WorkTask } from "../types/task";

export default function TasksPage() {

    const { id: projectId } = useParams();
    console.log("TasksPage projectId:", projectId);
    const navigate = useNavigate();

    const [dialogOpen, setDialogOpen] = useState(false);
    const [selectedTask, setSelectedTask] = useState<WorkTask | null>(null);
    const [deleteOpen, setDeleteOpen] = useState(false);
    const [taskToDelete, setTaskToDelete] = useState<WorkTask | null>(null);

    const deleteMutation =
        useDeleteTask();

    const {
        data: tasks,
        isPending,
        isError
    } = useTasks({
        projectId: projectId ?? ""
    });

    function handleCreate() {
        setSelectedTask(null);
        setDialogOpen(true);
    }

    function handleEdit(task: WorkTask) {
        setSelectedTask(task);
        setDialogOpen(true);
    }

    function handleView(task: WorkTask) {
        navigate(`/tasks/${task.id}`);
    }

    function handleDelete(task: WorkTask) {
        setTaskToDelete(task);
        setDeleteOpen(true);
    }

    async function confirmDelete() {
        if (!taskToDelete)
            return;

        await deleteMutation.mutateAsync(
            taskToDelete.id
        );
        setDeleteOpen(false);
        setTaskToDelete(null);
    }

    return (
        <PageContainer>
            <PageTitle
                title="Tasks"
                subtitle="Manage project tasks"
            />
            <Grid
                container
                spacing={3}
                sx={{ mt: 1 }}
            >
                <Grid size={12}>
                    <Button
                        variant="contained"
                        onClick={handleCreate}
                    >
                        New Task
                    </Button>
                </Grid>

                <Grid size={12}>
                    {isPending &&
                        <AppLoader />
                    }

                    {isError &&
                        <EmptyState
                            message="Unable to load tasks."
                        />
                    }

                    {!isPending &&
                        !isError &&
                        tasks && (
                            // <TextField
                            //     fullWidth
                            //     label="Search Tasks"
                            //     value={query.search ?? ""}
                            //     onChange={e =>
                            //         setQuery({
                            //             ...query,
                            //             search: e.target.value
                            //         })
                            //     }
                            // />
                            <TaskTable
                                tasks={tasks}
                                onView={handleView}
                                onEdit={handleEdit}
                                onDelete={handleDelete}
                            />
                        )}
                </Grid>
            </Grid>

            <TaskDialog
                open={dialogOpen}
                projectId={projectId!}
                task={selectedTask}
                onClose={() => {
                    setDialogOpen(false);
                    setSelectedTask(null);
                }}
            />

            <DeleteTaskDialog
                open={deleteOpen}
                taskTitle={
                    taskToDelete?.title ?? ""
                }
                loading={
                    deleteMutation.isPending
                }
                onClose={() =>
                    setDeleteOpen(false)
                }
                onConfirm={confirmDelete}
            />
        </PageContainer>
    );
}
