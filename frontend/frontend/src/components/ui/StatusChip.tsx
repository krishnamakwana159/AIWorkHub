import Chip from "@mui/material/Chip";

type Props = {
    status: string;
};

export default function StatusChip({ status }: Props) {

    const color =
        status === "Completed"
            ? "success"
            : status === "Active"
            ? "primary"
            : status === "Planning"
            ? "info"
            : status === "OnHold"
            ? "warning"
            : "default";

    return (
        <Chip
            label={status}
            color={color}
            size="small"
        />
    );
}
