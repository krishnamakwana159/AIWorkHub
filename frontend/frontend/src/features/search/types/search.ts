export interface SearchProjectResult {
    id: string;
    name: string;
    description?: string;
    status: string;
}

export interface SearchTaskResult {
    id: string;
    projectId: string;
    title: string;
    status: string;
}

export interface SearchUserResult {
    id: string;
    fullName: string;
    email: string;
}

export interface SearchCommentResult {
    id: string;
    taskId: string;
    comment: string;
}

export interface SearchResponse {
    projects: SearchProjectResult[];
    tasks: SearchTaskResult[];
    users: SearchUserResult[];
    comments: SearchCommentResult[];
}
