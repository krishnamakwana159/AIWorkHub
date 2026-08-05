import { useMutation, useQueryClient } from "@tanstack/react-query";

import { toggleArchive } from "../api/projectsApi";
import { useAppSnackbar } from "@/shared/hooks/useAppSnackbar";

export function useToggleArchive() {
  const queryClient = useQueryClient();

  const snackbar = useAppSnackbar();

  return useMutation({
    mutationFn: toggleArchive,

    onSuccess: () => {
      snackbar.success("Project updated.");

      queryClient.invalidateQueries({
        queryKey: ["projects"],
      });
    },

    onError: () => {
      snackbar.error("Unable to update project.");
    },
  });
}
