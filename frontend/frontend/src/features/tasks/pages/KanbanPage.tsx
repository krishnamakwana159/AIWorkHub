import { useParams } from "react-router-dom";

import AppLoader from "@/components/ui/AppLoader";
import EmptyState from "@/components/ui/EmptyState";
import PageContainer from "@/components/common/PageContainer";

import KanbanBoard from "../components/kanban/KanbanBoard";
import { useKanban } from "../hooks/useKanban";

export default function KanbanPage() {
  const { id } = useParams();

  const { data, isPending, isError } = useKanban(id!);

  if (isPending) return <AppLoader />;

  if (isError || !data) return <EmptyState message="Unable to load board." />;

  return (
    <PageContainer>
      <KanbanBoard board={data} />
    </PageContainer>
  );
}
