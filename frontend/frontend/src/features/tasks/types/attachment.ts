export interface TaskAttachment {
    id: string;
    taskId: string;
    fileName: string;
    contentType: string;
    size: number;
    uploadedBy: string;
    createdAtUtc: string;
}
