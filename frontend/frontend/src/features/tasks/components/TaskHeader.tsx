import { Button, Stack, Typography } from "@mui/material";

import type { WorkTask } from "../types/task";
import StarIcon from "@mui/icons-material/Star";
import StarBorderIcon from "@mui/icons-material/StarBorder";
import PushPinIcon from "@mui/icons-material/PushPin";

import IconButton from "@mui/material/IconButton";

import { useToggleTaskFavorite } from "../hooks/useToggleTaskFavorite";
import { useToggleTaskPin } from "../hooks/useToggleTaskPin";

type Props = {
  task: WorkTask;

  onEdit(): void;
};

export default function TaskHeader({ task, onEdit }: Props) {
  const favoriteMutation = useToggleTaskFavorite();
  const pinMutation = useToggleTaskPin();

  return (
    <Stack
      sx={{
        direction: "row",
        justifyContent: "space-between",
        alignItems: "center",
        mb: 3,
      }}
    >
      <div>
        <Typography variant="h4">{task.title}</Typography>

        <Typography color="text.secondary">{task.description}</Typography>
      </div>

      <Stack direction="row" spacing={1}>
        <IconButton onClick={() => favoriteMutation.mutate(task.id)}>
          {task.isFavorite ? <StarIcon color="warning" /> : <StarBorderIcon />}
        </IconButton>

        <IconButton onClick={() => pinMutation.mutate(task.id)}>
          <PushPinIcon color={task.isPinned ? "primary" : "inherit"} />
        </IconButton>

        <Button variant="contained" onClick={onEdit}>
          Edit
        </Button>
      </Stack>
    </Stack>
  );
}
