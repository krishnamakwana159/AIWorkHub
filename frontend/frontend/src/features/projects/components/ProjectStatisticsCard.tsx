import {
    Card,
    CardContent,
    Grid,
    Typography
} from "@mui/material";

import type { Project } from "../types/project";

type Props = {
    project: Project;
};

export default function ProjectStatisticsCard({
    project
}: Props) {

    return (
        <Card>
            <CardContent>
                <Typography
                    variant="h6"
                    gutterBottom
                >
                    Statistics
                </Typography>

                <Grid
                    container
                    spacing={3}
                >
                    <Grid size={3}>
                        <Typography
                            variant="h4"
                        >
                            {project.progress}%
                        </Typography>

                        <Typography
                            color="text.secondary"
                        >
                            Progress
                        </Typography>

                    </Grid>

                    <Grid size={3}>
                        <Typography
                            variant="h4"
                        >
                            {project.isFavorite
                                ? "Yes"
                                : "No"}
                        </Typography>

                        <Typography
                            color="text.secondary"
                        >
                            Favorite
                        </Typography>

                    </Grid>

                    <Grid size={3}>
                        <Typography
                            variant="h4"
                        >
                            {project.isArchived
                                ? "Yes"
                                : "No"}
                        </Typography>

                        <Typography
                            color="text.secondary"
                        >
                            Archived
                        </Typography>
                    </Grid>

                    <Grid size={3}>
                        <Typography
                            variant="h4"
                        >
                            {project.status}
                        </Typography>

                        <Typography
                            color="text.secondary"
                        >
                            Status
                        </Typography>
                    </Grid>
                </Grid>
            </CardContent>
        </Card>
    );
}
