import { useMutation, useQueryClient } from "@tanstack/react-query";

import { addProjectMember } from "../api/projectsApi";
import type { AddProjectMemberRequest } from "../types/project";

export function useAddProjectMember(projectId: string) {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: (request: AddProjectMemberRequest) =>
            addProjectMember(projectId, request),
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ["project-members", projectId]
            });
        }
    });
}
