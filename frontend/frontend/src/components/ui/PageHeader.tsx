import {
    Box,
    Stack,
    Typography
} from "@mui/material";
import type { ReactNode } from "react";

type Props = {
    title: string;
    subtitle?: string;
    action?: ReactNode;
};

export default function PageHeader({
    title,
    subtitle,
    action

}: Props) {

    return (

        <Box sx={{ mb:4 }}>
            <Stack
                sx={{
                  direction: "row",
                  justifyContent: "space-between",
                  alignItems: "center"}}
            >
                <Box>
                    <Typography
                        variant="h4"
                        sx={{
                          fontWeight: 700
                        }}
                    >
                        {title}
                    </Typography>

                    {subtitle && (
                        <Typography
                            color="text.secondary"
                        >
                            {subtitle}
                        </Typography>
                    )}

                </Box>

                {action}

            </Stack>

        </Box>

    );

}
