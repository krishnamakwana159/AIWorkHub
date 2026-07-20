import {
    Stack,
    Typography
} from "@mui/material";

type Props = {
    title: string;
    subtitle?: string;
};

export default function SectionHeader({
    title,
    subtitle
}: Props) {

    return (

        <Stack
            spacing={0.5}
            sx={{ mb:3}}
        >
            <Typography
                variant="h5"
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
        </Stack>
    );
}
