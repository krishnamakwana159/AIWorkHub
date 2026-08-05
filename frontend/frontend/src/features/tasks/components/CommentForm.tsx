import { Button, Stack } from "@mui/material";
import { useState } from "react";

type Props = {
  loading?: boolean;
  initialValue?: string;
  onSubmit(content: string): void;
};

export default function CommentForm({
  loading = false,
  initialValue = "",
  onSubmit,
}: Props) {
  const [content, setContent] = useState(initialValue);

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    if (!content.trim()) return;

    onSubmit(content);

    setContent("");
  }

  return (
    <form onSubmit={handleSubmit}>
      <Stack spacing={2}>
        <textarea
          rows={4}
          value={content}
          onChange={(e) => setContent(e.target.value)}
          style={{
            width: "100%",
            resize: "vertical",
            padding: 12,
            borderRadius: 8,
          }}
        />

        <Button type="submit" variant="contained" disabled={loading}>
          Add Comment
        </Button>
      </Stack>
    </form>
  );
}
