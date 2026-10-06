import {
    Bar,
    BarChart,
    CartesianGrid,
    Legend,
    ResponsiveContainer,
    Tooltip,
    XAxis,
    YAxis
} from "recharts";

import { Card, CardContent, Typography } from "@mui/material";

export interface TeamPerformancePoint {
    userId: string;
    userName: string;
    assignedTasks: number;
    completedTasks: number;
}

type Props = {
    data: TeamPerformancePoint[];
};

export default function TeamPerformanceChart({ data }: Props) {
    const chartData = data.slice(0, 10);

    return (
        <Card>
            <CardContent>
                <Typography variant="h6" gutterBottom>
                    Team Performance
                </Typography>

                {chartData.length === 0 ? (
                    <Typography variant="body2" color="text.secondary">
                        No team activity yet.
                    </Typography>
                ) : (
                    <ResponsiveContainer width="100%" height={320}>
                        <BarChart data={chartData}>
                            <CartesianGrid strokeDasharray="3 3" />

                            <XAxis
                                dataKey="userName"
                                interval={0}
                                angle={-20}
                                textAnchor="end"
                                height={60}
                            />

                            <YAxis allowDecimals={false} />

                            <Tooltip />
                            <Legend />

                            <Bar
                                dataKey="assignedTasks"
                                name="Assigned"
                                fill="#94a3b8"
                                radius={[4, 4, 0, 0]}
                            />

                            <Bar
                                dataKey="completedTasks"
                                name="Completed"
                                fill="#16a34a"
                                radius={[4, 4, 0, 0]}
                            />
                        </BarChart>
                    </ResponsiveContainer>
                )}
            </CardContent>
        </Card>
    );
}
