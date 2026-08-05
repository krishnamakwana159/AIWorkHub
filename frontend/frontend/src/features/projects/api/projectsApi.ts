import apiClient from "@/shared/api/apiClient";

import type {
    CreateProjectRequest,
    Project,
    UpdateProjectRequest
} from "../types/project";

import type { ProjectQuery } from "../types/projectQuery";

export async function createProject(
    request: CreateProjectRequest
) {
    const response = await apiClient.post(
        "/projects",
        request
    );

    return response.data;
}

export async function updateProject(
    id: string,
    request: UpdateProjectRequest
) {
    await apiClient.put(
        `/projects/${id}`,
        request
    );
}

export async function getProject(
    id: string
): Promise<Project> {
    const { data } =
        await apiClient.get(
            `/projects/${id}`
        );

    return data;
}

export async function getProjects(
    query: ProjectQuery
) {
    const { data } =
        await apiClient.get(
            "/projects",
            {
                params: query
            }
        );

    return data;
}

export const deleteProject = (
    id: string
) => apiClient.delete(`/projects/${id}`);

export const toggleFavorite = (
    id: string
) => apiClient.patch(
    `/projects/${id}/favorite`
);

export const toggleArchive = (
    id: string
) => apiClient.patch(
    `/projects/${id}/archive`
);
