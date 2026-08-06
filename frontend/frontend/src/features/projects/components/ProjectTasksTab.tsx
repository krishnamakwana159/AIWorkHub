import { Button, Stack } from "@mui/material";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

import TaskTable from "@/features/tasks/components/TaskTable";
import TaskDialog from "@/features/tasks/components/TaskDialog";
import DeleteTaskDialog from "@/features/tasks/components/DeleteTaskDialog";
import { useDeleteTask } from "@/features/tasks/hooks/useDeleteTask";
import { useTasks } from "@/features/tasks/hooks/useTasks";
import type { WorkTask } from "@/features/tasks/types/task";

type Props = {
    project: {
        id: string;
    };
};

export default function ProjectTasksTab({
    project
}: Props) {

    const navigate = useNavigate();
    const [open, setOpen] = useState(false);
    const [selectedTask, setSelectedTask] = useState<WorkTask | null>(null);
    const [taskToDelete, setTaskToDelete] = useState<WorkTask | null>(null);
    const deleteMutation = useDeleteTask();

    const {
        data: tasks = []
    } = useTasks({
        projectId: project.id
    });

    function handleView(task: WorkTask) {
        navigate(`/tasks/${task.id}`);
    }

    async function confirmDelete() {
        if (!taskToDelete) {
            return;
        }

        await deleteMutation.mutateAsync(taskToDelete.id);
        setTaskToDelete(null);
    }

    return (
        <>
            <Stack
                sx={{
                    display: "flex",
                    flexDirection: "row",
                    justifyContent: "flex-end",
                    mb: 2
                }}
            >
                <Button
                    variant="contained"
                    onClick={() => {
                        setSelectedTask(null);
                        setOpen(true);
                    }}
                >
                    New Task
                </Button>
            </Stack>

            <TaskTable
                tasks={tasks}
                onView={handleView}
                onEdit={(task) => {
                    setSelectedTask(task);
                    setOpen(true);
                }}
                onDelete={setTaskToDelete}
            />

            <TaskDialog
                open={open}
                projectId={project.id}
                task={selectedTask}
                onClose={() => setOpen(false)}
            />

            <DeleteTaskDialog
                open={!!taskToDelete}
                taskTitle={taskToDelete?.title ?? ""}
                loading={deleteMutation.isPending}
                onClose={() => setTaskToDelete(null)}
                onConfirm={confirmDelete}
            />
        </>
    );
}
