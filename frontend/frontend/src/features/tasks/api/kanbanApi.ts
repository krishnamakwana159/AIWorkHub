import apiClient from "@/shared/api/apiClient";
import type { KanbanBoardResponse, MoveTaskRequest } from "../types/kanban";

export async function getKanbanBoard(projectId: string) {
  const { data } = await apiClient.get<KanbanBoardResponse>("/tasks/kanban", {
    params: {
      projectId,
    },
  });

  return data;
}

export async function moveTask(taskId: string, request: MoveTaskRequest) {
  await apiClient.patch(`/tasks/${taskId}/move`, request);
}
