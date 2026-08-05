import { useQuery } from "@tanstack/react-query";
import { getAttachments } from "../api/tasksApi";

export function useAttachments(
    taskId: string
) {
    return useQuery({
        queryKey: [
            "task-attachments",
            taskId
        ],
        queryFn: () =>
            getAttachments(taskId),
        enabled: !!taskId
    });

}
