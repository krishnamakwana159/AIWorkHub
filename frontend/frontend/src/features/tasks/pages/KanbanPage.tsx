import { useOutletContext } from "react-router-dom";

import AppLoader from "@/components/ui/AppLoader";
import EmptyState from "@/components/ui/EmptyState";

import KanbanBoard from "../components/kanban/KanbanBoard";
import { useKanban } from "../hooks/useKanban";
import type { ProjectOutletContext } from "@/features/projects/pages/ProjectLayout";

export default function KanbanPage() {
  const { project } = useOutletContext<ProjectOutletContext>();

  const { data, isPending, isError } = useKanban(project.id);

  if (isPending) return <AppLoader />;

  if (isError || !data) return <EmptyState message="Unable to load board." />;

  return <KanbanBoard board={data} />;
}
