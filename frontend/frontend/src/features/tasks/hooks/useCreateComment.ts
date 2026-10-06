import { useMutation, useQueryClient } from "@tanstack/react-query";

import { createComment } from "../api/tasksApi";
import type { CreateCommentRequest } from "../types/comment";

export function useCreateComment() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({
      taskId,
      request,
    }: {
      taskId: string;
      request: CreateCommentRequest;
    }) => createComment(taskId, request),

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["task-comments"],
      });
    },
  });
}
