import { Card, CardContent, Stack, Typography, Button } from "@mui/material";
import type { TaskAttachment } from "../types/attachment";

type Props = {
  attachment: TaskAttachment;
  onDownload(attachment: TaskAttachment): void;
  onDelete(id: string): void;
};

export default function AttachmentCard({
  attachment,
  onDownload,
  onDelete,
}: Props) {
  return (
    <Card>
      <CardContent>
        <Stack
          direction="row"
          sx={{ justifyContent: "space-between", alignItems: "center" }}
        >
          <div>
            <Typography sx={{ fontWeight: 600 }}>
              {attachment.fileName}
            </Typography>

            <Typography variant="body2" color="text.secondary">
              {(attachment.size / 1024).toFixed(2)} KB · {attachment.uploadedBy}
            </Typography>
          </div>

          <Stack direction="row" spacing={1}>
            <Button onClick={() => onDownload(attachment)}>Download</Button>

            <Button color="error" onClick={() => onDelete(attachment.id)}>
              Delete
            </Button>
          </Stack>
        </Stack>
      </CardContent>
    </Card>
  );
}
