import { useMutation, useQueryClient } from "@tanstack/react-query";

import { assignTask } from "../api/tasksApi";

export function useAssignTask() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ id, userId }: { id: string; userId: string }) =>
      assignTask(id, userId),

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["tasks"],
      });

      queryClient.invalidateQueries({
        queryKey: ["task"],
      });
    },
  });
}
