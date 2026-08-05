import type { TaskPriority, WorkTaskStatus } from "@/shared/constants/task";

export interface WorkTask {
  id: string;
  projectId: string;
  title: string;
  description?: string;
  priority: TaskPriority;
  status: WorkTaskStatus;
  isPinned: boolean;
  isFavorite: boolean;
  order: number;
  estimatedHours: number;
  actualHours: number;
  startDateUtc?: string;
  dueDateUtc?: string;
  completedAtUtc?: string;
}

export interface CreateTaskRequest {
  projectId: string;
  title: string;
  description?: string;
  priority: TaskPriority;
  startDateUtc?: string;
  dueDateUtc?: string;
  estimatedHours: number;
}

export interface UpdateTaskRequest {
  title: string;
  description?: string;
  priority: TaskPriority;
  status: WorkTaskStatus;
  startDateUtc?: string;
  dueDateUtc?: string;
  estimatedHours: number;
  actualHours: number;
}
