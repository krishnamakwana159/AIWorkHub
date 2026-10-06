import {
    Box,
    Chip,
    IconButton,
    Stack,
    Tooltip,
    Typography
} from "@mui/material";

import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import EditIcon from "@mui/icons-material/Edit";
import StarIcon from "@mui/icons-material/Star";
import StarBorderIcon from "@mui/icons-material/StarBorder";
import ArchiveIcon from "@mui/icons-material/Archive";
import UnarchiveIcon from "@mui/icons-material/Unarchive";

import { useNavigate } from "react-router-dom";

import {
    ProjectPriorityInfo,
    ProjectStatusInfo
} from "@/shared/constants/project";

import type { Project } from "../types/project";

type Props = {
    project: Project;
    onEdit(): void;
    onToggleFavorite(): void;
    onToggleArchive(): void;
};

export default function ProjectHeader({
    project,
    onEdit,
    onToggleFavorite,
    onToggleArchive
}: Props) {

    const navigate = useNavigate();

    return (
        <Stack
            direction={{ xs: "column", sm: "row" }}
            spacing={2}
            sx={{
                mb: 3,
                justifyContent: "space-between",
                alignItems: { xs: "flex-start", sm: "center" }
            }}
        >
            <Stack direction="row" spacing={2} sx={{ alignItems: "center", minWidth: 0 }}>
                <IconButton
                    onClick={() => navigate("/projects")}
                    sx={{ flexShrink: 0 }}
                >
                    <ArrowBackIcon />
                </IconButton>

                <Box
                    sx={{
                        width: 6,
                        alignSelf: "stretch",
                        borderRadius: 999,
                        bgcolor: project.color || "primary.main",
                        flexShrink: 0
                    }}
                />

                <Stack spacing={0.75} sx={{ minWidth: 0 }}>
                    <Stack
                        direction="row"
                        spacing={1.5}
                        sx={{ alignItems: "center", flexWrap: "wrap" }}
                    >
                        <Typography
                            variant="h5"
                            sx={{
                                fontWeight: 700,
                                overflow: "hidden",
                                textOverflow: "ellipsis",
                                whiteSpace: "nowrap"
                            }}
                        >
                            {project.name}
                        </Typography>

                        <Chip
                            size="small"
                            label={ProjectStatusInfo[project.status].label}
                            color={ProjectStatusInfo[project.status].color}
                        />

                        <Chip
                            size="small"
                            label={ProjectPriorityInfo[project.priority].label}
                            color={ProjectPriorityInfo[project.priority].color}
                        />
                    </Stack>

                    <Typography
                        variant="body2"
                        color="text.secondary"
                        sx={{
                            overflow: "hidden",
                            textOverflow: "ellipsis",
                            whiteSpace: "nowrap",
                            maxWidth: 480
                        }}
                    >
                        {project.description || "No description"}
                    </Typography>
                </Stack>
            </Stack>

            <Stack direction="row" spacing={0.5} sx={{ alignSelf: { xs: "flex-end", sm: "center" }, flexShrink: 0 }}>
                <Tooltip title={project.isFavorite ? "Remove from favorites" : "Add to favorites"}>
                    <IconButton
                        color="warning"
                        onClick={onToggleFavorite}
                    >
                        {project.isFavorite
                            ? <StarIcon />
                            : <StarBorderIcon />}
                    </IconButton>
                </Tooltip>

                <Tooltip
                    title={
                        project.isArchived
                            ? "Restore"
                            : "Archive"
                    }
                >
                    <IconButton onClick={onToggleArchive}>
                        {project.isArchived
                            ? <UnarchiveIcon />
                            : <ArchiveIcon />}
                    </IconButton>
                </Tooltip>

                <Tooltip title="Edit project">
                    <IconButton
                        color="primary"
                        onClick={onEdit}
                    >
                        <EditIcon />
                    </IconButton>
                </Tooltip>
            </Stack>
        </Stack>
    );

}
