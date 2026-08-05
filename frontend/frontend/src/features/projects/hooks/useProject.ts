import { useQuery } from "@tanstack/react-query";

import { getProject } from "../api/projectsApi";

export function useProject(id: string) {
    return useQuery({
        queryKey: ["project", id],
        queryFn: () => getProject(id),
        enabled: !!id
    });
}
