import { useOutletContext } from "react-router-dom";
import {
    Chip,
    List,
    ListItem,
    ListItemText,
    Paper,
    Stack,
    Typography
} from "@mui/material";

import AppLoader from "@/components/ui/AppLoader";
import EmptyState from "@/components/ui/EmptyState";

import { useActivities } from "../hooks/useActivities";
import type { ProjectOutletContext } from "./ProjectLayout";

const ActivityActionLabels: Record<number, string> = {
    1: "Created",
    2: "Updated",
    3: "Deleted",
    4: "Assigned",
    5: "Status changed",
    6: "Comment added",
    7: "Comment updated",
    8: "Comment deleted",
    9: "Attachment added",
    10: "Attachment removed",
    11: "Attachment uploaded",
    12: "Attachment deleted",
    13: "Login",
    14: "Logout"
};

export default function ProjectActivityPage() {
    const { project } = useOutletContext<ProjectOutletContext>();
    const { data: activities = [], isPending, isError } = useActivities(project.id);

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
        <Paper variant="outlined">
            <List disablePadding>
                {activities.map((activity) => (
                    <ListItem
                        key={activity.id}
                        divider
                        alignItems="flex-start"
                    >
                        <ListItemText
                            primary={
                                <Stack
                                    sx={{
                                        display: "flex",
                                        flexDirection: "row",
                                        alignItems: "center",
                                        gap: 1
                                    }}
                                >
                                    <Typography variant="subtitle2">
                                        {activity.description}
                                    </Typography>
                                    <Chip
                                        label={ActivityActionLabels[activity.action] ?? "Activity"}
                                        size="small"
                                    />
                                </Stack>
                            }
                            secondary={new Date(activity.createdAtUtc).toLocaleString()}
                        />
                    </ListItem>
                ))}
            </List>
        </Paper>
    );
}
