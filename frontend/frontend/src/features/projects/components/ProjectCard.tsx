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
  Typography
} from "@mui/material";

import type { Project } from "../types/project";

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
      </CardActions>
    </Card>
  );
}
