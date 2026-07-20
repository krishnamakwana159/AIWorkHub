import { useQuery } from "@tanstack/react-query";
import { getProjects } from "../api/projectsApi";
import type { ProjectQuery } from "../types/projectQuery";

export function useProjects(query: ProjectQuery) {
    return useQuery({
        queryKey: ["projects", query],
        queryFn: () => getProjects(query)
    });
}
