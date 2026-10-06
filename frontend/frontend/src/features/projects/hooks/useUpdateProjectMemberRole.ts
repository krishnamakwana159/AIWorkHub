import { useMutation, useQueryClient } from "@tanstack/react-query";

import { updateProjectMemberRole } from "../api/projectsApi";
import type { ProjectRole } from "@/shared/constants/project";

export function useUpdateProjectMemberRole(projectId: string) {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: ({ userId, role }: { userId: string; role: ProjectRole }) =>
            updateProjectMemberRole(projectId, userId, role),
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["project-members", projectId]
            });
        }
    });
}
