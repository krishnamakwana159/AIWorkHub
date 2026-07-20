import Typography from "@mui/material/Typography";

type Props = {
    title: string;
    subtitle?: string;
};

export default function PageTitle({
    title,
    subtitle
}: Props) {

    return (
        <>
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
                    sx={{
                        mt: 1
                    }}
                >
                    {subtitle}
                </Typography>
            )}
        </>
    );
}
