import {
    Button,
    Chip,
    IconButton,
    List,
    ListItem,
    ListItemText,
    Paper,
    Stack,
    Tooltip,
    Typography
} from "@mui/material";
import DeleteOutlineIcon from "@mui/icons-material/DeleteOutlined";
import DoneIcon from "@mui/icons-material/Done";
import { useNavigate } from "react-router-dom";

import PageContainer from "@/components/common/PageContainer";
import PageTitle from "@/components/common/PageTitle";
import AppLoader from "@/components/ui/AppLoader";
import EmptyState from "@/components/ui/EmptyState";

import {
    useDeleteNotification,
    useMarkAllNotificationsAsRead,
    useMarkNotificationAsRead
} from "../hooks/useNotificationActions";
import { useNotifications } from "../hooks/useNotifications";

export default function NotificationsPage() {
    const navigate = useNavigate();
    const { data: notifications = [], isPending, isError } = useNotifications();
    const markRead = useMarkNotificationAsRead();
    const markAllRead = useMarkAllNotificationsAsRead();
    const deleteMutation = useDeleteNotification();
    const unreadCount = notifications.filter((notification) => !notification.isRead).length;

    return (
        <PageContainer>
            <Stack
                sx={{
                    display: "flex",
                    flexDirection: "row",
                    justifyContent: "space-between",
                    alignItems: "center",
                    gap: 2
                }}
            >
                <PageTitle
                    title="Notifications"
                    subtitle="Your notifications"
                />
                <Button
                    variant="outlined"
                    disabled={unreadCount === 0 || markAllRead.isPending}
                    onClick={() => markAllRead.mutate()}
                >
                    Mark All Read
                </Button>
            </Stack>

            {isPending && <AppLoader />}

            {isError && <EmptyState message="Unable to load notifications." />}

            {!isPending && !isError && notifications.length === 0 && (
                <EmptyState message="No notifications yet." />
            )}

            {!isPending && !isError && notifications.length > 0 && (
                <Paper variant="outlined">
                    <List disablePadding>
                        {notifications.map((notification) => (
                            <ListItem
                                key={notification.id}
                                divider
                                secondaryAction={
                                    <Stack
                                        sx={{
                                            display: "flex",
                                            flexDirection: "row",
                                            gap: 1
                                        }}
                                    >
                                        {!notification.isRead && (
                                            <Tooltip title="Mark as read">
                                                <IconButton
                                                    onClick={() => markRead.mutate(notification.id)}
                                                >
                                                    <DoneIcon />
                                                </IconButton>
                                            </Tooltip>
                                        )}
                                        <Tooltip title="Delete">
                                            <IconButton
                                                color="error"
                                                onClick={() => deleteMutation.mutate(notification.id)}
                                            >
                                                <DeleteOutlineIcon />
                                            </IconButton>
                                        </Tooltip>
                                    </Stack>
                                }
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
                                            <Typography
                                                variant="subtitle2"
                                                onClick={() => {
                                                    if (notification.navigationUrl) {
                                                        navigate(notification.navigationUrl);
                                                    }
                                                }}
                                                sx={{ cursor: notification.navigationUrl ? "pointer" : "default" }}
                                            >
                                                {notification.title}
                                            </Typography>
                                            {!notification.isRead && <Chip label="Unread" size="small" color="primary" />}
                                        </Stack>
                                    }
                                    secondary={`${notification.message} • ${new Date(notification.createdAtUtc).toLocaleString()}`}
                                />
                            </ListItem>
                        ))}
                    </List>
                </Paper>
            )}
        </PageContainer>
    );
}
