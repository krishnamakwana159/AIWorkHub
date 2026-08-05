import { useMutation, useQueryClient } from "@tanstack/react-query";

import { toggleFavorite } from "../api/projectsApi";
import { useAppSnackbar } from "@/shared/hooks/useAppSnackbar";

export function useToggleFavorite() {
  const queryClient = useQueryClient();

  const snackbar = useAppSnackbar();

  type Project = {
    id: string | number;
    isFavorite: boolean;
  };

  return useMutation({
    mutationFn: toggleFavorite,

    onMutate: async (projectId) => {
      await queryClient.cancelQueries({
        queryKey: ["projects"],
      });

      const previous = queryClient.getQueryData<Project[] | undefined>(["projects"]);

      queryClient.setQueriesData(
        {
          queryKey: ["projects"],
        },
        (old: Project[] | undefined) => {
          if (!old) {
            return old;
          }

          return old.map((project: Project) =>
            project.id === projectId
              ? {
                  ...project,
                  isFavorite: !project.isFavorite,
                }
              : project,
          );
        },
      );

      return {
        previous,
      };
    },

    onError: (_, __, context) => {
      if (context?.previous) {
        queryClient.setQueryData(["projects"], context.previous);
      }

      snackbar.error("Unable to update favorite.");
    },

    onSuccess: () => {
      snackbar.success("Favorite updated.");
    },

    onSettled: () => {
      queryClient.invalidateQueries({
        queryKey: ["projects"],
      });
    },
  });
}
