import { Cell, Legend, Pie, PieChart, ResponsiveContainer, Tooltip } from "recharts";

import { Card, CardContent, Typography } from "@mui/material";

export interface StatusSlice {
    name: string;
    value: number;
    color: string;
}

type Props = {
    title: string;
    data: StatusSlice[];
};

export default function StatusDonutChart({ title, data }: Props) {
    const chartData = data.filter((slice) => slice.value > 0);

    return (
        <Card>
            <CardContent>
                <Typography variant="h6" gutterBottom>
                    {title}
                </Typography>

                {chartData.length === 0 ? (
                    <Typography variant="body2" color="text.secondary">
                        No tasks to break down yet.
                    </Typography>
                ) : (
                    <ResponsiveContainer width="100%" height={260}>
                        <PieChart>
                            <Pie
                                data={chartData}
                                dataKey="value"
                                nameKey="name"
                                innerRadius={60}
                                outerRadius={90}
                                paddingAngle={2}
                            >
                                {chartData.map((slice) => (
                                    <Cell key={slice.name} fill={slice.color} />
                                ))}
                            </Pie>

                            <Tooltip />
                            <Legend />
                        </PieChart>
                    </ResponsiveContainer>
                )}
            </CardContent>
        </Card>
    );
}
