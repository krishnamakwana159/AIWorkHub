import { useState } from "react";

import {
    Button,
    Card,
    CardContent,
    Chip,
    Divider,
    IconButton,
    List,
    ListItem,
    ListItemText,
    MenuItem,
    Stack,
    TextField,
    Tooltip,
    Typography
} from "@mui/material";
import AutoAwesomeIcon from "@mui/icons-material/AutoAwesome";
import ContentCopyIcon from "@mui/icons-material/ContentCopy";

import PageContainer from "@/components/common/PageContainer";
import PageTitle from "@/components/common/PageTitle";
import AppLoader from "@/components/ui/AppLoader";
import EmptyState from "@/components/ui/EmptyState";

import { useProjects } from "@/features/projects/hooks/useProjects";

import { useGenerateTaskDescription } from "../hooks/useGenerateTaskDescription";
import { useGenerateTaskBreakdown } from "../hooks/useGenerateTaskBreakdown";
import { useSuggestTaskPriority } from "../hooks/useSuggestTaskPriority";
import { useProjectSummary } from "../hooks/useProjectSummary";

function CopyButton({ text }: { text: string }) {
    return (
        <Tooltip title="Copy to clipboard">
            <IconButton
                size="small"
                onClick={() => navigator.clipboard.writeText(text)}
            >
                <ContentCopyIcon fontSize="small" />
            </IconButton>
        </Tooltip>
    );
}

function DescriptionGenerator() {
    const [title, setTitle] = useState("");
    const mutation = useGenerateTaskDescription();

    return (
        <Card>
            <CardContent>
                <Typography variant="h6" gutterBottom>
                    Generate Task Description
                </Typography>

                <Divider sx={{ mb: 3 }} />

                <Stack spacing={2}>
                    <TextField
                        label="Task Title"
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                        fullWidth
                    />

                    <Button
                        variant="contained"
                        startIcon={<AutoAwesomeIcon />}
                        disabled={!title.trim() || mutation.isPending}
                        onClick={() => mutation.mutate({ title })}
                        sx={{ alignSelf: "flex-start" }}
                    >
                        Generate
                    </Button>

                    {mutation.isPending && <AppLoader />}

                    {mutation.data && (
                        <Stack
                            direction="row"
                            sx={{ justifyContent: "space-between", alignItems: "flex-start", gap: 1 }}
                        >
                            <Typography sx={{ whiteSpace: "pre-wrap" }}>
                                {mutation.data}
                            </Typography>

                            <CopyButton text={mutation.data} />
                        </Stack>
                    )}
                </Stack>
            </CardContent>
        </Card>
    );
}

function BreakdownAndPriority() {
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");

    const breakdownMutation = useGenerateTaskBreakdown();
    const priorityMutation = useSuggestTaskPriority();

    return (
        <Card>
            <CardContent>
                <Typography variant="h6" gutterBottom>
                    Task Breakdown &amp; Priority
                </Typography>

                <Divider sx={{ mb: 3 }} />

                <Stack spacing={2}>
                    <TextField
                        label="Task Title"
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                        fullWidth
                    />

                    <TextField
                        label="Task Description"
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                        multiline
                        rows={3}
                        fullWidth
                    />

                    <Stack direction="row" spacing={1}>
                        <Button
                            variant="contained"
                            startIcon={<AutoAwesomeIcon />}
                            disabled={!title.trim() || breakdownMutation.isPending}
                            onClick={() =>
                                breakdownMutation.mutate({ title, description })
                            }
                        >
                            Generate Breakdown
                        </Button>

                        <Button
                            variant="outlined"
                            startIcon={<AutoAwesomeIcon />}
                            disabled={!title.trim() || priorityMutation.isPending}
                            onClick={() =>
                                priorityMutation.mutate({ title, description })
                            }
                        >
                            Suggest Priority
                        </Button>
                    </Stack>

                    {priorityMutation.data && (
                        <Chip
                            label={`Suggested priority: ${priorityMutation.data}`}
                            color="primary"
                            sx={{ alignSelf: "flex-start" }}
                        />
                    )}

                    {breakdownMutation.isPending && <AppLoader />}

                    {breakdownMutation.data && breakdownMutation.data.length > 0 && (
                        <List disablePadding>
                            {breakdownMutation.data.map((step, index) => (
                                <ListItem key={index} disableGutters>
                                    <ListItemText primary={`${index + 1}. ${step}`} />
                                </ListItem>
                            ))}
                        </List>
                    )}
                </Stack>
            </CardContent>
        </Card>
    );
}

function ProjectSummaryGenerator() {
    const { data: projects = [] } = useProjects({ page: 1, pageSize: 100 });
    const [projectId, setProjectId] = useState("");

    const { data: summary, isPending, isError } = useProjectSummary(
        projectId || undefined
    );

    return (
        <Card>
            <CardContent>
                <Stack
                    direction="row"
                    sx={{ justifyContent: "space-between", alignItems: "center", mb: 2 }}
                >
                    <Typography variant="h6">Project Summary</Typography>

                    <TextField
                        select
                        size="small"
                        label="Project"
                        value={projectId}
                        onChange={(e) => setProjectId(e.target.value)}
                        sx={{ minWidth: 220 }}
                    >
                        {projects.map((project) => (
                            <MenuItem key={project.id} value={project.id}>
                                {project.name}
                            </MenuItem>
                        ))}
                    </TextField>
                </Stack>

                <Divider sx={{ mb: 3 }} />

                {!projectId && (
                    <EmptyState message="Select a project to generate an AI summary." />
                )}

                {projectId && isPending && <AppLoader />}

                {projectId && isError && (
                    <EmptyState message="Unable to generate a summary for this project." />
                )}

                {projectId && summary && (
                    <Stack
                        direction="row"
                        sx={{ justifyContent: "space-between", alignItems: "flex-start", gap: 1 }}
                    >
                        <Typography sx={{ whiteSpace: "pre-wrap" }}>
                            {summary}
                        </Typography>

                        <CopyButton text={summary} />
                    </Stack>
                )}
            </CardContent>
        </Card>
    );
}

export default function AIPage() {
    return (
        <PageContainer>
            <PageTitle
                title="AI Assistant"
                subtitle="AI powered productivity"
            />

            <Stack spacing={4} sx={{ mt: 3 }}>
                <DescriptionGenerator />
                <BreakdownAndPriority />
                <ProjectSummaryGenerator />
            </Stack>
        </PageContainer>
    );
}
