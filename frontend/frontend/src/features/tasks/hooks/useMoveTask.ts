import { useMutation, useQueryClient } from "@tanstack/react-query";
import { moveTask } from "../api/kanbanApi";
import type { MoveTaskRequest } from "../types/kanban";

export function useMoveTask() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({
      taskId,
      request,
    }: {
      taskId: string;
      request: MoveTaskRequest;
    }) => moveTask(taskId, request),

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["kanban"],
      });
    },
  });
}
