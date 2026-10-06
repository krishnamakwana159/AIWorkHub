import { useEffect, useState } from "react";

import {
    Button,
    Card,
    CardContent,
    Chip,
    Divider,
    List,
    ListItem,
    ListItemText,
    Stack,
    Typography
} from "@mui/material";

import PlayArrowIcon from "@mui/icons-material/PlayArrow";
import StopIcon from "@mui/icons-material/Stop";

import { useRunningTimer } from "../hooks/useRunningTimer";
import { useStartTimer } from "../hooks/useStartTimer";
import { useStopTimer } from "../hooks/useStopTimer";
import { useTaskTimeEntries } from "../hooks/useTaskTimeEntries";

import ManualTimeEntryDialog from "./ManualTimeEntryDialog";

type Props = {
    taskId: string;
};

function formatElapsed(startTimeUtc: string, nowMs: number) {
    const startMs = new Date(startTimeUtc).getTime();
    const totalSeconds = Math.max(0, Math.floor((nowMs - startMs) / 1000));

    const hours = Math.floor(totalSeconds / 3600);
    const minutes = Math.floor((totalSeconds % 3600) / 60);
    const seconds = totalSeconds % 60;

    return [hours, minutes, seconds]
        .map((n) => String(n).padStart(2, "0"))
        .join(":");
}

function formatDateTime(value: string) {
    return new Date(value).toLocaleString(undefined, {
        month: "short",
        day: "numeric",
        hour: "2-digit",
        minute: "2-digit"
    });
}

export default function TaskTimerCard({ taskId }: Props) {
    const { data: runningTimer } = useRunningTimer();
    const { data: entries = [] } = useTaskTimeEntries(taskId);

    const startMutation = useStartTimer();
    const stopMutation = useStopTimer();

    const [manualOpen, setManualOpen] = useState(false);
    const [nowMs, setNowMs] = useState(() => Date.now());

    const isRunningHere = runningTimer?.workTaskId === taskId;
    const isRunningElsewhere = !!runningTimer && !isRunningHere;

    useEffect(() => {
        if (!isRunningHere) {
            return;
        }

        const interval = setInterval(() => setNowMs(Date.now()), 1000);

        return () => clearInterval(interval);
    }, [isRunningHere]);

    return (
        <Card>
            <CardContent>
                <Stack
                    direction="row"
                    sx={{ justifyContent: "space-between", alignItems: "center", mb: 2 }}
                >
                    <Typography variant="h6">Time Tracking</Typography>

                    <Button
                        variant="outlined"
                        onClick={() => setManualOpen(true)}
                    >
                        Log Time
                    </Button>
                </Stack>

                <Stack
                    direction="row"
                    spacing={2}
                    sx={{ alignItems: "center", mb: 2 }}
                >
                    {isRunningHere && (
                        <Typography variant="h4" sx={{ fontVariantNumeric: "tabular-nums" }}>
                            {formatElapsed(runningTimer.startTimeUtc, nowMs)}
                        </Typography>
                    )}

                    {isRunningHere ? (
                        <Button
                            variant="contained"
                            color="error"
                            startIcon={<StopIcon />}
                            disabled={stopMutation.isPending}
                            onClick={() => stopMutation.mutate()}
                        >
                            Stop
                        </Button>
                    ) : (
                        <Button
                            variant="contained"
                            startIcon={<PlayArrowIcon />}
                            disabled={isRunningElsewhere || startMutation.isPending}
                            onClick={() =>
                                startMutation.mutate({ workTaskId: taskId })
                            }
                        >
                            Start Timer
                        </Button>
                    )}

                    {isRunningElsewhere && (
                        <Chip
                            size="small"
                            label={`Timer running on "${runningTimer.taskTitle}"`}
                        />
                    )}
                </Stack>

                <Divider sx={{ mb: 2 }} />

                <Typography variant="subtitle2" color="text.secondary" gutterBottom>
                    History
                </Typography>

                {entries.length === 0 ? (
                    <Typography variant="body2" color="text.secondary">
                        No time logged for this task yet.
                    </Typography>
                ) : (
                    <List disablePadding>
                        {entries.map((entry) => (
                            <ListItem key={entry.id} disableGutters>
                                <ListItemText
                                    primary={`${entry.hours.toFixed(2)}h — ${entry.userName}`}
                                    secondary={
                                        entry.isRunning
                                            ? "In progress"
                                            : `${formatDateTime(entry.startTimeUtc)}${
                                                  entry.endTimeUtc
                                                      ? ` – ${formatDateTime(entry.endTimeUtc)}`
                                                      : ""
                                              }${entry.description ? ` · ${entry.description}` : ""}`
                                    }
                                />
                            </ListItem>
                        ))}
                    </List>
                )}
            </CardContent>

            <ManualTimeEntryDialog
                open={manualOpen}
                taskId={taskId}
                onClose={() => setManualOpen(false)}
            />
        </Card>
    );
}
