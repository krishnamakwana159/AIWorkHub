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
    title: string;
    message: string;
    loading?: boolean;
    onClose(): void;
    onConfirm(): void;
};

export default function ConfirmDialog({
    open,
    title,
    message,
    loading = false,
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
                {title}
            </DialogTitle>

            <DialogContent>
                <DialogContentText>
                    {message}
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
                    Confirm
                </Button>
            </DialogActions>
        </Dialog>
    );
}
