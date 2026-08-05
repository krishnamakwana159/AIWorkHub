import { useParams } from "react-router-dom";

import Grid from "@mui/material/Grid";

import AppLoader from "@/components/ui/AppLoader";
import EmptyState from "@/components/ui/EmptyState";
import PageContainer from "@/components/common/PageContainer";

import { useProject } from "../hooks/useProject";

import ProjectHeader from "../components/ProjectHeader";
import ProjectOverviewCard from "../components/ProjectOverviewCard";
import ProjectStatisticsCard from "../components/ProjectStatisticsCard";
import { useToggleFavorite } from "../hooks/useToggleFavorite";
import { useToggleArchive } from "../hooks/useToggleArchive";
import ProjectDialog from "../components/ProjectDialog";
import { useState } from "react";
import ProjectTabs from "../components/ProjectTabs";
import ProjectTasksTab from "../components/ProjectTasksTab";

export default function ProjectLayout() {
  const { id } = useParams();
  const [editOpen, setEditOpen] = useState(false);
  const favoriteMutation = useToggleFavorite();
  const archiveMutation = useToggleArchive();
  const [tab, setTab] = useState(0);

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
      <ProjectTabs value={tab} onChange={setTab} />

      {tab === 0 && (
        <Grid container spacing={3}>
          <Grid size={12}>
            <ProjectOverviewCard project={project} />
          </Grid>

          <Grid size={12}>
            <ProjectStatisticsCard project={project} />
          </Grid>
        </Grid>
      )}

      {tab === 1 && <ProjectTasksTab project={project} />}

      {tab === 2 && <>Members Coming Soon</>}

      {tab === 3 && <>Activity Coming Soon</>}

      <ProjectDialog
        open={editOpen}
        project={project}
        onClose={() => setEditOpen(false)}
      />
    </PageContainer>
  );
}
