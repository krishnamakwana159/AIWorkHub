import {
    AppBar,
    Toolbar,
    Typography,
    Box,
    Button
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../contexts/AuthContext";

export default function AppHeader() {

    const auth = useAuth();
    const navigate = useNavigate();

    function logout() {
        auth.logout();
        navigate("/login");
    }

    return (
        <AppBar
            position="fixed"
            elevation={1}
            color="inherit"
        >
            <Toolbar>
                <Typography
                    variant="h6"
                    sx={{ fontWeight: 700 }}
                >
                  AIWorkHub
                </Typography>

                <Box sx={{ flexGrow: 1 }} />
                <Button
                    onClick={logout}
                >
                    Logout
                </Button>
            </Toolbar>
        </AppBar>

    );

}
