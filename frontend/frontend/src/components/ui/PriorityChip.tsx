import Chip from "@mui/material/Chip";

type Props = {
    priority: string;
};

export default function PriorityChip({ priority }: Props) {

    const color =
        priority === "Critical"
            ? "error"
            : priority === "High"
            ? "warning"
            : priority === "Medium"
            ? "info"
            : "success";

    return (
        <Chip
            label={priority}
            color={color}
            size="small"
        />
    );
}
