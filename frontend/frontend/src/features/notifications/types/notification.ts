export interface Notification {
    id: string;
    title: string;
    message: string;
    isRead: boolean;
    navigationUrl?: string;
    type: number;
    createdAtUtc: string;
}
