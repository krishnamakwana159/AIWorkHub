import { useState } from "react";
import { Box, Toolbar } from "@mui/material";
import { Outlet } from "react-router-dom";

import AppHeader from "../components/layout/AppHeader";
import AppSidebar from "../components/layout/AppSidebar";
import {
    SIDEBAR_COLLAPSED_WIDTH,
    SIDEBAR_EXPANDED_WIDTH
} from "../components/layout/layoutConstants";

export default function MainLayout() {
    const [collapsed, setCollapsed] = useState(false);

    const drawerWidth = collapsed
        ? SIDEBAR_COLLAPSED_WIDTH
        : SIDEBAR_EXPANDED_WIDTH;

    return (
        <Box sx={{ display: "flex" }}>
            <AppHeader
                drawerWidth={drawerWidth}
                collapsed={collapsed}
                onToggleCollapse={() => setCollapsed((prev) => !prev)}
            />

            <AppSidebar collapsed={collapsed} />

            <Box
                component="main"
                sx={{
                    flexGrow: 1,
                    p: 3,
                    minWidth: 0
                }}
            >
                <Toolbar />
                <Outlet />
            </Box>
        </Box>
    );
}
