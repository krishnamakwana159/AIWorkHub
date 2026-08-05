import type {
    TaskPriority,
    WorkTaskStatus
} from "@/shared/constants/task";

export interface KanbanTask {
    id: string;
    title: string;
    status: WorkTaskStatus;
    priority: TaskPriority;
    order: number;
    assignedUserId?: string;
    assignedUserName?: string;
    dueDateUtc?: string;
    actualHours: number;
    estimatedHours: number;
}

export interface KanbanColumn {
    status: WorkTaskStatus;
    title: string;
    count: number;
    tasks: KanbanTask[];
}

export interface KanbanBoardResponse {
    projectId: string;
    projectName: string;
    columns: KanbanColumn[];
}

export interface MoveTaskRequest {
    projectId: string;
    status: WorkTaskStatus;
    order: number;
}
