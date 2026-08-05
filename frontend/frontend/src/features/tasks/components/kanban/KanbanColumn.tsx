import { Paper, Typography, Stack } from "@mui/material";

import {
  SortableContext,
  verticalListSortingStrategy,
} from "@dnd-kit/sortable";

import { useDroppable } from "@dnd-kit/core";

import KanbanTaskCard from "./KanbanTaskCard";

import type { KanbanColumn } from "../../types/kanban";

type Props = {
  column: KanbanColumn;
};

export default function KanbanColumn({ column }: Props) {
  const { setNodeRef } = useDroppable({
    id: column.status.toString(),
  });

  return (
    <Paper
      ref={setNodeRef}
      sx={{
        p: 2,
        minHeight: 650,
      }}
    >
      <Typography sx={{variant:"h6", mb:2}}>
        {column.title}({column.count})
      </Typography>

      <SortableContext
        items={column.tasks.map((x) => x.id)}
        strategy={verticalListSortingStrategy}
      >
        <Stack>
          {column.tasks.map((task) => (
            <KanbanTaskCard key={task.id} task={task} />
          ))}
        </Stack>
      </SortableContext>
    </Paper>
  );
}
