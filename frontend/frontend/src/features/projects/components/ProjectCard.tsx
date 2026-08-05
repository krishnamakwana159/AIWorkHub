import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import VisibilityIcon from "@mui/icons-material/Visibility";
import {
  ProjectPriorityInfo,
  ProjectStatusInfo
} from "@/shared/constants/project";

import {
  Button,
  Card,
  CardActions,
  CardContent,
  Chip,
  LinearProgress,
  Stack,
  Typography,
  IconButton
} from "@mui/material";
import Archive from "@mui/icons-material/Archive";
import Unarchive from "@mui/icons-material/Unarchive";
import type { Project } from "../types/project";
import { useToggleFavorite } from "../hooks/useToggleFavorite";
import { useToggleArchive } from "../hooks/useToggleArchive";

import Star from "@mui/icons-material/Star";
import StarBorder from "@mui/icons-material/StarBorder";

type Props = {
  project: Project;
  onView(project: Project): void;
  onEdit(project: Project): void;
  onDelete(project: Project): void;
};

export default function ProjectCard({
  project,
  onView,
  onEdit,
  onDelete
}: Props) {

  const favoriteMutation = useToggleFavorite();
  const archiveMutation = useToggleArchive();

  return (
    <Card elevation={2}>
      <CardContent>
        <Typography
          variant="h6"
          gutterBottom
        >
          {project.name}
        </Typography>

        <Typography
          variant="body2"
          color="text.secondary"
        >
          {project.description}
        </Typography>

        <Stack
          sx={{
            direction: "row",
            spacing: 1,
            mt: 2
          }}
        >
          <Chip
            label={
              ProjectStatusInfo[
                project.status
              ].label
            }
            color={
              ProjectStatusInfo[
                project.status
              ].color
            }
          />

          <Chip
            label={
              ProjectPriorityInfo[
                project.priority
              ].label
            }
            color={
              ProjectPriorityInfo[
                project.priority
              ].color
            }
          />
        </Stack>

        <Typography
          sx={{
            mt: 3,
            mb: 1,
            variant: "body2"
          }}
        >
          Progress {project.progress}%
        </Typography>

        <LinearProgress
          variant="determinate"
          value={project.progress}
        />
      </CardContent>

      <CardActions>
        <Button
          startIcon={<VisibilityIcon />}
          onClick={() => onView(project)}
        >
          View
        </Button>

        <Button
          startIcon={<EditIcon />}
          onClick={() => onEdit(project)}
        >
          Edit
        </Button>

        <Button
          color="error"
          startIcon={<DeleteIcon />}
          onClick={() => onDelete(project)}
        >
          Delete
        </Button>
        <IconButton
            color="warning"
            onClick={() =>
                favoriteMutation.mutate(project.id)
            }
        >
            {project.isFavorite
                ? <Star />
                : <StarBorder />}
        </IconButton>
        <IconButton
            color={
                project.isArchived
                    ? "success"
                    : "default"
            }
            onClick={() =>
                archiveMutation.mutate(project.id)
            }
        >
            {project.isArchived
                ? <Unarchive />
                : <Archive />}
        </IconButton>
      </CardActions>
    </Card>
  );
}
