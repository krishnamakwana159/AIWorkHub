import {
    useMutation,
    useQueryClient
} from "@tanstack/react-query";
import { updateProject } from "../api/projectsApi";
import type { UpdateProjectRequest } from "../types/project";

export function useUpdateProject() {

    const queryClient =
        useQueryClient();

    return useMutation({

        mutationFn: ({
            id,
            request
        }: {
            id: string;
            request: UpdateProjectRequest;
        }) => updateProject(id, request),
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["projects"]
            });
        }
    });

}
