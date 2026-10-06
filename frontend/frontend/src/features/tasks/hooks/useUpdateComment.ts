import { useMutation, useQueryClient } from "@tanstack/react-query";

import { updateComment } from "../api/tasksApi";
import type { UpdateCommentRequest } from "../types/comment";

export function useUpdateComment() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({
      commentId,
      request,
    }: {
      commentId: string;
      request: UpdateCommentRequest;
    }) => updateComment(commentId, request),

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["task-comments"],
      });
    },
  });
}
