import { useState } from "react";
import { useNavigate } from "react-router-dom";

import {
    Badge,
    ClickAwayListener,
    IconButton,
    List,
    ListItemButton,
    ListItemText,
    Paper,
    Popper,
    Stack,
    Typography
} from "@mui/material";
import NotificationsIcon from "@mui/icons-material/Notifications";

import { useNotifications } from "../hooks/useNotifications";
import { useMarkNotificationAsRead } from "../hooks/useNotificationActions";
import { useUnreadNotificationCount } from "../hooks/useUnreadNotificationCount";

export default function NotificationBell() {
    const navigate = useNavigate();
    const [anchorEl, setAnchorEl] = useState<HTMLButtonElement | null>(null);
    const [open, setOpen] = useState(false);

    const { data: unreadCount = 0 } = useUnreadNotificationCount();
    const { data: notifications = [] } = useNotifications();
    const markRead = useMarkNotificationAsRead();

    const recent = notifications.slice(0, 5);

    function handleSelect(notification: (typeof notifications)[number]) {
        if (!notification.isRead) {
            markRead.mutate(notification.id);
        }

        setOpen(false);

        if (notification.navigationUrl) {
            navigate(notification.navigationUrl);
        } else {
            navigate("/notifications");
        }
    }

    return (
        <ClickAwayListener onClickAway={() => setOpen(false)}>
            <span>
                <IconButton
                    ref={setAnchorEl}
                    onClick={() => setOpen((prev) => !prev)}
                    sx={{
                        bgcolor: open ? "action.selected" : "transparent"
                    }}
                >
                    <Badge badgeContent={unreadCount} color="error">
                        <NotificationsIcon />
                    </Badge>
                </IconButton>

                <Popper
                    open={open}
                    anchorEl={anchorEl}
                    placement="bottom-end"
                    sx={{ zIndex: 1300, width: 360 }}
                >
                    <Paper
                        elevation={6}
                        sx={{
                            mt: 1.5,
                            borderRadius: 3,
                            overflow: "hidden"
                        }}
                    >
                        <Stack
                            direction="row"
                            sx={{
                                justifyContent: "space-between",
                                alignItems: "center",
                                px: 2,
                                py: 1.5,
                                borderBottom: "1px solid",
                                borderColor: "divider"
                            }}
                        >
                            <Typography variant="subtitle2" sx={{ fontWeight: 700 }}>
                                Notifications
                            </Typography>

                            {unreadCount > 0 && (
                                <Typography variant="caption" color="primary.main">
                                    {unreadCount} unread
                                </Typography>
                            )}
                        </Stack>

                        {recent.length === 0 && (
                            <Typography
                                variant="body2"
                                color="text.secondary"
                                sx={{ p: 3, textAlign: "center" }}
                            >
                                No notifications yet.
                            </Typography>
                        )}

                        {recent.length > 0 && (
                            <List disablePadding>
                                {recent.map((notification) => (
                                    <ListItemButton
                                        key={notification.id}
                                        onClick={() => handleSelect(notification)}
                                        sx={{
                                            px: 2,
                                            py: 1.25,
                                            borderLeft: "3px solid",
                                            borderColor: notification.isRead
                                                ? "transparent"
                                                : "primary.main"
                                        }}
                                    >
                                        <ListItemText
                                            primary={notification.title}
                                            secondary={notification.message}
                                            slotProps={{
                                                primary: {
                                                    sx: {
                                                        fontWeight: notification.isRead ? 400 : 700
                                                    }
                                                },
                                                secondary: {
                                                    sx: {
                                                        overflow: "hidden",
                                                        textOverflow: "ellipsis",
                                                        whiteSpace: "nowrap"
                                                    }
                                                }
                                            }}
                                        />
                                    </ListItemButton>
                                ))}
                            </List>
                        )}

                        <ListItemButton
                            onClick={() => {
                                setOpen(false);
                                navigate("/notifications");
                            }}
                            sx={{
                                borderTop: "1px solid",
                                borderColor: "divider",
                                py: 1.25
                            }}
                        >
                            <ListItemText
                                primary="View all notifications"
                                slotProps={{
                                    primary: {
                                        sx: {
                                            textAlign: "center",
                                            color: "primary.main",
                                            fontWeight: 600
                                        }
                                    }
                                }}
                            />
                        </ListItemButton>
                    </Paper>
                </Popper>
            </span>
        </ClickAwayListener>
    );
}
