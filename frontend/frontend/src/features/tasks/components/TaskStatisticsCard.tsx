import {
  Card,
  CardContent,
  Grid,
  LinearProgress,
  Stack,
  Typography,
} from "@mui/material";

import type { WorkTask } from "../types/task";

type Props = {
  task: WorkTask;
};

export default function TaskStatisticsCard({ task }: Props) {
  const progress =
    task.estimatedHours === 0
      ? 0
      : Math.min(
          100,
          Math.round((task.actualHours / task.estimatedHours) * 100),
        );

  return (
    <Card>
      <CardContent>
        <Typography variant="h6" gutterBottom>
          Statistics
        </Typography>

        <Grid container spacing={3}>
          <Grid size={{ xs: 12, md: 4 }}>
            <Stack>
              <Typography variant="caption">Estimated Hours</Typography>

              <Typography variant="h5">{task.estimatedHours}</Typography>
            </Stack>
          </Grid>

          <Grid size={{ xs: 12, md: 4 }}>
            <Stack>
              <Typography variant="caption">Actual Hours</Typography>

              <Typography variant="h5">{task.actualHours}</Typography>
            </Stack>
          </Grid>

          <Grid size={{ xs: 12 }}>
            <Typography variant="caption">Progress</Typography>

            <LinearProgress
              variant="determinate"
              value={progress}
              sx={{
                mt: 1,
                height: 10,
                borderRadius: 2,
              }}
            />

            <Typography sx={{mt:1, variant:"body2"}}>
              {progress}%
            </Typography>
          </Grid>
        </Grid>
      </CardContent>
    </Card>
  );
}
