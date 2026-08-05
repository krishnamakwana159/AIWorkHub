import { Card, CardContent, Chip, Typography } from "@mui/material";

import { useSortable } from "@dnd-kit/sortable";

import { CSS } from "@dnd-kit/utilities";

import { TaskPriorityInfo } from "@/shared/constants/task";

import type { KanbanTask } from "../../types/kanban";

type Props = {
  task: KanbanTask;
};

export default function KanbanTaskCard({ task }: Props) {
  const {
    attributes,
    listeners,
    setNodeRef,
    transform,
    transition,
    isDragging,
  } = useSortable({
    id: task.id,
  });

  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
    opacity: isDragging ? 0.35 : 1,
  };

  return (
    <Card
      ref={setNodeRef}
      style={style}
      {...attributes}
      {...listeners}
      sx={{
        mb: 2,
        cursor: "grab",
      }}
    >
      <CardContent>
        <Typography sx={{variant:"subtitle1", fontWeight:600}}>
          {task.title}
        </Typography>

        <Chip
          size="small"
          sx={{ mt: 1 }}
          label={TaskPriorityInfo[task.priority].label}
        />
      </CardContent>
    </Card>
  );
}
