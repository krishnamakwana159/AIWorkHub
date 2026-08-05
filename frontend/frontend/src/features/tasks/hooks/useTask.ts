import { useQuery } from "@tanstack/react-query";

import { getTask } from "../api/tasksApi";

export function useTask(id: string) {
  return useQuery({
    queryKey: ["task", id],

    queryFn: () => getTask(id),

    enabled: !!id,
  });
}
