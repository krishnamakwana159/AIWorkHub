export interface TimeEntry {
    id: string;
    workTaskId: string;
    taskTitle: string;
    startTimeUtc: string;
    endTimeUtc?: string;
    hours: number;
    isRunning: boolean;
    description?: string;
    userName: string;
}

export interface StartTimerRequest {
    workTaskId: string;
}

export interface AddManualEntryRequest {
    workTaskId: string;
    startTimeUtc: string;
    endTimeUtc: string;
    description?: string;
}
