import { Button, Stack, Typography } from "@mui/material";

import AttachmentCard from "./AttachmentCard";

import { useAttachments } from "../hooks/useAttachments";
import { useUploadAttachment } from "../hooks/useUploadAttachment";
import { useDeleteAttachment } from "../hooks/useDeleteAttachment";

import { downloadAttachment } from "../api/tasksApi";
import type { TaskAttachment } from "../types/attachment";

type Props = {
  taskId: string;
};

export default function AttachmentsSection({ taskId }: Props) {
  const { data: attachments = [] } = useAttachments(taskId);
  const uploadMutation = useUploadAttachment();
  const deleteMutation = useDeleteAttachment();

  async function upload(e: React.ChangeEvent<HTMLInputElement>) {
    const file = e.target.files?.[0];

    if (!file) return;

    await uploadMutation.mutateAsync({
      taskId,
      file,
    });

    e.target.value = "";
  }

  async function download(attachment: TaskAttachment) {
    const response = await downloadAttachment(attachment.id);
    const url = window.URL.createObjectURL(response.data);
    const a = document.createElement("a");
    a.href = url;
    a.download = attachment.fileName;
    a.click();
    window.URL.revokeObjectURL(url);
  }

  function handleDelete(id: string) {
    deleteMutation.mutate(id);
  }

  return (
    <Stack spacing={3}>
      <Typography variant="h6">Attachments</Typography>

      <Button
        component="label"
        variant="contained"
        disabled={uploadMutation.isPending}
        sx={{ alignSelf: "flex-start" }}
      >
        Upload
        <input hidden type="file" onChange={upload} />
      </Button>

      {attachments.map((file) => (
        <AttachmentCard
          key={file.id}
          attachment={file}
          onDownload={download}
          onDelete={handleDelete}
        />
      ))}
    </Stack>
  );
}
