import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  MenuItem,
  TextField,
} from "@mui/material";

import { useState } from "react";

import { useAssignTask } from "../hooks/useAssignTask";

type User = {
  id: string;
  fullName: string;
};

type Props = {
  open: boolean;
  taskId: string;
  users: User[];
  onClose(): void;
};

export default function AssignTaskDialog({
  open,
  taskId,
  users,
  onClose,
}: Props) {
  const [userId, setUserId] = useState("");
  const mutation = useAssignTask();

  async function assign() {
    if (!userId) return;

    await mutation.mutateAsync({
      id: taskId,
      userId,
    });
    onClose();
  }

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="xs">
      <DialogTitle>Assign Task</DialogTitle>

      <DialogContent>
        <TextField
          fullWidth
          select
          margin="normal"
          label="User"
          value={userId}
          onChange={(e) => setUserId(e.target.value)}
        >
          {users.map((user) => (
            <MenuItem key={user.id} value={user.id}>
              {user.fullName}
            </MenuItem>
          ))}
        </TextField>
      </DialogContent>

      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>

        <Button variant="contained" onClick={assign}>
          Assign
        </Button>
      </DialogActions>
    </Dialog>
  );
}
