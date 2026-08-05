import { useQuery } from "@tanstack/react-query";

import { getComments } from "../api/tasksApi";

export function useComments(taskId: string) {
  return useQuery({
    queryKey: ["task-comments", taskId],

    queryFn: () => getComments(taskId),

    enabled: !!taskId,
  });
}
