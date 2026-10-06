import { Card, CardContent, Stack, Typography } from "@mui/material";

type Stat = {
    label: string;
    value: string | number;
};

type Props = {
    stats: Stat[];
};

export default function ReportStatsGrid({ stats }: Props) {
    return (
        <Stack
            direction="row"
            spacing={2}
            sx={{ flexWrap: "wrap" }}
        >
            {stats.map((stat) => (
                <Card
                    key={stat.label}
                    variant="outlined"
                    sx={{ minWidth: 160, flex: "1 1 160px" }}
                >
                    <CardContent>
                        <Typography variant="caption" color="text.secondary">
                            {stat.label}
                        </Typography>

                        <Typography variant="h5">{stat.value}</Typography>
                    </CardContent>
                </Card>
            ))}
        </Stack>
    );
}
