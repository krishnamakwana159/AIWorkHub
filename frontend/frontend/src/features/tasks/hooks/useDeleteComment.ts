import { useMutation, useQueryClient } from "@tanstack/react-query";

import { deleteComment } from "../api/tasksApi";

export function useDeleteComment() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteComment,

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["task-comments"],
      });
    },
  });
}
