import { Box } from "@mui/material";
import type { PropsWithChildren } from "react";

export default function PageContainer({
    children
}: PropsWithChildren) {

    return (

        <Box
            sx={{
                p: 3
            }}
        >
            {children}
        </Box>

    );

}
