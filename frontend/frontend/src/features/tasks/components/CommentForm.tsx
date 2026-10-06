import { Button, Stack } from "@mui/material";
import { useState } from "react";

type Props = {
  loading?: boolean;
  initialValue?: string;
  submitLabel?: string;
  onSubmit(content: string): void;
  onCancel?(): void;
};

export default function CommentForm({
  loading = false,
  initialValue = "",
  submitLabel = "Add Comment",
  onSubmit,
  onCancel,
}: Props) {
  const [content, setContent] = useState(initialValue);

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    if (!content.trim()) return;

    onSubmit(content);

    if (!onCancel) {
      setContent("");
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <Stack spacing={2} sx={{ mt: onCancel ? 2 : 0 }}>
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

        <Stack direction="row" spacing={1}>
          <Button type="submit" variant="contained" disabled={loading}>
            {submitLabel}
          </Button>

          {onCancel && (
            <Button onClick={onCancel} disabled={loading}>
              Cancel
            </Button>
          )}
        </Stack>
      </Stack>
    </form>
  );
}
