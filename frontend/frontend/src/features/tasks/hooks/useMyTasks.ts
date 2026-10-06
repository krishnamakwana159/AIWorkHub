import { useQuery } from "@tanstack/react-query";

import { getMyTasks } from "../api/tasksApi";

export function useMyTasks() {
    return useQuery({
        queryKey: ["my-tasks"],
        queryFn: getMyTasks
    });
}
