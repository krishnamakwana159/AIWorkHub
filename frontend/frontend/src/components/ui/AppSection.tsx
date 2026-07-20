import { Stack, Typography } from "@mui/material";
import type { PropsWithChildren, ReactNode } from "react";

type Props = PropsWithChildren<{
    title: string;
    action?: ReactNode;
}>;

export default function AppSection({
    title,
    action,
    children
}: Props) {
    return (
        <Stack spacing={2}>
            <Stack
                sx={{direction:"row",
                justifyContent:"space-between",
                alignItems:"center"}}
            >
                <Typography variant="h6">
                    {title}
                </Typography>

                {action}
            </Stack>

            {children}
        </Stack>
    );
}
