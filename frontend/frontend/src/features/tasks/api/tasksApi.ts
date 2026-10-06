import apiClient from "@/shared/api/apiClient";
import type { CreateTaskRequest, UpdateTaskRequest } from "../types/task";
import type { TaskQuery } from "../types/taskQuery";
import type { TaskAttachment } from "../types/attachment";
import type { CalendarTask } from "../types/calendarTask";
import type { MyTask } from "../types/myTask";
import type {
  CreateCommentRequest,
  TaskComment,
  UpdateCommentRequest,
} from "../types/comment";
import type { ProjectRole } from "@/shared/constants/project";

export async function getCalendarTasks() {
  const { data } = await apiClient.get<CalendarTask[]>("/tasks/calendar");

  return data;
}

export async function getMyTasks() {
  const { data } = await apiClient.get<MyTask[]>("/tasks/mine");

  return data;
}

export async function getTasks(query: TaskQuery) {
  const { data } = await apiClient.get("/tasks", {
    params: query,
  });

  return data;
}

export async function getTask(id: string) {
  const { data } = await apiClient.get(`/tasks/${id}`);

  return data;
}

export async function createTask(request: CreateTaskRequest) {
  const { data } = await apiClient.post("/tasks", request);

  return data;
}

export async function updateTask(id: string, request: UpdateTaskRequest) {
  await apiClient.put(`/tasks/${id}`, request);
}

export async function deleteTask(id: string) {
  await apiClient.delete(`/tasks/${id}`);
}
export const toggleTaskFavorite = (id: string) =>
  apiClient.patch(`/tasks/${id}/favorite`);

export const toggleTaskPin = (id: string) =>
  apiClient.patch(`/tasks/${id}/pin`);

export async function updateStatus(id: string, status: number) {
  await apiClient.patch(`/tasks/${id}/status`, {
    status,
  });
}

export async function getComments(taskId: string) {
  const { data } = await apiClient.get<TaskComment[]>(`/tasks/${taskId}/comments`);

  return data;
}

export async function createComment(
  taskId: string,
  request: CreateCommentRequest,
) {
  await apiClient.post(`/tasks/${taskId}/comments`, request);
}

export async function updateComment(id: string, request: UpdateCommentRequest) {
  await apiClient.put(`/tasks/comments/${id}`, request);
}

export async function deleteComment(id: string) {
  await apiClient.delete(`/tasks/comments/${id}`);
}

export async function getAttachments(taskId: string) {
  const { data } = await apiClient.get<TaskAttachment[]>(
    `/tasks/${taskId}/attachments`,
  );

  return data;
}

export async function uploadAttachment(taskId: string, file: File) {
  const formData = new FormData();

  formData.append("file", file);

  await apiClient.post(`/tasks/${taskId}/attachments`, formData, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
}

export function downloadAttachment(id: string) {
  return apiClient.get(`/tasks/attachments/${id}/download`, {
    responseType: "blob",
  });
}

export function deleteAttachment(id: string) {
  return apiClient.delete(`/tasks/attachments/${id}`);
}

export async function assignTask(id: string, userId: string) {
  await apiClient.patch(`/tasks/${id}/assign`, {
    userId,
  });
}

export async function updateProjectMemberRole(
    projectId: string,
    userId: string,
    role: ProjectRole
) {
    await apiClient.put(
        `/projects/${projectId}/members/${userId}/role`,
        { role }
    );
}

export async function removeProjectMember(
    projectId: string,
    userId: string
) {
    await apiClient.delete(
        `/projects/${projectId}/members/${userId}`
    );
}


