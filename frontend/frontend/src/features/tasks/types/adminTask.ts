import type { TaskPriority, WorkTaskStatus } from "@/shared/constants/task";

export interface AdminTask {
    id: string;
    projectId: string;
    projectName: string;
    title: string;
    description?: string;
    priority: TaskPriority;
    status: WorkTaskStatus;
    isPinned: boolean;
    isFavorite: boolean;
    startDateUtc?: string;
    dueDateUtc?: string;
    completedAtUtc?: string;
    assigneeId?: string;
    assigneeName?: string;
}
