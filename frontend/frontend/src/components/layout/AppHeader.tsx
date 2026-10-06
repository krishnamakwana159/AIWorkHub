import {
    AppBar,
    Toolbar,
    Box,
    Button,
    Divider,
    IconButton,
    Stack,
    Tooltip
} from "@mui/material";
import MenuOpenIcon from "@mui/icons-material/MenuOpen";
import MenuIcon from "@mui/icons-material/Menu";
import LogoutIcon from "@mui/icons-material/Logout";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../contexts/AuthContext";
import GlobalSearchBar from "@/features/search/components/GlobalSearchBar";
import NotificationBell from "@/features/notifications/components/NotificationBell";

type Props = {
    drawerWidth: number;
    collapsed: boolean;
    onToggleCollapse(): void;
};

export default function AppHeader({
    drawerWidth,
    collapsed,
    onToggleCollapse
}: Props) {

    const auth = useAuth();
    const navigate = useNavigate();

    function logout() {
        auth.logout();
        navigate("/login");
    }

    return (
        <AppBar
            position="fixed"
            elevation={0}
            color="inherit"
            sx={{
                width: `calc(100% - ${drawerWidth}px)`,
                ml: `${drawerWidth}px`,
                borderBottom: "1px solid",
                borderColor: "divider",
                transition: (theme) =>
                    theme.transitions.create(["width", "margin"], {
                        easing: theme.transitions.easing.sharp,
                        duration: theme.transitions.duration.enteringScreen
                    })
            }}
        >
            <Toolbar sx={{ gap: 2 }}>
                <Tooltip title={collapsed ? "Expand menu" : "Collapse menu"}>
                    <IconButton onClick={onToggleCollapse} sx={{ flexShrink: 0 }}>
                        {collapsed ? <MenuIcon /> : <MenuOpenIcon />}
                    </IconButton>
                </Tooltip>

                <GlobalSearchBar />

                <Box sx={{ flexGrow: 1 }} />

                <Stack direction="row" spacing={1} sx={{ alignItems: "center" }}>
                    <NotificationBell />

                    <Divider orientation="vertical" flexItem sx={{ mx: 0.5, my: 1.5 }} />

                    <Button
                        color="inherit"
                        startIcon={<LogoutIcon fontSize="small" />}
                        onClick={logout}
                        sx={{ fontWeight: 600 }}
                    >
                        Logout
                    </Button>
                </Stack>
            </Toolbar>
        </AppBar>

    );

}
