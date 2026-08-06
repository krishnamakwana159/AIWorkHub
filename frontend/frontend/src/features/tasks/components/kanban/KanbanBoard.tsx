import Grid from "@mui/material/Grid";
import { DndContext, DragOverlay } from "@dnd-kit/core";
import type { DragEndEvent } from '@dnd-kit/core';
import { useState } from "react";
import KanbanColumn from "./KanbanColumn";
import type { KanbanBoardResponse, KanbanTask } from "../../types/kanban";
import { useMoveTask } from "../../hooks/useMoveTask";
import KanbanTaskCard from "./KanbanTaskCard";

type Props = {
  board: KanbanBoardResponse;
};

export default function KanbanBoard({ board }: Props) {
  const [columns, setColumns] = useState(board.columns);

  const moveTask = useMoveTask();
  const [activeTask, setActiveTask] = useState<KanbanTask | null>(null);

  async function handleDragEnd(event: DragEndEvent) {
    const { active, over } = event;

    if (!over) return;

    let sourceColumnIndex = -1;
    let sourceTaskIndex = -1;

    let destinationColumnIndex = -1;
    let destinationTaskIndex = 0;

    for (let c = 0; c < columns.length; c++) {
      const taskIndex = columns[c].tasks.findIndex((x) => x.id === active.id);

      if (taskIndex >= 0) {
        sourceColumnIndex = c;
        sourceTaskIndex = taskIndex;
      }

      const overTaskIndex = columns[c].tasks.findIndex((x) => x.id === over.id);

      if (overTaskIndex >= 0) {
        destinationColumnIndex = c;
        destinationTaskIndex = overTaskIndex;
      }

      if (columns[c].status.toString() === over.id) {
        destinationColumnIndex = c;

        destinationTaskIndex = columns[c].tasks.length;
      }
    }

    if (sourceColumnIndex < 0) return;

    const updated = structuredClone(columns);

    const [task] = updated[sourceColumnIndex].tasks.splice(sourceTaskIndex, 1);

    task.status = updated[destinationColumnIndex].status;

    updated[destinationColumnIndex].tasks.splice(destinationTaskIndex, 0, task);

    updated.forEach((column) => {
      column.count = column.tasks.length;

      column.tasks.forEach((task, index) => (task.order = index));
    });

    setColumns(updated);

    await moveTask.mutateAsync({
      taskId: task.id,

      request: {
        projectId: board.projectId,

        status: task.status,

        order: task.order,
      },
    });
  }

  return (
    <DndContext
      onDragStart={(event) => {
        for (const column of columns) {
          const task = column.tasks.find((x) => x.id === event.active.id);

          if (task) {
            setActiveTask(task);

            break;
          }
        }
      }}
      onDragCancel={() => setActiveTask(null)}
      onDragEnd={(event) => {
        setActiveTask(null);

        handleDragEnd(event);
      }}
    >
      <Grid container spacing={2}>
        {columns.map((column) => (
          <Grid
            key={column.status}
            size={{
              xs: 12,
              md: 3,
            }}
          >
            <KanbanColumn column={column} />
          </Grid>
        ))}
      </Grid>
      <DragOverlay>
        {activeTask && <KanbanTaskCard task={activeTask} />}
      </DragOverlay>
    </DndContext>
  );
}
