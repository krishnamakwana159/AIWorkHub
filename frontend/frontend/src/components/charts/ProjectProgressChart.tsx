import {
    Bar,
    BarChart,
    CartesianGrid,
    Cell,
    ResponsiveContainer,
    Tooltip,
    XAxis,
    YAxis
} from "recharts";

import { Card, CardContent, Typography } from "@mui/material";

export interface ProjectProgressPoint {
    projectId: string;
    projectName: string;
    progress: number;
}

type Props = {
    data: ProjectProgressPoint[];
};

function colorFor(progress: number) {
    if (progress >= 75) return "#16a34a";
    if (progress >= 40) return "#2563eb";
    if (progress > 0) return "#f59e0b";
    return "#94a3b8";
}

export default function ProjectProgressChart({ data }: Props) {
    const chartData = data.slice(0, 10);
    const height = Math.max(220, chartData.length * 44);

    return (
        <Card>
            <CardContent>
                <Typography variant="h6" gutterBottom>
                    Project Progress
                </Typography>

                {chartData.length === 0 ? (
                    <Typography variant="body2" color="text.secondary">
                        No projects yet.
                    </Typography>
                ) : (
                    <ResponsiveContainer width="100%" height={height}>
                        <BarChart
                            data={chartData}
                            layout="vertical"
                            margin={{ left: 24 }}
                        >
                            <CartesianGrid strokeDasharray="3 3" horizontal={false} />

                            <XAxis
                                type="number"
                                domain={[0, 100]}
                                unit="%"
                            />

                            <YAxis
                                type="category"
                                dataKey="projectName"
                                width={140}
                            />

                            <Tooltip
                                formatter={(value) => `${value}%`}
                            />

                            <Bar dataKey="progress" radius={[0, 4, 4, 0]}>
                                {chartData.map((entry) => (
                                    <Cell
                                        key={entry.projectId}
                                        fill={colorFor(entry.progress)}
                                    />
                                ))}
                            </Bar>
                        </BarChart>
                    </ResponsiveContainer>
                )}
            </CardContent>
        </Card>
    );
}
