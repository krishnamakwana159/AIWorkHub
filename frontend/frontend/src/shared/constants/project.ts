import type { ChipProps } from "@mui/material";

export const ProjectStatus = {
    Planning: 1,
    Active: 2,
    Completed: 3,
    OnHold: 4,
    Cancelled: 5
} as const;

export type ProjectStatus =
    typeof ProjectStatus[keyof typeof ProjectStatus];

export const ProjectPriority = {
    Low: 1,
    Medium: 2,
    High: 3,
    Critical: 4
} as const;

export type ProjectPriority =
    typeof ProjectPriority[keyof typeof ProjectPriority];

export const ProjectStatusInfo = {
    [ProjectStatus.Planning]: {
        label: "Planning",
        color: "default"
    },
    [ProjectStatus.Active]: {
        label: "Active",
        color: "success"
    },
    [ProjectStatus.Completed]: {
        label: "Completed",
        color: "primary"
    },
    [ProjectStatus.OnHold]: {
        label: "On Hold",
        color: "warning"
    },
    [ProjectStatus.Cancelled]: {
        label: "Cancelled",
        color: "error"
    }
} satisfies Record<
    ProjectStatus,
    {
        label: string;
        color: ChipProps["color"];
    }
>;

export const ProjectPriorityInfo = {
    [ProjectPriority.Low]: {
        label: "Low",
        color: "success"
    },
    [ProjectPriority.Medium]: {
        label: "Medium",
        color: "info"
    },
    [ProjectPriority.High]: {
        label: "High",
        color: "warning"
    },
    [ProjectPriority.Critical]: {
        label: "Critical",
        color: "error"
    }
} satisfies Record<
    ProjectPriority,
    {
        label: string;
        color: ChipProps["color"];
    }
>;
