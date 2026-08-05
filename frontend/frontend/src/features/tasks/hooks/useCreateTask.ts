import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createTask } from "../api/tasksApi";
import { useAppSnackbar } from "@/shared/hooks/useAppSnackbar";

export function useCreateTask() {
  const snackbar = useAppSnackbar();

  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createTask,

    onSuccess: () => {
      snackbar.success("Task created successfully.");

      queryClient.invalidateQueries({
        queryKey: ["tasks"],
      });
    },

    onError: () => {
      snackbar.error("Unable to create task.");
    },
  });
}
