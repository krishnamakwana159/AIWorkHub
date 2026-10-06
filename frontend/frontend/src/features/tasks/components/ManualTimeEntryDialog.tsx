import { useState } from "react";

import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    Stack,
    TextField
} from "@mui/material";

import { useAddManualTimeEntry } from "../hooks/useAddManualTimeEntry";

type Props = {
    open: boolean;
    taskId: string;
    onClose(): void;
};

function toLocalInputValue(date: Date) {
    const offset = date.getTimezoneOffset();
    const local = new Date(date.getTime() - offset * 60000);

    return local.toISOString().slice(0, 16);
}

export default function ManualTimeEntryDialog({
    open,
    taskId,
    onClose
}: Props) {
    const now = new Date();
    const hourAgo = new Date(now.getTime() - 60 * 60 * 1000);

    const [start, setStart] = useState(toLocalInputValue(hourAgo));
    const [end, setEnd] = useState(toLocalInputValue(now));
    const [description, setDescription] = useState("");
    const [error, setError] = useState<string | null>(null);

    const mutation = useAddManualTimeEntry();

    function handleClose() {
        setError(null);
        setDescription("");
        onClose();
    }

    async function handleSubmit() {
        const startDate = new Date(start);
        const endDate = new Date(end);

        if (endDate <= startDate) {
            setError("End time must be after start time.");
            return;
        }

        setError(null);

        await mutation.mutateAsync({
            workTaskId: taskId,
            startTimeUtc: startDate.toISOString(),
            endTimeUtc: endDate.toISOString(),
            description: description || undefined
        });

        handleClose();
    }

    return (
        <Dialog open={open} onClose={handleClose} fullWidth maxWidth="xs">
            <DialogTitle>Log Time</DialogTitle>

            <DialogContent>
                <Stack spacing={2} sx={{ mt: 1 }}>
                    <TextField
                        label="Start"
                        type="datetime-local"
                        value={start}
                        onChange={(e) => setStart(e.target.value)}
                        slotProps={{ inputLabel: { shrink: true } }}
                        fullWidth
                    />

                    <TextField
                        label="End"
                        type="datetime-local"
                        value={end}
                        onChange={(e) => setEnd(e.target.value)}
                        slotProps={{ inputLabel: { shrink: true } }}
                        fullWidth
                        error={!!error}
                        helperText={error ?? undefined}
                    />

                    <TextField
                        label="Description"
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                        multiline
                        rows={3}
                        fullWidth
                    />
                </Stack>
            </DialogContent>

            <DialogActions>
                <Button onClick={handleClose}>Cancel</Button>

                <Button
                    variant="contained"
                    disabled={mutation.isPending}
                    onClick={handleSubmit}
                >
                    Log Time
                </Button>
            </DialogActions>
        </Dialog>
    );
}
