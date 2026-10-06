import { useState } from "react";

import {
  Card,
  CardContent,
  IconButton,
  Stack,
  Typography,
} from "@mui/material";

import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";

import type { TaskComment } from "../types/comment";
import CommentForm from "./CommentForm";

type Props = {
  comment: TaskComment;

  onDelete(id: string): void;
  onUpdate(id: string, content: string): void;
  updating?: boolean;
};

export default function CommentCard({
  comment,
  onDelete,
  onUpdate,
  updating = false,
}: Props) {
  const [isEditing, setIsEditing] = useState(false);

  function handleUpdate(content: string) {
    onUpdate(comment.id, content);
    setIsEditing(false);
  }

  return (
    <Card>
      <CardContent>
        <Stack
          direction="row"
          sx={{ justifyContent: "space-between", alignItems: "flex-start" }}
        >
          <div>
            <Typography sx={{ fontWeight: 600 }}>
              {comment.userName}
            </Typography>

            <Typography variant="body2" color="text.secondary">
              {new Date(comment.createdAtUtc).toLocaleString()}
              {comment.updatedAtUtc && " (edited)"}
            </Typography>
          </div>

          <Stack direction="row">
            <IconButton
              size="small"
              onClick={() => setIsEditing((prev) => !prev)}
            >
              <EditIcon fontSize="small" />
            </IconButton>

            <IconButton
              size="small"
              color="error"
              onClick={() => onDelete(comment.id)}
            >
              <DeleteIcon fontSize="small" />
            </IconButton>
          </Stack>
        </Stack>

        {isEditing ? (
          <CommentForm
            initialValue={comment.content}
            loading={updating}
            submitLabel="Save"
            onSubmit={handleUpdate}
            onCancel={() => setIsEditing(false)}
          />
        ) : (
          <Typography sx={{ mt: 2, whiteSpace: "pre-wrap" }}>
            {comment.content}
          </Typography>
        )}
      </CardContent>
    </Card>
  );
}
