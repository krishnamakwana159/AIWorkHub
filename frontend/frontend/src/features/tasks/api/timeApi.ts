import apiClient from "@/shared/api/apiClient";

import type {
    AddManualEntryRequest,
    StartTimerRequest,
    TimeEntry
} from "../types/timeEntry";

export async function getRunningTimer() {
    const { data } = await apiClient.get<TimeEntry | null>(
        "/time/running"
    );

    return data;
}

export async function getTaskTimeEntries(taskId: string) {
    const { data } = await apiClient.get<TimeEntry[]>(
        `/time/task/${taskId}`
    );

    return data;
}

export async function startTimer(request: StartTimerRequest) {
    await apiClient.post("/time/start", request);
}

export async function stopTimer() {
    await apiClient.post("/time/stop");
}

export async function addManualTimeEntry(request: AddManualEntryRequest) {
    await apiClient.post("/time/manual", request);
}
