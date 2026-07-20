import {
    Drawer,
    List,
    ListItemButton,
    ListItemText,
    Toolbar
} from "@mui/material";

import { NavLink } from "react-router-dom";

const drawerWidth = 250;

const menus = [
    {
        label: "Dashboard",
        path: "/dashboard"
    },
    {
        label: "Projects",
        path: "/projects"
    },
    {
        label: "Tasks",
        path: "/tasks"
    },
    {
        label: "Kanban",
        path: "/kanban"
    },
    {
        label: "Reports",
        path: "/reports"
    },
    {
        label: "AI Assistant",
        path: "/ai"
    },
    {
        label: "Notifications",
        path: "/notifications"
    },
    {
        label: "Settings",
        path: "/settings"
    },
    {
        label: "Profile",
        path: "/profile"
    }
];

export default function AppSidebar() {
    return (
        <Drawer
            variant="permanent"
            sx={{
                width: drawerWidth,
                flexShrink: 0,
                "& .MuiDrawer-paper": {
                    width: drawerWidth,
                    boxSizing: "border-box"
                }
            }}
        >
            <Toolbar />

            <List>
                {menus.map(menu => (
                    <ListItemButton
                        key={menu.path}
                        component={NavLink}
                        to={menu.path}
                        sx={{
                            "&.active": {
                                bgcolor: "primary.main",
                                color: "primary.contrastText"
                            }
                        }}
                    >
                        <ListItemText primary={menu.label} />
                    </ListItemButton>
                ))}
            </List>
        </Drawer>
    );
}
