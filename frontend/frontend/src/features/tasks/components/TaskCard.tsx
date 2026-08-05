import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import VisibilityIcon from "@mui/icons-material/Visibility";

import PushPinIcon from "@mui/icons-material/PushPin";
import PushPinOutlinedIcon from "@mui/icons-material/PushPinOutlined";

import Star from "@mui/icons-material/Star";
import StarBorder from "@mui/icons-material/StarBorder";

import {
  Button,
  Card,
  CardActions,
  CardContent,
  Chip,
  Stack,
  Typography,
  IconButton,
} from "@mui/material";

import { TaskPriorityInfo, WorkTaskStatusInfo } from "@/shared/constants/task";

import type { WorkTask } from "../types/task";

import { useToggleTaskFavorite } from "../hooks/useToggleTaskFavorite";
import { useToggleTaskPin } from "../hooks/useToggleTaskPin";

type Props = {
  task: WorkTask;
  onView(task: WorkTask): void;
  onEdit(task: WorkTask): void;
  onDelete(task: WorkTask): void;
};

export default function TaskCard({ task, onView, onEdit, onDelete }: Props) {
  const favoriteMutation = useToggleTaskFavorite();

  const pinMutation = useToggleTaskPin();

  return (
    <Card elevation={2}>
      <CardContent>
        <Typography variant="h6" gutterBottom>
          {task.title}
        </Typography>

        <Typography variant="body2" color="text.secondary">
          {task.description}
        </Typography>

        <Stack sx={{direction:"row", spacing:1, mt:2}}>
          <Chip
            label={WorkTaskStatusInfo[task.status].label}
            color={WorkTaskStatusInfo[task.status].color}
          />

          <Chip
            label={TaskPriorityInfo[task.priority].label}
            color={TaskPriorityInfo[task.priority].color}
          />
        </Stack>
      </CardContent>

      <CardActions>
        <Button startIcon={<VisibilityIcon />} onClick={() => onView(task)}>
          View
        </Button>

        <Button startIcon={<EditIcon />} onClick={() => onEdit(task)}>
          Edit
        </Button>

        <Button
          color="error"
          startIcon={<DeleteIcon />}
          onClick={() => onDelete(task)}
        >
          Delete
        </Button>

        <IconButton
          color="warning"
          onClick={() => favoriteMutation.mutate(task.id)}
        >
          {task.isFavorite ? <Star /> : <StarBorder />}
        </IconButton>

        <IconButton color="primary" onClick={() => pinMutation.mutate(task.id)}>
          {task.isPinned ? <PushPinIcon /> : <PushPinOutlinedIcon />}
        </IconButton>
      </CardActions>
    </Card>
  );
}
