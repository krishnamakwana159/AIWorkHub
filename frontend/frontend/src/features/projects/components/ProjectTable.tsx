import Grid from "@mui/material/Grid";
import ProjectCard from "./ProjectCard";
import type { Project } from "../types/project";

type Props = {
    projects: Project[];
    onView(project: Project): void;
    onEdit(project: Project): void;
    onDelete(project: Project): void;
};

export default function ProjectTable({
    projects,
    onView,
    onEdit,
    onDelete
}: Props) {
  console.log(projects)
    return (
        <Grid
            container
            spacing={3}
        >
            {projects.map(project => (
                <Grid
                    key={project.id}
                    size={{
                        xs: 12,
                        sm: 6,
                        lg: 4
                    }}
                >
                    <ProjectCard
                        project={project}
                        onView={onView}
                        onEdit={onEdit}
                        onDelete={onDelete}
                    />
                </Grid>
            ))}
        </Grid>
    );
}
