import {
    Card,
    CardContent,
    Divider,
    Grid,
    LinearProgress,
    Stack,
    Typography
} from "@mui/material";

import type { Project } from "../types/project";

type Props = {
    project: Project;
};

export default function ProjectOverviewCard({
    project
}: Props) {

    return (

        <Card>
            <CardContent>
                <Typography
                    variant="h6"
                    gutterBottom
                >
                    Overview
                </Typography>

                <Divider sx={{ mb: 3 }} />

                <Grid
                    container
                    spacing={3}
                >
                    <Grid size={6}>
                        <Stack spacing={1}>
                            <Typography
                                variant="body2"
                                color="text.secondary"
                            >
                                Progress
                            </Typography>

                            <LinearProgress
                                variant="determinate"
                                value={project.progress}
                            />
                            <Typography>
                                {project.progress}%
                            </Typography>
                        </Stack>
                    </Grid>

                    <Grid size={6}>
                        <Stack spacing={2}>
                            <Typography>
                                <strong>Created</strong>
                            </Typography>

                            <Typography>
                                {new Date(
                                    project.createdAtUtc
                                ).toLocaleDateString()}
                            </Typography>

                            <Typography>
                                <strong>Start Date</strong>
                            </Typography>

                            <Typography>
                                {project.startDateUtc
                                    ? new Date(
                                        project.startDateUtc
                                    ).toLocaleDateString()
                                    : "-"}
                            </Typography>

                            <Typography>
                                <strong>Target Date</strong>
                            </Typography>
                            <Typography>
                                {project.targetCompletionDateUtc
                                    ? new Date(
                                        project.targetCompletionDateUtc
                                    ).toLocaleDateString()
                                    : "-"}
                            </Typography>
                        </Stack>
                    </Grid>
                </Grid>
            </CardContent>
        </Card>
    );
}
