import { useQuery } from "@tanstack/react-query";
import { getKanbanBoard } from "../api/kanbanApi";

export function useKanban(projectId: string) {
  return useQuery({
    queryKey: ["kanban", projectId],

    queryFn: () => getKanbanBoard(projectId),

    enabled: !!projectId,
  });
}
