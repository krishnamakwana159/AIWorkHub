import {
    Avatar,
    Card,
    CardContent,
    Chip,
    List,
    ListItem,
    ListItemAvatar,
    ListItemText,
    Typography
} from "@mui/material";

export interface TopPerformerPoint {
    userId: string;
    userName: string;
    productivityScore: number;
}

type Props = {
    data: TopPerformerPoint[];
};

const MEDAL_COLORS = ["#eab308", "#94a3b8", "#b45309"];

export default function TopPerformersList({ data }: Props) {
    return (
        <Card>
            <CardContent>
                <Typography variant="h6" gutterBottom>
                    Top Performers
                </Typography>

                {data.length === 0 ? (
                    <Typography variant="body2" color="text.secondary">
                        No data yet.
                    </Typography>
                ) : (
                    <List disablePadding>
                        {data.map((performer, index) => (
                            <ListItem key={performer.userId} disableGutters>
                                <ListItemAvatar>
                                    <Avatar
                                        sx={{
                                            bgcolor: MEDAL_COLORS[index] ?? "grey.400",
                                            width: 32,
                                            height: 32,
                                            fontSize: 14
                                        }}
                                    >
                                        {index + 1}
                                    </Avatar>
                                </ListItemAvatar>

                                <ListItemText primary={performer.userName} />

                                <Chip
                                    size="small"
                                    label={`${performer.productivityScore} pts`}
                                    color="primary"
                                    variant="outlined"
                                />
                            </ListItem>
                        ))}
                    </List>
                )}
            </CardContent>
        </Card>
    );
}
