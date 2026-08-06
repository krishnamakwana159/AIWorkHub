import { Outlet, useParams } from "react-router-dom";

import AppLoader from "@/components/ui/AppLoader";
import EmptyState from "@/components/ui/EmptyState";
import PageContainer from "@/components/common/PageContainer";

import { useProject } from "../hooks/useProject";

import ProjectHeader from "../components/ProjectHeader";
import { useToggleFavorite } from "../hooks/useToggleFavorite";
import { useToggleArchive } from "../hooks/useToggleArchive";
import ProjectDialog from "../components/ProjectDialog";
import { useState } from "react";
import ProjectTabs from "../components/ProjectTabs";
import type { Project } from "../types/project";

export type ProjectOutletContext = {
  project: Project;
};

export default function ProjectLayout() {
  const { id } = useParams();
  const [editOpen, setEditOpen] = useState(false);
  const favoriteMutation = useToggleFavorite();
  const archiveMutation = useToggleArchive();

  const { data: project, isPending, isError } = useProject(id!);

  if (isPending) {
    return <AppLoader />;
  }

  if (isError || !project) {
    return <EmptyState message="Project not found." />;
  }

  return (
    <PageContainer>
      <ProjectHeader
        project={project}
        onEdit={() => setEditOpen(true)}
        onToggleFavorite={() => favoriteMutation.mutate(project.id)}
        onToggleArchive={() => archiveMutation.mutate(project.id)}
      />
      <ProjectTabs projectId={project.id} />
      <Outlet context={{ project } satisfies ProjectOutletContext} />

      <ProjectDialog
        open={editOpen}
        project={project}
        onClose={() => setEditOpen(false)}
      />
    </PageContainer>
  );
}
