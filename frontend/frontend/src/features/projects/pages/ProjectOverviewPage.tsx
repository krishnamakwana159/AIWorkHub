import Grid from "@mui/material/Grid";

import ProjectOverviewCard from "../components/ProjectOverviewCard";
import ProjectStatisticsCard from "../components/ProjectStatisticsCard";

import type { Project } from "../types/project";

type Props = {
  project: Project;
};

export default function ProjectOverviewPage({ project }: Props) {
  return (
    <Grid container spacing={3}>
      <Grid size={12}>
        <ProjectOverviewCard project={project} />
      </Grid>

      <Grid size={12}>
        <ProjectStatisticsCard project={project} />
      </Grid>
    </Grid>
  );
}
