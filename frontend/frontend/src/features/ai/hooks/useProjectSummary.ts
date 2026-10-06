import { useQuery } from "@tanstack/react-query";

import { getProjectSummary } from "../api/aiApi";

export function useProjectSummary(projectId?: string) {
    return useQuery({
        queryKey: ["ai-project-summary", projectId],
        queryFn: () => getProjectSummary(projectId as string),
        enabled: !!projectId,
        staleTime: 1000 * 60 * 5
    });
}
