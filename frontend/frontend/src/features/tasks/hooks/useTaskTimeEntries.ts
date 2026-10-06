import { useQuery } from "@tanstack/react-query";

import { getTaskTimeEntries } from "../api/timeApi";

export function useTaskTimeEntries(taskId: string) {
    return useQuery({
        queryKey: ["task-time-entries", taskId],
        queryFn: () => getTaskTimeEntries(taskId),
        enabled: !!taskId
    });
}
