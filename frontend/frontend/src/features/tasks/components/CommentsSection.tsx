import { Stack, Typography } from "@mui/material";

import CommentCard from "./CommentCard";
import CommentForm from "./CommentForm";

import { useComments } from "../hooks/useComments";
import { useCreateComment } from "../hooks/useCreateComment";
import { useDeleteComment } from "../hooks/useDeleteComment";
import { useUpdateComment } from "../hooks/useUpdateComment";
import type { TaskComment } from "../types/comment";

type Props = {
  taskId: string;
};

export default function CommentsSection({ taskId }: Props) {
  const { data: comments = [] } = useComments(taskId);
  const createMutation = useCreateComment();
  const updateMutation = useUpdateComment();
  const deleteMutation = useDeleteComment();

  async function handleCreate(content: string) {
    await createMutation.mutateAsync({
      taskId,
      request: {
        content,
      },
    });
  }

  async function handleUpdate(id: string, content: string) {
    await updateMutation.mutateAsync({
      commentId: id,
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
          onUpdate={handleUpdate}
          updating={updateMutation.isPending}
        />
      ))}
    </Stack>
  );
}
