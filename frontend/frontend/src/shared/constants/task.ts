export const TaskPriority = {
    Low: 1,
    Medium: 2,
    High: 3,
    Critical: 4
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
    Todo: 1,
    InProgress: 2,
    InReview: 3,
    Completed: 4,
    Cancelled: 5
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
