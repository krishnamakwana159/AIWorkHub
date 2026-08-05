import { Button, Stack } from "@mui/material";
import { useState } from "react";

import TaskTable from "@/features/tasks/components/TaskTable";
import TaskDialog from "@/features/tasks/components/TaskDialog";
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

    const [open, setOpen] = useState(false);
    const [selectedTask, setSelectedTask] = useState<WorkTask | null>(null);

    const {
        data: tasks = []
    } = useTasks({
        projectId: project.id
    });

    function handleView(task: WorkTask) {
        console.log(task);
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
                onDelete={() => {
                    // TODO
                }}
            />

            <TaskDialog
                open={open}
                projectId={project.id}
                task={selectedTask}
                onClose={() => setOpen(false)}
            />
        </>
    );
}
