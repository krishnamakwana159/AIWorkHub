import { useOutletContext } from "react-router-dom";

import ProjectTasksTab from "../components/ProjectTasksTab";
import type { ProjectOutletContext } from "./ProjectLayout";

export default function ProjectTasksPage() {
  const { project } = useOutletContext<ProjectOutletContext>();

  return <ProjectTasksTab project={project} />;
}
