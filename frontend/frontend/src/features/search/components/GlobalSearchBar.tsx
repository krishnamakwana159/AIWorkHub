import { useState } from "react";
import { useNavigate } from "react-router-dom";

import {
    Box,
    ClickAwayListener,
    InputAdornment,
    InputBase,
    List,
    ListItemButton,
    ListItemText,
    Paper,
    Popper,
    Typography
} from "@mui/material";
import SearchIcon from "@mui/icons-material/Search";

import { useGlobalSearch } from "../hooks/useGlobalSearch";

export default function GlobalSearchBar() {
    const navigate = useNavigate();
    const [anchorEl, setAnchorEl] = useState<HTMLDivElement | null>(null);

    const [query, setQuery] = useState("");
    const [open, setOpen] = useState(false);

    const { data, isFetching } = useGlobalSearch(query);

    const hasResults =
        !!data &&
        (data.projects.length > 0 ||
            data.tasks.length > 0 ||
            data.users.length > 0 ||
            data.comments.length > 0);

    function go(path: string) {
        navigate(path);
        setOpen(false);
        setQuery("");
    }

    return (
        <ClickAwayListener onClickAway={() => setOpen(false)}>
            <Box ref={setAnchorEl} sx={{ width: 360 }}>
                <Paper
                    variant="outlined"
                    sx={{
                        display: "flex",
                        alignItems: "center",
                        px: 1.5,
                        height: 40,
                        borderRadius: 999,
                        borderColor: open ? "primary.main" : "divider",
                        bgcolor: "action.hover",
                        transition: "border-color 0.15s ease"
                    }}
                >
                    <InputAdornment position="start">
                        <SearchIcon fontSize="small" sx={{ color: "text.secondary" }} />
                    </InputAdornment>

                    <InputBase
                        placeholder="Search projects, tasks, people..."
                        value={query}
                        onChange={(e) => {
                            setQuery(e.target.value);
                            setOpen(true);
                        }}
                        onFocus={() => setOpen(true)}
                        fullWidth
                        sx={{ ml: 1, fontSize: 14 }}
                    />
                </Paper>

                <Popper
                    open={open && query.trim().length >= 2}
                    anchorEl={anchorEl}
                    placement="bottom-start"
                    sx={{ zIndex: 1300, width: anchorEl?.offsetWidth }}
                >
                    <Paper elevation={6} sx={{ mt: 1, borderRadius: 3, maxHeight: 420, overflowY: "auto" }}>
                        {isFetching && (
                            <Typography variant="body2" color="text.secondary" sx={{ p: 2 }}>
                                Searching...
                            </Typography>
                        )}

                        {!isFetching && !hasResults && (
                            <Typography variant="body2" color="text.secondary" sx={{ p: 2 }}>
                                No results found.
                            </Typography>
                        )}

                        {!isFetching && data && data.projects.length > 0 && (
                            <List
                                subheader={
                                    <Typography variant="overline" sx={{ px: 2, color: "text.secondary" }}>
                                        Projects
                                    </Typography>
                                }
                                dense
                            >
                                {data.projects.map((project) => (
                                    <ListItemButton
                                        key={project.id}
                                        onClick={() => go(`/projects/${project.id}`)}
                                    >
                                        <ListItemText
                                            primary={project.name}
                                            secondary={project.status}
                                        />
                                    </ListItemButton>
                                ))}
                            </List>
                        )}

                        {!isFetching && data && data.tasks.length > 0 && (
                            <List
                                subheader={
                                    <Typography variant="overline" sx={{ px: 2, color: "text.secondary" }}>
                                        Tasks
                                    </Typography>
                                }
                                dense
                            >
                                {data.tasks.map((task) => (
                                    <ListItemButton
                                        key={task.id}
                                        onClick={() => go(`/tasks/${task.id}`)}
                                    >
                                        <ListItemText
                                            primary={task.title}
                                            secondary={task.status}
                                        />
                                    </ListItemButton>
                                ))}
                            </List>
                        )}

                        {!isFetching && data && data.users.length > 0 && (
                            <List
                                subheader={
                                    <Typography variant="overline" sx={{ px: 2, color: "text.secondary" }}>
                                        People
                                    </Typography>
                                }
                                dense
                            >
                                {data.users.map((user) => (
                                    <ListItemButton key={user.id} disableRipple sx={{ cursor: "default" }}>
                                        <ListItemText
                                            primary={user.fullName}
                                            secondary={user.email}
                                        />
                                    </ListItemButton>
                                ))}
                            </List>
                        )}

                        {!isFetching && data && data.comments.length > 0 && (
                            <List
                                subheader={
                                    <Typography variant="overline" sx={{ px: 2, color: "text.secondary" }}>
                                        Comments
                                    </Typography>
                                }
                                dense
                            >
                                {data.comments.map((comment) => (
                                    <ListItemButton
                                        key={comment.id}
                                        onClick={() => go(`/tasks/${comment.taskId}`)}
                                    >
                                        <ListItemText
                                            primary={comment.comment}
                                            slotProps={{
                                                primary: {
                                                    sx: {
                                                        overflow: "hidden",
                                                        textOverflow: "ellipsis",
                                                        whiteSpace: "nowrap"
                                                    }
                                                }
                                            }}
                                        />
                                    </ListItemButton>
                                ))}
                            </List>
                        )}
                    </Paper>
                </Popper>
            </Box>
        </ClickAwayListener>
    );
}
