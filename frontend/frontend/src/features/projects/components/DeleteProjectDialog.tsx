import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    Typography
} from "@mui/material";

type Props = {
    open: boolean;
    projectName: string;
    loading?: boolean;
    onClose(): void;
    onConfirm(): void;
};

export default function DeleteProjectDialog({
    open,
    projectName,
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
                Delete Project
            </DialogTitle>

            <DialogContent>
                <Typography>
                    Are you sure you want to delete
                    <strong> {projectName}</strong>?
                </Typography>
            </DialogContent>

            <DialogActions>
                <Button
                    onClick={onClose}
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
