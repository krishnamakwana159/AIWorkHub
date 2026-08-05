import {
  Card,
  CardContent,
  Chip,
  Divider,
  Grid,
  Stack,
  Typography,
} from "@mui/material";

import { TaskPriorityInfo, WorkTaskStatusInfo } from "@/shared/constants/task";

import type { WorkTask } from "../types/task";

type Props = {
  task: WorkTask;
};

function formatDate(date?: string) {
  if (!date) return "-";

  return new Date(date).toLocaleDateString();
}

export default function TaskOverviewCard({ task }: Props) {
  return (
    <Card>
      <CardContent>
        <Typography variant="h6" gutterBottom>
          Task Overview
        </Typography>

        <Divider sx={{ mb: 3 }} />

        <Grid container spacing={3}>
          <Grid size={{ xs: 12, md: 6 }}>
            <Stack spacing={2}>
              <div>
                <Typography variant="caption" color="text.secondary">
                  Title
                </Typography>

                <Typography>{task.title}</Typography>
              </div>

              <div>
                <Typography variant="caption" color="text.secondary">
                  Description
                </Typography>

                <Typography>{task.description || "-"}</Typography>
              </div>

              <div>
                <Typography variant="caption" color="text.secondary">
                  Priority
                </Typography>

                <br />

                <Chip label={TaskPriorityInfo[task.priority].label} />
              </div>

              <div>
                <Typography variant="caption" color="text.secondary">
                  Status
                </Typography>

                <br />

                <Chip
                  color="primary"
                  label={WorkTaskStatusInfo[task.status].label}
                />
              </div>
            </Stack>
          </Grid>

          <Grid size={{ xs: 12, md: 6 }}>
            <Stack spacing={2}>
              <div>
                <Typography variant="caption" color="text.secondary">
                  Estimated Hours
                </Typography>

                <Typography>{task.estimatedHours}</Typography>
              </div>

              <div>
                <Typography variant="caption" color="text.secondary">
                  Actual Hours
                </Typography>

                <Typography>{task.actualHours}</Typography>
              </div>

              <div>
                <Typography variant="caption" color="text.secondary">
                  Start Date
                </Typography>

                <Typography>{formatDate(task.startDateUtc)}</Typography>
              </div>

              <div>
                <Typography variant="caption" color="text.secondary">
                  Due Date
                </Typography>

                <Typography>{formatDate(task.dueDateUtc)}</Typography>
              </div>

              <div>
                <Typography variant="caption" color="text.secondary">
                  Completed
                </Typography>

                <Typography>{formatDate(task.completedAtUtc)}</Typography>
              </div>
            </Stack>
          </Grid>
        </Grid>
      </CardContent>
    </Card>
  );
}
