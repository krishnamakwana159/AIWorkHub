import { Stack, Typography } from "@mui/material";

import CommentCard from "./CommentCard";
import CommentForm from "./CommentForm";

import { useComments } from "../hooks/useComments";
import { useCreateComment } from "../hooks/useCreateComment";
import { useDeleteComment } from "../hooks/useDeleteComment";
import type { TaskComment } from "../types/comment";

type Props = {
  taskId: string;
};

export default function CommentsSection({ taskId }: Props) {
  const { data: comments = [] } = useComments(taskId);
  const createMutation = useCreateComment();
  const deleteMutation = useDeleteComment();

  async function handleCreate(content: string) {
    await createMutation.mutateAsync({
      taskId,
      request: {
        content,
      },
    });
  }

  async function handleDelete(id: string) {
    await deleteMutation.mutateAsync(id);
  }

  return (
    <Stack spacing={3}>
      <Typography variant="h6">Comments</Typography>

      <CommentForm loading={createMutation.isPending} onSubmit={handleCreate} />

      {comments.map((comment: TaskComment) => (
        <CommentCard
          key={comment.id}
          comment={comment}
          onDelete={handleDelete}
        />
      ))}
    </Stack>
  );
}
