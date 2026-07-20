import apiClient from "@/shared/api/apiClient";
import type { CreateProjectRequest, UpdateProjectRequest } from "../types/project";
import type { ProjectQuery } from "../types/projectQuery";

export async function createProject(
  request: CreateProjectRequest
) {

  const response =
    await apiClient.post(
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

// export async function getProjects(
//   params: GetProjectsParams
// ) {
//   const response =
//     await apiClient.get<Project[]>(
//       "/projects",
//       {
//         params
//       }
//     );
//   return response.data;
// }

export const getProjects = async (query: ProjectQuery) => {
    const { data } = await apiClient.get("/projects", {
        params: query
    });

    return data;
};

export const deleteProject = (id: string) =>
    apiClient.delete(`/projects/${id}`);

export const toggleFavorite = (id: string) =>
    apiClient.patch(`/projects/${id}/favorite`);

export const toggleArchive = (id: string) =>
    apiClient.patch(`/projects/${id}/archive`);
