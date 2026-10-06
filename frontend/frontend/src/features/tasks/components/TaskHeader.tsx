import { useState } from "react";

import { Button, Chip, Stack, Typography } from "@mui/material";

import type { WorkTask } from "../types/task";
import StarIcon from "@mui/icons-material/Star";
import StarBorderIcon from "@mui/icons-material/StarBorder";
import PushPinIcon from "@mui/icons-material/PushPin";
import PersonAddIcon from "@mui/icons-material/PersonAdd";

import IconButton from "@mui/material/IconButton";

import { useToggleTaskFavorite } from "../hooks/useToggleTaskFavorite";
import { useToggleTaskPin } from "../hooks/useToggleTaskPin";
import { useUserLookup } from "@/features/projects/hooks/useUserLookup";

import AssignTaskDialog from "./AssignTaskDialog";

type Props = {
  task: WorkTask;

  onEdit(): void;
};

export default function TaskHeader({ task, onEdit }: Props) {
  const favoriteMutation = useToggleTaskFavorite();
  const pinMutation = useToggleTaskPin();
  const { data: users = [] } = useUserLookup();

  const [assignOpen, setAssignOpen] = useState(false);

  return (
    <Stack
      direction="row"
      sx={{
        justifyContent: "space-between",
        alignItems: "flex-start",
        flexWrap: "wrap",
        gap: 2,
        mb: 3,
      }}
    >
      <div>
        <Typography variant="h4">{task.title}</Typography>

        <Typography color="text.secondary">{task.description}</Typography>

        <Chip
          size="small"
          sx={{ mt: 1 }}
          icon={<PersonAddIcon fontSize="small" />}
          label={task.assigneeName ?? "Unassigned"}
          onClick={() => setAssignOpen(true)}
          variant={task.assigneeName ? "filled" : "outlined"}
        />
      </div>

      <Stack direction="row" spacing={1}>
        <IconButton onClick={() => favoriteMutation.mutate(task.id)}>
          {task.isFavorite ? <StarIcon color="warning" /> : <StarBorderIcon />}
        </IconButton>

        <IconButton onClick={() => pinMutation.mutate(task.id)}>
          <PushPinIcon color={task.isPinned ? "primary" : "inherit"} />
        </IconButton>

        <Button
          variant="outlined"
          startIcon={<PersonAddIcon />}
          onClick={() => setAssignOpen(true)}
        >
          Assign
        </Button>

        <Button variant="contained" onClick={onEdit}>
          Edit
        </Button>
      </Stack>

      <AssignTaskDialog
        open={assignOpen}
        taskId={task.id}
        users={users}
        onClose={() => setAssignOpen(false)}
      />
    </Stack>
  );
}
