import type {
    ProjectPriority,
    ProjectStatus
} from "@/shared/constants/project";

export interface Project {
    id: string;
    name: string;
    description?: string;
    color: string;
    status: ProjectStatus;
    priority: ProjectPriority;
    isArchived: boolean;
    isFavorite: boolean;
    progress: number;
    startDateUtc?: string;
    targetCompletionDateUtc?: string;
    completedAtUtc?: string;
    createdAtUtc: string;

}

export interface GetProjectsParams {
    search?: string;
    status?: ProjectStatus;
    priority?: ProjectPriority;
    favorite?: boolean;
    archived?: boolean;
    page?: number;
    pageSize?: number;

}

export interface CreateProjectRequest {
    name: string;
    description?: string;
    color: string;
    priority: ProjectPriority;
    startDateUtc?: string;
    targetCompletionDateUtc?: string;
}

export interface UpdateProjectRequest
    extends CreateProjectRequest {
    status: ProjectStatus;
    isFavorite: boolean;
}
