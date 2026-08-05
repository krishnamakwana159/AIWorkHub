import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    DialogTitle
} from "@mui/material";

type Props = {
    open: boolean;
    taskTitle: string;
    loading: boolean;
    onClose(): void;
    onConfirm(): void;
};

export default function DeleteTaskDialog({
    open,
    taskTitle,
    loading,
    onClose,
    onConfirm
}: Props) {
    return (
        <Dialog
            open={open}
            onClose={onClose}
            maxWidth="xs"
            fullWidth
        >
            <DialogTitle>
                Delete Task
            </DialogTitle>

            <DialogContent>
                <DialogContentText>
                    Are you sure you want to delete{" "}
                    <strong>{taskTitle}</strong>?
                </DialogContentText>
            </DialogContent>

            <DialogActions>

                <Button
                    onClick={onClose}
                    disabled={loading}
                >
                    Cancel
                </Button>

                <Button
                    color="error"
                    variant="contained"
                    onClick={onConfirm}
                    disabled={loading}
                >
                    Delete
                </Button>

            </DialogActions>

        </Dialog>
    );
}
