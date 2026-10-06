import apiClient from "@/shared/api/apiClient";

import type {
    GenerateBreakdownRequest,
    GenerateDescriptionRequest,
    GenerateDescriptionResponse,
    SuggestPriorityRequest
} from "../types/ai";

export async function generateTaskDescription(
    request: GenerateDescriptionRequest
) {
    const { data } = await apiClient.post<GenerateDescriptionResponse>(
        "/ai/generate-description",
        request
    );

    return data.description;
}

export async function generateTaskBreakdown(
    request: GenerateBreakdownRequest
) {
    const { data } = await apiClient.post<string[]>(
        "/ai/generate-breakdown",
        request
    );

    return data;
}

export async function suggestTaskPriority(
    request: SuggestPriorityRequest
) {
    const { data } = await apiClient.post<string>(
        "/ai/suggest-priority",
        request
    );

    return data;
}

export async function getProjectSummary(projectId: string) {
    const { data } = await apiClient.get<string>(
        `/ai/project-summary/${projectId}`
    );

    return data;
}
