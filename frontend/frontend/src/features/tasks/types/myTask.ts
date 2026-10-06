import type { TaskPriority, WorkTaskStatus } from "@/shared/constants/task";

export interface MyTask {
    id: string;
    projectId: string;
    projectName: string;
    title: string;
    priority: TaskPriority;
    status: WorkTaskStatus;
    dueDateUtc?: string;
    assigneeName?: string;
}
