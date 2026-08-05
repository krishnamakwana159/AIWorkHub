export interface TaskComment {
    id: string;
    taskId: string;
    content: string;
    userId: string;
    userName: string;
    createdAtUtc: string;
    updatedAtUtc?: string;
}

export interface CreateCommentRequest {
    content: string;
}

export interface UpdateCommentRequest {
    content: string;
}
