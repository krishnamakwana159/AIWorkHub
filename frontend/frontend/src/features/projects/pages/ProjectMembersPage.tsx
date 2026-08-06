import { useState } from "react";
import { useOutletContext } from "react-router-dom";
import {
    Button,
    Chip,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    MenuItem,
    Paper,
    Stack,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    TextField
} from "@mui/material";

import AppLoader from "@/components/ui/AppLoader";
import EmptyState from "@/components/ui/EmptyState";
import { ProjectRole, ProjectRoleInfo } from "@/shared/constants/project";

import { useAddProjectMember } from "../hooks/useAddProjectMember";
import { useProjectMembers } from "../hooks/useProjectMembers";
import { useUserLookup } from "../hooks/useUserLookup";
import type { ProjectOutletContext } from "./ProjectLayout";

export default function ProjectMembersPage() {
    const { project } = useOutletContext<ProjectOutletContext>();
    const [open, setOpen] = useState(false);
    const [userId, setUserId] = useState("");
    const [role, setRole] = useState<ProjectRole>(ProjectRole.Member);
    const membersQuery = useProjectMembers(project.id);
    const usersQuery = useUserLookup();
    const addMutation = useAddProjectMember(project.id);

    async function handleAdd() {
        if (!userId) {
            return;
        }

        await addMutation.mutateAsync({
            userId,
            role
        });
        setUserId("");
        setRole(ProjectRole.Member);
        setOpen(false);
    }

    if (membersQuery.isPending) {
        return <AppLoader />;
    }

    if (membersQuery.isError) {
        return <EmptyState message="Unable to load members." />;
    }

    const members = membersQuery.data ?? [];

    return (
        <Stack spacing={2}>
            <Stack
                sx={{
                    display: "flex",
                    flexDirection: "row",
                    justifyContent: "flex-end"
                }}
            >
                <Button variant="contained" onClick={() => setOpen(true)}>
                    Add Member
                </Button>
            </Stack>

            {members.length === 0 ? (
                <EmptyState message="No members added yet." />
            ) : (
                <TableContainer component={Paper} variant="outlined">
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Name</TableCell>
                                <TableCell>Email</TableCell>
                                <TableCell>Role</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {members.map((member) => (
                                <TableRow key={member.userId}>
                                    <TableCell>{member.fullName}</TableCell>
                                    <TableCell>{member.email}</TableCell>
                                    <TableCell>
                                        <Chip label={ProjectRoleInfo[member.role]} size="small" />
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}

            <Dialog open={open} onClose={() => setOpen(false)} maxWidth="xs" fullWidth>
                <DialogTitle>Add Project Member</DialogTitle>
                <DialogContent>
                    <Stack spacing={2} sx={{ mt: 1 }}>
                        <TextField
                            select
                            label="User"
                            value={userId}
                            onChange={(event) => setUserId(event.target.value)}
                            disabled={usersQuery.isPending}
                            fullWidth
                        >
                            {(usersQuery.data ?? []).map((user) => (
                                <MenuItem key={user.id} value={user.id}>
                                    {user.fullName} ({user.email})
                                </MenuItem>
                            ))}
                        </TextField>

                        <TextField
                            select
                            label="Role"
                            value={role}
                            onChange={(event) => setRole(Number(event.target.value) as ProjectRole)}
                            fullWidth
                        >
                            {Object.values(ProjectRole).map((value) => (
                                <MenuItem key={value} value={value}>
                                    {ProjectRoleInfo[value]}
                                </MenuItem>
                            ))}
                        </TextField>
                    </Stack>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => setOpen(false)} disabled={addMutation.isPending}>
                        Cancel
                    </Button>
                    <Button
                        variant="contained"
                        onClick={handleAdd}
                        disabled={!userId || addMutation.isPending}
                    >
                        Add
                    </Button>
                </DialogActions>
            </Dialog>
        </Stack>
    );
}
