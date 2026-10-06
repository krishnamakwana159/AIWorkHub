import { useQuery } from "@tanstack/react-query";

import { getProjectReport } from "../api/reportsApi";

export function useProjectReport(projectId?: string) {
    return useQuery({
        queryKey: ["project-report", projectId],
        queryFn: () => getProjectReport(projectId as string),
        enabled: !!projectId
    });
}
