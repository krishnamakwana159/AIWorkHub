import {
    Stack,
    Typography
} from "@mui/material";

import AppCard from "./AppCard";

type Props = {
    title: string;
    value: number | string;
    subtitle?: string;
};

export default function StatCard({
    title,
    value,
    subtitle
}: Props) {

    return (

        <AppCard>

            <Stack spacing={1}>

                <Typography
                    color="text.secondary"
                    variant="body2"
                >
                    {title}
                </Typography>

                <Typography
                    variant="h4"
                    sx={{
                      fontWeight: 700
                    }}
                >
                    {value}
                </Typography>

                {subtitle && (
                    <Typography
                        variant="caption"
                        color="text.secondary"
                    >
                        {subtitle}
                    </Typography>

                )}
            </Stack>
        </AppCard>

    );

}
