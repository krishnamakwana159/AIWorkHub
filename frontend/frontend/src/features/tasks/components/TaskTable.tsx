import Grid from "@mui/material/Grid";

import TaskCard from "./TaskCard";

import type { WorkTask } from "../types/task";

type Props = {
  tasks: WorkTask[];
  onView(task: WorkTask): void;
  onEdit(task: WorkTask): void;
  onDelete(task: WorkTask): void;
};

export default function TaskTable({ tasks, onView, onEdit, onDelete }: Props) {
  console.log(tasks)
  return (
    <Grid container spacing={3}>
      {tasks.map((task) => (
        <Grid
          key={task.id}
          size={{
            xs: 12,
            sm: 6,
            lg: 4,
          }}
        >
          <TaskCard
            task={task}
            onView={onView}
            onEdit={onEdit}
            onDelete={onDelete}
          />
        </Grid>
      ))}
    </Grid>
  );
}
