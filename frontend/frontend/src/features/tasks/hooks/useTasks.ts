import { useQuery } from "@tanstack/react-query";
import { getTasks } from "../api/tasksApi";
import type { TaskQuery } from "../types/taskQuery";

export function useTasks(
    query: TaskQuery
) {
    return useQuery({
        queryKey: [
            "tasks",
            query
        ],
        queryFn: () =>
            getTasks(query)
    });
}
