import { Card, CardContent, Stack, Typography, Button } from "@mui/material";
import type { TaskAttachment } from "../types/attachment";

type Props = {
  attachment: TaskAttachment;
  onDownload(id: string): void;
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
          sx={{direction:"row",
          justifyContent:"space-between",
          alignItems:"center"}}
        >
          <div>
            <Typography sx={{fontWeight:600}}>{attachment.fileName}</Typography>

            <Typography variant="body2" color="text.secondary">
              {(attachment.size / 1024).toFixed(2)} KB
            </Typography>
          </div>

          <Stack direction="row" spacing={1}>
            <Button onClick={() => onDownload(attachment.id)}>Download</Button>

            <Button color="error" onClick={() => onDelete(attachment.id)}>
              Delete
            </Button>
          </Stack>
        </Stack>
      </CardContent>
    </Card>
  );
}
