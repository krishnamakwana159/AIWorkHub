import {
  Card,
  CardContent,
  IconButton,
  Stack,
  Typography,
} from "@mui/material";

import DeleteIcon from "@mui/icons-material/Delete";

import type { TaskComment } from "../types/comment";

type Props = {
  comment: TaskComment;

  onDelete(id: string): void;
};

export default function CommentCard({
  comment,

  onDelete,
}: Props) {
  return (
    <Card>
      <CardContent>
        <Stack sx={{direction:"row", justifyContent:"space-between"}}>
          <div>
            <Typography sx={{fontWeight:600}}>{comment.userName}</Typography>

            <Typography variant="body2" color="text.secondary">
              {new Date(comment.createdAtUtc).toLocaleString()}
            </Typography>
          </div>

          <IconButton color="error" onClick={() => onDelete(comment.id)}>
            <DeleteIcon />
          </IconButton>
        </Stack>

        <Typography sx={{ mt: 2 }}>{comment.content}</Typography>
      </CardContent>
    </Card>
  );
}
