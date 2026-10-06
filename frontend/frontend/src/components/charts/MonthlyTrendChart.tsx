import {
    Bar,
    CartesianGrid,
    ComposedChart,
    Legend,
    Line,
    ResponsiveContainer,
    Tooltip,
    XAxis,
    YAxis
} from "recharts";

import { Card, CardContent, Typography } from "@mui/material";

const MONTH_LABELS = [
    "Jan", "Feb", "Mar", "Apr", "May", "Jun",
    "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
];

export interface MonthlyTrendPoint {
    year: number;
    month: number;
    tasksCreated: number;
    tasksCompleted: number;
    hoursLogged: number;
}

type Props = {
    data: MonthlyTrendPoint[];
};

export default function MonthlyTrendChart({ data }: Props) {
    const chartData = data.map((point) => ({
        ...point,
        label: `${MONTH_LABELS[point.month - 1]} ${point.year}`
    }));

    return (
        <Card>
            <CardContent>
                <Typography variant="h6" gutterBottom>
                    Monthly Trend
                </Typography>

                {chartData.length === 0 ? (
                    <Typography variant="body2" color="text.secondary">
                        Not enough data yet to show a trend.
                    </Typography>
                ) : (
                    <ResponsiveContainer width="100%" height={320}>
                        <ComposedChart data={chartData}>
                            <CartesianGrid strokeDasharray="3 3" />

                            <XAxis dataKey="label" />

                            <YAxis
                                yAxisId="tasks"
                                allowDecimals={false}
                                label={{
                                    value: "Tasks",
                                    angle: -90,
                                    position: "insideLeft"
                                }}
                            />

                            <YAxis
                                yAxisId="hours"
                                orientation="right"
                                label={{
                                    value: "Hours",
                                    angle: 90,
                                    position: "insideRight"
                                }}
                            />

                            <Tooltip />
                            <Legend />

                            <Bar
                                yAxisId="tasks"
                                dataKey="tasksCreated"
                                name="Created"
                                fill="#94a3b8"
                                radius={[4, 4, 0, 0]}
                            />

                            <Bar
                                yAxisId="tasks"
                                dataKey="tasksCompleted"
                                name="Completed"
                                fill="#2563eb"
                                radius={[4, 4, 0, 0]}
                            />

                            <Line
                                yAxisId="hours"
                                type="monotone"
                                dataKey="hoursLogged"
                                name="Hours Logged"
                                stroke="#f59e0b"
                                strokeWidth={2}
                                dot={false}
                            />
                        </ComposedChart>
                    </ResponsiveContainer>
                )}
            </CardContent>
        </Card>
    );
}
