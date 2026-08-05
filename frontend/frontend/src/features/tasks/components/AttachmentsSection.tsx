import { Button, Stack, Typography } from "@mui/material";

import AttachmentCard from "./AttachmentCard";

import { useAttachments } from "../hooks/useAttachments";
import { useUploadAttachment } from "../hooks/useUploadAttachment";
import { useDeleteAttachment } from "../hooks/useDeleteAttachment";

import { downloadAttachment } from "../api/tasksApi";

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
  }

  async function download(id: string) {
    const response = await downloadAttachment(id);
    const url = window.URL.createObjectURL(response.data);
    const a = document.createElement("a");
    a.href = url;
    a.download = "";
    a.click();
    window.URL.revokeObjectURL(url);
  }

  return (
    <Stack spacing={3}>
      <Typography variant="h6">Attachments</Typography>

      <Button component="label" variant="contained">
        Upload
        <input hidden type="file" onChange={upload} />
      </Button>

      {attachments.map((file) => (
        <AttachmentCard
          key={file.id}
          attachment={file}
          onDownload={download}
          onDelete={(id: any) => deleteMutation.mutate(id)}
        />
      ))}
    </Stack>
  );
}
