import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateTask } from "../api/tasksApi";
import { useAppSnackbar } from "@/shared/hooks/useAppSnackbar";
import type { UpdateTaskRequest } from "../types/task";

export function useUpdateTask() {
  const snackbar = useAppSnackbar();
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({
      id,
      request,
    }: {
      id: string;
      request: UpdateTaskRequest;
    }) => updateTask(id, request),

    onSuccess: () => {
      snackbar.success("Task updated successfully.");

      queryClient.invalidateQueries({
        queryKey: ["tasks"],
      });
    },

    onError: () => {
      snackbar.error("Unable to update task.");
    },
  });
}
