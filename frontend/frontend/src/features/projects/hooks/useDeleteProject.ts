import {
    useMutation,
    useQueryClient
} from "@tanstack/react-query";

import { deleteProject } from "../api/projectsApi";
import { useAppSnackbar } from "@/shared/hooks/useAppSnackbar";

export function useDeleteProject() {

    const queryClient = useQueryClient();
    const snackbar = useAppSnackbar();

    return useMutation({
        mutationFn: deleteProject,
        onSuccess: () => {
            snackbar.success(
                "Project deleted successfully."
            );
            queryClient.invalidateQueries({
                queryKey: ["projects"]
            });
        },

        onError: () => {
            snackbar.error(
                "Unable to delete project."
            );
        }
    });
}
