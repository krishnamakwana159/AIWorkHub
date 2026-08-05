import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteTask } from "../api/tasksApi";
import { useAppSnackbar } from "@/shared/hooks/useAppSnackbar";

export function useDeleteTask() {
  const snackbar = useAppSnackbar();

  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteTask,
    
    onSuccess: () => {
      snackbar.success("Task deleted successfully.");

      queryClient.invalidateQueries({
        queryKey: ["tasks"],
      });
    },

    onError: () => {
      snackbar.error("Unable to delete task.");
    },
  });
}
