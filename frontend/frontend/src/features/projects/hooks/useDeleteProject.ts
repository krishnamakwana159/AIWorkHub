import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";

import { deleteProject } from "../api/projectsApi";

export function useDeleteProject() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: deleteProject,

        onSuccess: () => {
            toast.success("Project deleted.");

            queryClient.invalidateQueries({
                queryKey: ["projects"]
            });
        },

        onError: () => {
            toast.error("Unable to delete project.");
        }
    });
}
