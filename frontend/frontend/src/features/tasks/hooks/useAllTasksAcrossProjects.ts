import { useQuery } from "@tanstack/react-query";

import { getAllTasksAcrossProjects } from "../api/tasksApi";

export function useAllTasksAcrossProjects() {
    return useQuery({
        queryKey: ["all-tasks-across-projects"],
        queryFn: getAllTasksAcrossProjects
    });
}
