import { Box, Toolbar } from "@mui/material";
import { Outlet } from "react-router-dom";

import AppHeader from "../components/layout/AppHeader";
import AppSidebar from "../components/layout/AppSidebar";

export default function MainLayout() {
  return (
    <Box sx={{ display: "flex" }}>
      <AppHeader />

      <AppSidebar />

      <Box
        component="main"
        sx={{
          flexGrow: 1,
          p: 3
        }}
      >
        <Toolbar />

        <Outlet />
      </Box>
    </Box>
  );
}
