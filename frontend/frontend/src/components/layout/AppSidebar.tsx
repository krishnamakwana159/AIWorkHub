import {
    Box,
    Drawer,
    List,
    ListItemButton,
    ListItemIcon,
    ListItemText,
    Stack,
    Tooltip,
    Typography
} from "@mui/material";

import DashboardIcon from "@mui/icons-material/DashboardOutlined";
import FolderIcon from "@mui/icons-material/FolderOutlined";
import TaskAltIcon from "@mui/icons-material/TaskAltOutlined";
import CalendarMonthIcon from "@mui/icons-material/CalendarMonthOutlined";
import AssessmentIcon from "@mui/icons-material/AssessmentOutlined";
import AutoAwesomeIcon from "@mui/icons-material/AutoAwesomeOutlined";
import NotificationsIcon from "@mui/icons-material/NotificationsOutlined";
import SettingsIcon from "@mui/icons-material/SettingsOutlined";
import PersonIcon from "@mui/icons-material/PersonOutlined";

import { NavLink } from "react-router-dom";

import {
    SIDEBAR_COLLAPSED_WIDTH,
    SIDEBAR_EXPANDED_WIDTH
} from "./layoutConstants";

const menus = [
    { label: "Dashboard", path: "/dashboard", icon: <DashboardIcon fontSize="small" /> },
    { label: "Projects", path: "/projects", icon: <FolderIcon fontSize="small" /> },
    { label: "Tasks", path: "/tasks", icon: <TaskAltIcon fontSize="small" /> },
    { label: "Calendar", path: "/calendar", icon: <CalendarMonthIcon fontSize="small" /> },
    { label: "Reports", path: "/reports", icon: <AssessmentIcon fontSize="small" /> },
    { label: "AI Assistant", path: "/ai", icon: <AutoAwesomeIcon fontSize="small" /> },
    { label: "Notifications", path: "/notifications", icon: <NotificationsIcon fontSize="small" /> },
    { label: "Settings", path: "/settings", icon: <SettingsIcon fontSize="small" /> },
    { label: "Profile", path: "/profile", icon: <PersonIcon fontSize="small" /> }
];

type Props = {
    collapsed: boolean;
};

export default function AppSidebar({ collapsed }: Props) {
    const width = collapsed ? SIDEBAR_COLLAPSED_WIDTH : SIDEBAR_EXPANDED_WIDTH;

    return (
        <Drawer
            variant="permanent"
            sx={{
                width,
                flexShrink: 0,
                whiteSpace: "nowrap",
                transition: (theme) =>
                    theme.transitions.create("width", {
                        easing: theme.transitions.easing.sharp,
                        duration: theme.transitions.duration.enteringScreen
                    }),
                "& .MuiDrawer-paper": {
                    width,
                    overflowX: "hidden",
                    boxSizing: "border-box",
                    borderRight: "1px solid",
                    borderColor: "divider",
                    transition: (theme) =>
                        theme.transitions.create("width", {
                            easing: theme.transitions.easing.sharp,
                            duration: theme.transitions.duration.enteringScreen
                        })
                }
            }}
        >
            <Stack
                direction="row"
                spacing={1.5}
                sx={{
                    alignItems: "center",
                    height: 64,
                    px: collapsed ? 0 : 2.5,
                    justifyContent: collapsed ? "center" : "flex-start"
                }}
            >
                <Box
                    sx={{
                        width: 34,
                        height: 34,
                        borderRadius: 2,
                        bgcolor: "primary.main",
                        color: "primary.contrastText",
                        display: "flex",
                        alignItems: "center",
                        justifyContent: "center",
                        fontWeight: 800,
                        fontSize: 16,
                        flexShrink: 0
                    }}
                >
                    A
                </Box>

                {!collapsed && (
                    <Stack sx={{ minWidth: 0 }}>
                        <Typography
                            variant="subtitle1"
                            sx={{ fontWeight: 800, lineHeight: 1.1, whiteSpace: "nowrap" }}
                        >
                            AIWorkHub
                        </Typography>

                        <Typography
                            variant="caption"
                            color="text.secondary"
                            sx={{ lineHeight: 1, whiteSpace: "nowrap" }}
                        >
                            Work, organized
                        </Typography>
                    </Stack>
                )}
            </Stack>

            <List sx={{ px: collapsed ? 1 : 1.5, py: 1 }}>
                {menus.map(menu => {
                    const button = (
                        <ListItemButton
                            key={menu.path}
                            component={NavLink}
                            to={menu.path}
                            sx={{
                                borderRadius: 2,
                                mb: 0.5,
                                color: "text.secondary",
                                justifyContent: collapsed ? "center" : "flex-start",
                                px: collapsed ? 1.5 : 2,
                                "& .MuiListItemIcon-root": {
                                    color: "text.secondary",
                                    minWidth: collapsed ? 0 : 36,
                                    justifyContent: "center"
                                },
                                "&.active": {
                                    bgcolor: "primary.main",
                                    color: "primary.contrastText",
                                    "& .MuiListItemIcon-root": {
                                        color: "primary.contrastText"
                                    }
                                }
                            }}
                        >
                            <ListItemIcon>{menu.icon}</ListItemIcon>

                            {!collapsed && (
                                <ListItemText
                                    primary={menu.label}
                                    slotProps={{
                                        primary: { sx: { fontSize: 14, fontWeight: 600 } }
                                    }}
                                />
                            )}
                        </ListItemButton>
                    );

                    return collapsed ? (
                        <Tooltip key={menu.path} title={menu.label} placement="right">
                            {button}
                        </Tooltip>
                    ) : (
                        button
                    );
                })}
            </List>
        </Drawer>
    );
}
