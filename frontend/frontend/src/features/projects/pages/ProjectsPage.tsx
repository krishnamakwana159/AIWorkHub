import { useState, useEffect } from "react";
import Button from "@mui/material/Button";
import Grid from "@mui/material/Grid";

import PageContainer from "@/components/common/PageContainer";
import PageTitle from "@/components/common/PageTitle";

import AppLoader from "@/components/ui/AppLoader";
import EmptyState from "@/components/ui/EmptyState";
import SearchInput from "@/components/ui/SearchInput";

import ProjectDialog from "../components/ProjectDialog";
import ProjectTable from "../components/ProjectTable";
import { useProjects } from "../hooks/useProjects";

import type { Project } from "../types/project";
import { useDeleteProject } from "../hooks/useDeleteProject";

import { useNavigate, useSearchParams } from 'react-router-dom'
import ConfirmDialog from "@/shared/components/ConfirmDialog";

export default function ProjectsPage() {
  const navigate = useNavigate();
  const [searchParams, setSearchParams] = useSearchParams();
  const [search, setSearch] = useState("");

  const [dialogOpen, setDialogOpen] =
    useState(() => searchParams.get("new") === "1");

  const [selectedProject, setSelectedProject] =
    useState<Project | null>(null);

  useEffect(() => {
    if (searchParams.get("new") === "1") {
      const next = new URLSearchParams(searchParams);
      next.delete("new");
      setSearchParams(next, { replace: true });
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const deleteMutation = useDeleteProject();

  const [deleteOpen, setDeleteOpen] =
      useState(false);

  const [projectToDelete, setProjectToDelete] =
      useState<Project | null>(null);

  function handleDelete(project: Project) {
      setProjectToDelete(project);
      setDeleteOpen(true);
  }

  const {
    data,
    isPending,
    isError
  } = useProjects({
    search,
    page: 1,
    pageSize: 20
  });

  function handleCreate() {
    setSelectedProject(null);
    setDialogOpen(true);
  }

  function handleEdit(project: Project) {
    console.log("Project edit:")
    setSelectedProject(project);
    setDialogOpen(true);
  }

  function handleView(project: Project) {
    navigate(`/projects/${project.id}`);
  }
  async function confirmDelete() {
      if (!projectToDelete) {
          return;
      }

      await deleteMutation.mutateAsync(
          projectToDelete.id
      );

      setDeleteOpen(false);

      setProjectToDelete(null);
  }

  return (
    <PageContainer>
      <PageTitle
        title="Projects"
        subtitle="Manage your projects"
      />

      <Grid
        container
        spacing={3}
        sx={{ mt: 1 }}
      >
        <Grid size={12}>
          <Button
            variant="contained"
            onClick={handleCreate}
          >
            New Project
          </Button>
        </Grid>

        <Grid size={12}>
          <SearchInput
            value={search}
            onChange={setSearch}
          />
        </Grid>

        <Grid size={12}>
          {isPending && <AppLoader />}

          {isError && (
            <EmptyState message="Unable to load projects." />
          )}

          {!isPending && !isError && data && (
            <ProjectTable
                projects={data}
                onView={handleView}
                onEdit={handleEdit}
                onDelete={handleDelete}
            />
          )}
        </Grid>
      </Grid>

      <ProjectDialog
        open={dialogOpen}
        project={selectedProject}
        onClose={() => {
          setDialogOpen(false);
          setSelectedProject(null);
        }}
      />

      <ConfirmDialog
          open={deleteOpen}
          title="Delete Project"
          message={`Delete "${projectToDelete?.name}" ?`}
          loading={deleteMutation.isPending}
          onClose={() => setDeleteOpen(false)}
          onConfirm={confirmDelete}
      />
    </PageContainer>
  );
}
