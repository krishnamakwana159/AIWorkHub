import { Box } from "@mui/material";
import type { PropsWithChildren } from "react";

export default function AuthLayout({
    children
}: PropsWithChildren) {
    return (
        <Box
            sx={{
                minHeight: "100vh",
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                backgroundColor: "grey.100"
            }}
        >
            {children}
        </Box>
    );
}
