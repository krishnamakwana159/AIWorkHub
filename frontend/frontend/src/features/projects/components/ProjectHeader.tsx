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
            sx={{ mb: 3, justifyContent:"space-between", direction:"row", alignItems:"center" }}
        >
            <Stack spacing={1}>
                <Stack
                    sx={{direction:"row",
                    spacing:1,
                    alignItems:"center"}}
                >
                    <IconButton
                        onClick={() => navigate("/projects")}
                    >
                        <ArrowBackIcon />
                    </IconButton>

                    <Typography
                        sx={{variant:"h4",
                        fontWeight:700}}
                    >
                        {project.name}
                    </Typography>

                    <Tooltip title="Favorite">

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

                </Stack>

                <Typography color="text.secondary">

                    {project.description || "No description"}

                </Typography>

                <Stack
                    direction="row"
                    spacing={1}
                >

                    <Chip
                        label={
                            ProjectStatusInfo[
                                project.status
                            ].label
                        }
                        color={
                            ProjectStatusInfo[
                                project.status
                            ].color
                        }
                    />

                    <Chip
                        label={
                            ProjectPriorityInfo[
                                project.priority
                            ].label
                        }
                        color={
                            ProjectPriorityInfo[
                                project.priority
                            ].color
                        }
                    />

                </Stack>

            </Stack>

            <Box>

                <IconButton
                    color="primary"
                    onClick={onEdit}
                >
                    <EditIcon />
                </IconButton>

            </Box>

        </Stack>

    );

}
