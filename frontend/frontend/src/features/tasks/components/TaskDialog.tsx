import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
} from "@mui/material";

import TaskForm from "./TaskForm";

import { useCreateTask } from "../hooks/useCreateTask";
import { useUpdateTask } from "../hooks/useUpdateTask";

import type { WorkTask } from "../types/task";
import type { TaskFormValues } from "../schema/taskSchema";

import { TaskPriority } from "@/shared/constants/task";

type Props = {
  open: boolean;
  projectId: string;
  task?: WorkTask | null;
  onClose(): void;
};

const defaultForm: TaskFormValues = {
  title: "",
  description: "",
  priority: TaskPriority.Medium,
  estimatedHours: 0,
  startDateUtc: "",
  dueDateUtc: "",
};

export default function TaskDialog({ open, projectId, task, onClose }: Props) {
  const createMutation = useCreateTask();

  const updateMutation = useUpdateTask();

  const defaultValues = task
    ? {
        title: task.title,
        description: task.description ?? "",
        priority: task.priority,
        estimatedHours: task.estimatedHours,
        startDateUtc: task.startDateUtc ?? "",
        dueDateUtc: task.dueDateUtc ?? "",
      }
    : defaultForm;

  async function handleSubmit(values: TaskFormValues) {
    if (task) {
      await updateMutation.mutateAsync({
        id: task.id,
        request: {
            ...values,
            priority: values.priority as TaskPriority,
            status: task.status,
            actualHours: task.actualHours,
        },
      });
    } else {
      await createMutation.mutateAsync({
        ...values,
        priority: values.priority as TaskPriority,
        projectId,
      });
    }
    onClose();
  }

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>{task ? "Edit Task" : "Create Task"}</DialogTitle>

      <DialogContent sx={{ pt: 2 }}>
        <TaskForm defaultValues={defaultValues} onSubmit={handleSubmit} />
      </DialogContent>

      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>

        <Button
          type="submit"
          form="task-form"
          variant="contained"
          disabled={createMutation.isPending || updateMutation.isPending}
        >
          {task ? "Update" : "Create"}
        </Button>
      </DialogActions>
    </Dialog>
  );
}
