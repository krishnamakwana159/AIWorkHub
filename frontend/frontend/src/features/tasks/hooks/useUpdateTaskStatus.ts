import { useMutation, useQueryClient } from "@tanstack/react-query";

import { updateStatus } from "../api/tasksApi";

export function useUpdateTaskStatus() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({
      id,
      status,
    }: {
      id: string;

      status: number;
    }) => updateStatus(id, status),

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["tasks"],
      });
    },
  });
}
