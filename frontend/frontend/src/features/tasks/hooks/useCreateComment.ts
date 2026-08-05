import { useMutation, useQueryClient } from "@tanstack/react-query";

import { createComment } from "../api/tasksApi";

export function useCreateComment() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ taskId, request }: any) => createComment(taskId, request),

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["task-comments"],
      });
    },
  });
}
