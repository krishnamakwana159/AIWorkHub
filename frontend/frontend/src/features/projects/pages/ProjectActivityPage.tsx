import { useMemo } from "react";
import { useOutletContext } from "react-router-dom";

import dayjs from "dayjs";
import relativeTime from "dayjs/plugin/relativeTime";

import {
    Avatar,
    Box,
    Stack,
    Tooltip,
    Typography
} from "@mui/material";

import AddCircleIcon from "@mui/icons-material/AddCircle";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import PersonAddIcon from "@mui/icons-material/PersonAdd";
import SwapHorizIcon from "@mui/icons-material/SwapHoriz";
import CommentIcon from "@mui/icons-material/Comment";
import AttachFileIcon from "@mui/icons-material/AttachFile";
import LoginIcon from "@mui/icons-material/Login";
import LogoutIcon from "@mui/icons-material/Logout";
import CircleIcon from "@mui/icons-material/Circle";

import AppLoader from "@/components/ui/AppLoader";
import EmptyState from "@/components/ui/EmptyState";

import { useActivities } from "../hooks/useActivities";
import type { Activity } from "../types/activity";
import type { ProjectOutletContext } from "./ProjectLayout";

dayjs.extend(relativeTime);

type ActionMeta = {
    label: string;
    icon: React.ReactNode;
    color: string;
};

const ACTION_META: Record<number, ActionMeta> = {
    1: { label: "Created", icon: <AddCircleIcon fontSize="small" />, color: "#16a34a" },
    2: { label: "Updated", icon: <EditIcon fontSize="small" />, color: "#2563eb" },
    3: { label: "Deleted", icon: <DeleteIcon fontSize="small" />, color: "#dc2626" },
    4: { label: "Assigned", icon: <PersonAddIcon fontSize="small" />, color: "#7c3aed" },
    5: { label: "Status changed", icon: <SwapHorizIcon fontSize="small" />, color: "#f59e0b" },
    6: { label: "Comment added", icon: <CommentIcon fontSize="small" />, color: "#2563eb" },
    7: { label: "Comment updated", icon: <CommentIcon fontSize="small" />, color: "#2563eb" },
    8: { label: "Comment deleted", icon: <CommentIcon fontSize="small" />, color: "#dc2626" },
    9: { label: "Attachment added", icon: <AttachFileIcon fontSize="small" />, color: "#16a34a" },
    10: { label: "Attachment removed", icon: <AttachFileIcon fontSize="small" />, color: "#dc2626" },
    11: { label: "Attachment uploaded", icon: <AttachFileIcon fontSize="small" />, color: "#16a34a" },
    12: { label: "Attachment deleted", icon: <AttachFileIcon fontSize="small" />, color: "#dc2626" },
    13: { label: "Logged in", icon: <LoginIcon fontSize="small" />, color: "#64748b" },
    14: { label: "Logged out", icon: <LogoutIcon fontSize="small" />, color: "#64748b" }
};

function metaFor(action: number): ActionMeta {
    return ACTION_META[action] ?? {
        label: "Activity",
        icon: <CircleIcon fontSize="small" />,
        color: "#64748b"
    };
}

function initials(name?: string) {
    if (!name) return "?";

    return name
        .split(" ")
        .filter(Boolean)
        .slice(0, 2)
        .map((part) => part[0]?.toUpperCase())
        .join("");
}

function dayLabel(date: string) {
    const day = dayjs(date);

    if (day.isSame(dayjs(), "day")) return "Today";
    if (day.isSame(dayjs().subtract(1, "day"), "day")) return "Yesterday";

    return day.format("MMMM D, YYYY");
}

function groupByDay(activities: Activity[]) {
    const groups: { label: string; items: Activity[] }[] = [];

    for (const activity of activities) {
        const label = dayLabel(activity.createdAtUtc);
        const lastGroup = groups[groups.length - 1];

        if (lastGroup && lastGroup.label === label) {
            lastGroup.items.push(activity);
        } else {
            groups.push({ label, items: [activity] });
        }
    }

    return groups;
}

export default function ProjectActivityPage() {
    const { project } = useOutletContext<ProjectOutletContext>();
    const { data: activities = [], isPending, isError } = useActivities(project.id);

    const groups = useMemo(() => groupByDay(activities), [activities]);

    if (isPending) {
        return <AppLoader />;
    }

    if (isError) {
        return <EmptyState message="Unable to load activity." />;
    }

    if (activities.length === 0) {
        return <EmptyState message="No project activity yet." />;
    }

    return (
        <Stack spacing={4}>
            {groups.map((group) => (
                <Box key={group.label}>
                    <Typography
                        variant="overline"
                        sx={{ color: "text.secondary", fontWeight: 700 }}
                    >
                        {group.label}
                    </Typography>

                    <Stack sx={{ mt: 1.5 }}>
                        {group.items.map((activity, index) => {
                            const meta = metaFor(activity.action);
                            const isLast = index === group.items.length - 1;

                            return (
                                <Stack
                                    key={activity.id}
                                    direction="row"
                                    spacing={2}
                                >
                                    <Stack sx={{ alignItems: "center" }}>
                                        <Box
                                            sx={{
                                                width: 32,
                                                height: 32,
                                                borderRadius: "50%",
                                                display: "flex",
                                                alignItems: "center",
                                                justifyContent: "center",
                                                bgcolor: `${meta.color}1a`,
                                                color: meta.color,
                                                flexShrink: 0
                                            }}
                                        >
                                            {meta.icon}
                                        </Box>

                                        {!isLast && (
                                            <Box
                                                sx={{
                                                    width: "2px",
                                                    flexGrow: 1,
                                                    minHeight: 24,
                                                    bgcolor: "divider",
                                                    my: 0.5
                                                }}
                                            />
                                        )}
                                    </Stack>

                                    <Stack sx={{ pb: 3, minWidth: 0 }}>
                                        <Stack
                                            direction="row"
                                            spacing={1}
                                            sx={{ alignItems: "center", flexWrap: "wrap" }}
                                        >
                                            <Typography variant="body2" sx={{ fontWeight: 600 }}>
                                                {activity.description}
                                            </Typography>

                                            <Typography
                                                variant="caption"
                                                sx={{
                                                    color: meta.color,
                                                    fontWeight: 600,
                                                    textTransform: "uppercase"
                                                }}
                                            >
                                                {meta.label}
                                            </Typography>
                                        </Stack>

                                        <Stack
                                            direction="row"
                                            spacing={1}
                                            sx={{ alignItems: "center", mt: 0.5 }}
                                        >
                                            {activity.userName && (
                                                <Avatar
                                                    sx={{ width: 18, height: 18, fontSize: 10 }}
                                                >
                                                    {initials(activity.userName)}
                                                </Avatar>
                                            )}

                                            <Typography variant="caption" color="text.secondary">
                                                {activity.userName ?? "System"}
                                            </Typography>

                                            <Typography variant="caption" color="text.secondary">
                                                ·
                                            </Typography>

                                            <Tooltip
                                                title={new Date(activity.createdAtUtc).toLocaleString()}
                                            >
                                                <Typography variant="caption" color="text.secondary">
                                                    {dayjs(activity.createdAtUtc).fromNow()}
                                                </Typography>
                                            </Tooltip>
                                        </Stack>
                                    </Stack>
                                </Stack>
                            );
                        })}
                    </Stack>
                </Box>
            ))}
        </Stack>
    );
}
