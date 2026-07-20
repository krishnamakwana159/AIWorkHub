import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle
} from "@mui/material";

import ProjectForm from "./ProjectForm";
import { useCreateProject } from "../hooks/useCreateProject";
import { useUpdateProject } from "../hooks/useUpdateProject";

import type { Project } from "../types/project";
import type { ProjectFormValues } from "../schema/projectSchema";
import { ProjectPriority } from "@/shared/constants/project";

type Props = {
  open: boolean;
  project?: Project | null;
  onClose(): void;
};

const defaultForm: ProjectFormValues = {
  name: "",
  description: "",
  color: "#2563EB",
  priority: ProjectPriority.Low
};

export default function ProjectDialog({
  open,
  project,
  onClose
}: Props) {
  const createMutation = useCreateProject();
  const updateMutation = useUpdateProject();

  const defaultValues: ProjectFormValues = project
    ? {
        name: project.name,
        description: project.description ?? "",
        color: project.color,
        priority: project.priority
      }
    : defaultForm;

  async function handleSubmit(values: ProjectFormValues) {
    console.log("handleSubmit: dialog...")
    if (project) {
      console.log("Dialog project:", project)
      await updateMutation.mutateAsync({
        id: project.id,
        request: {
          ...values,
          status: project.status,
          isFavorite: project.isFavorite,
          startDateUtc: project.startDateUtc,
          targetCompletionDateUtc: project.targetCompletionDateUtc
        }
      });
    } else {
      await createMutation.mutateAsync(values);
    }

    onClose();
  }

  return (
    <Dialog
      open={open}
      onClose={onClose}
      fullWidth
      maxWidth="sm"
    >
      <DialogTitle>
        {project ? "Edit Project" : "Create Project"}
      </DialogTitle>

      <DialogContent sx={{ pt: 2 }}>
        <ProjectForm
          defaultValues={defaultValues}
          onSubmit={handleSubmit}
        />
      </DialogContent>

      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>

        <Button
          type="submit"
          form="project-form"
          variant="contained"
          disabled={
            createMutation.isPending ||
            updateMutation.isPending
          }
        >
          {project ? "Update" : "Create"}
        </Button>
      </DialogActions>
    </Dialog>
  );
}
