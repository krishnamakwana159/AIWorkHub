export const TaskPriority = {
    Low: 0,
    Medium: 1,
    High: 2,
    Critical: 3
} as const;

export type TaskPriority =
    (typeof TaskPriority)[keyof typeof TaskPriority];

export const TaskPriorityInfo = {

    [TaskPriority.Low]: {
        label: "Low",
        color: "success"
    },

    [TaskPriority.Medium]: {
        label: "Medium",
        color: "info"
    },

    [TaskPriority.High]: {
        label: "High",
        color: "warning"
    },

    [TaskPriority.Critical]: {
        label: "Critical",
        color: "error"
    }

} as const;

export const WorkTaskStatus = {
    Todo: 0,
    InProgress: 1,
    InReview: 2,
    Completed: 3,
    Cancelled: 4
} as const;

export type WorkTaskStatus =
    (typeof WorkTaskStatus)[keyof typeof WorkTaskStatus];

export const WorkTaskStatusInfo = {

    [WorkTaskStatus.Todo]: {
        label: "To Do",
        color: "default"
    },

    [WorkTaskStatus.InProgress]: {
        label: "In Progress",
        color: "primary"
    },

    [WorkTaskStatus.InReview]: {
        label: "In Review",
        color: "warning"
    },

    [WorkTaskStatus.Completed]: {
        label: "Completed",
        color: "success"
    },

    [WorkTaskStatus.Cancelled]: {
        label: "Cancelled",
        color: "error"
    }

} as const;
