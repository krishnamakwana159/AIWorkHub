export interface Activity {
    id: string;
    entityType: number;
    entityId: string;
    action: number;
    description: string;
    userId?: string;
    createdAtUtc: string;
}
