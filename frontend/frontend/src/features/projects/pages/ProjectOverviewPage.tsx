import Grid from "@mui/material/Grid";
import { useOutletContext } from "react-router-dom";

import ProjectOverviewCard from "../components/ProjectOverviewCard";
import ProjectStatisticsCard from "../components/ProjectStatisticsCard";

import type { ProjectOutletContext } from "./ProjectLayout";

export default function ProjectOverviewPage() {
  const { project } = useOutletContext<ProjectOutletContext>();

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
