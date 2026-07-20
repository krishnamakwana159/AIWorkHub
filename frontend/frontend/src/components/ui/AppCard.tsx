import { Card, CardContent, type CardProps } from "@mui/material";
import type { PropsWithChildren } from "react";

type Props = PropsWithChildren<CardProps>;

export default function AppCard({ children, ...props }: Props) {
    return (
        <Card elevation={1} {...props}>
            <CardContent>{children}</CardContent>
        </Card>
    );
}
