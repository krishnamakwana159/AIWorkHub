import {
    Box,
    CircularProgress
} from "@mui/material";

export default function AppLoader() {

    return (

        <Box
            sx={{
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                py: 8
            }}
        >
            <CircularProgress />
        </Box>
    );
}
