import {
    useEffect,
    useMemo,
    useRef,
    useState,
    type ReactNode
} from "react";
import { useNavigate } from "react-router-dom";

import {
    Chip,
    Dialog,
    InputAdornment,
    List,
    ListItemButton,
    ListItemIcon,
    ListItemText,
    Stack,
    TextField,
    Typography
} from "@mui/material";

import SearchIcon from "@mui/icons-material/Search";
import DashboardIcon from "@mui/icons-material/DashboardOutlined";
import FolderIcon from "@mui/icons-material/FolderOutlined";
import TaskAltIcon from "@mui/icons-material/TaskAltOutlined";
import CalendarMonthIcon from "@mui/icons-material/CalendarMonthOutlined";
import AssessmentIcon from "@mui/icons-material/AssessmentOutlined";
import AutoAwesomeIcon from "@mui/icons-material/AutoAwesomeOutlined";
import NotificationsIcon from "@mui/icons-material/NotificationsOutlined";
import SettingsIcon from "@mui/icons-material/SettingsOutlined";
import PersonIcon from "@mui/icons-material/PersonOutlined";
import AddIcon from "@mui/icons-material/Add";
import FolderCopyIcon from "@mui/icons-material/FolderOutlined";
import PersonSearchIcon from "@mui/icons-material/PersonSearchOutlined";
import CommentIcon from "@mui/icons-material/CommentOutlined";

import { useGlobalSearch } from "@/features/search/hooks/useGlobalSearch";

type PaletteItem = {
    id: string;
    label: string;
    description?: string;
    group: string;
    icon: ReactNode;
    onSelect(): void;
};

const isMac =
    typeof navigator !== "undefined" && navigator.platform.toUpperCase().includes("MAC");

export default function CommandPalette() {
    const navigate = useNavigate();

    const [open, setOpen] = useState(false);
    const [query, setQuery] = useState("");
    const [selectedIndex, setSelectedIndex] = useState(0);

    const inputRef = useRef<HTMLInputElement>(null);

    const { data: searchResults, isFetching } = useGlobalSearch(query);

    function close() {
        setOpen(false);
        setQuery("");
        setSelectedIndex(0);
    }

    function go(path: string) {
        navigate(path);
        close();
    }

    // Global shortcut: Cmd+K (Mac) / Ctrl+K (Windows, Linux)
    useEffect(() => {
        function handleKeyDown(event: KeyboardEvent) {
            const isShortcut = (event.metaKey || event.ctrlKey) && event.key.toLowerCase() === "k";

            if (isShortcut) {
                event.preventDefault();
                setOpen((prev) => !prev);
            }
        }

        document.addEventListener("keydown", handleKeyDown);
        return () => document.removeEventListener("keydown", handleKeyDown);
    }, []);

    useEffect(() => {
        if (open) {
            setTimeout(() => inputRef.current?.focus(), 50);
        }
    }, [open]);

    const navigationItems: PaletteItem[] = useMemo(
        () => [
            {
                id: "nav-new-project",
                label: "New Project",
                description: "Create a new project",
                group: "Quick Actions",
                icon: <AddIcon fontSize="small" />,
                onSelect: () => go("/projects?new=1")
            },
            {
                id: "nav-dashboard",
                label: "Go to Dashboard",
                group: "Navigate",
                icon: <DashboardIcon fontSize="small" />,
                onSelect: () => go("/dashboard")
            },
            {
                id: "nav-projects",
                label: "Go to Projects",
                group: "Navigate",
                icon: <FolderIcon fontSize="small" />,
                onSelect: () => go("/projects")
            },
            {
                id: "nav-tasks",
                label: "Go to Tasks",
                group: "Navigate",
                icon: <TaskAltIcon fontSize="small" />,
                onSelect: () => go("/tasks")
            },
            {
                id: "nav-calendar",
                label: "Go to Calendar",
                group: "Navigate",
                icon: <CalendarMonthIcon fontSize="small" />,
                onSelect: () => go("/calendar")
            },
            {
                id: "nav-reports",
                label: "Go to Reports",
                group: "Navigate",
                icon: <AssessmentIcon fontSize="small" />,
                onSelect: () => go("/reports")
            },
            {
                id: "nav-ai",
                label: "Go to AI Assistant",
                group: "Navigate",
                icon: <AutoAwesomeIcon fontSize="small" />,
                onSelect: () => go("/ai")
            },
            {
                id: "nav-notifications",
                label: "Go to Notifications",
                group: "Navigate",
                icon: <NotificationsIcon fontSize="small" />,
                onSelect: () => go("/notifications")
            },
            {
                id: "nav-settings",
                label: "Go to Settings",
                group: "Navigate",
                icon: <SettingsIcon fontSize="small" />,
                onSelect: () => go("/settings")
            },
            {
                id: "nav-profile",
                label: "Go to Profile",
                group: "Navigate",
                icon: <PersonIcon fontSize="small" />,
                onSelect: () => go("/profile")
            }
        ],
        // eslint-disable-next-line react-hooks/exhaustive-deps
        []
    );

    const filteredNavigationItems = useMemo(() => {
        if (!query.trim()) {
            return navigationItems;
        }

        const normalized = query.trim().toLowerCase();
        return navigationItems.filter((item) =>
            item.label.toLowerCase().includes(normalized)
        );
    }, [navigationItems, query]);

    const searchItems: PaletteItem[] = useMemo(() => {
        if (!searchResults || query.trim().length < 2) {
            return [];
        }

        const items: PaletteItem[] = [];

        for (const project of searchResults.projects) {
            items.push({
                id: `project-${project.id}`,
                label: project.name,
                description: project.status,
                group: "Projects",
                icon: <FolderCopyIcon fontSize="small" />,
                onSelect: () => go(`/projects/${project.id}`)
            });
        }

        for (const task of searchResults.tasks) {
            items.push({
                id: `task-${task.id}`,
                label: task.title,
                description: task.status,
                group: "Tasks",
                icon: <TaskAltIcon fontSize="small" />,
                onSelect: () => go(`/tasks/${task.id}`)
            });
        }

        for (const user of searchResults.users) {
            items.push({
                id: `user-${user.id}`,
                label: user.fullName,
                description: user.email,
                group: "People",
                icon: <PersonSearchIcon fontSize="small" />,
                onSelect: () => {}
            });
        }

        for (const comment of searchResults.comments) {
            items.push({
                id: `comment-${comment.id}`,
                label: comment.comment,
                description: "Comment",
                group: "Comments",
                icon: <CommentIcon fontSize="small" />,
                onSelect: () => go(`/tasks/${comment.taskId}`)
            });
        }

        return items;
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [searchResults, query]);

    const allItems = useMemo(
        () => [...filteredNavigationItems, ...searchItems],
        [filteredNavigationItems, searchItems]
    );

    function handleQueryChange(value: string) {
        setQuery(value);
        setSelectedIndex(0);
    }

    function handleKeyDown(event: React.KeyboardEvent) {
        if (event.key === "ArrowDown") {
            event.preventDefault();
            setSelectedIndex((prev) => (prev + 1) % Math.max(allItems.length, 1));
        } else if (event.key === "ArrowUp") {
            event.preventDefault();
            setSelectedIndex(
                (prev) => (prev - 1 + allItems.length) % Math.max(allItems.length, 1)
            );
        } else if (event.key === "Enter") {
            event.preventDefault();
            allItems[selectedIndex]?.onSelect();
        }
    }

    let renderedGroup = "";

    return (
        <Dialog
            open={open}
            onClose={close}
            fullWidth
            maxWidth="sm"
            slotProps={{
                paper: {
                    sx: {
                        position: "fixed",
                        top: 96,
                        m: 0,
                        borderRadius: 3
                    }
                }
            }}
        >
            <TextField
                inputRef={inputRef}
                value={query}
                onChange={(e) => handleQueryChange(e.target.value)}
                onKeyDown={handleKeyDown}
                placeholder="Search or type a command..."
                variant="standard"
                fullWidth
                autoFocus
                slotProps={{
                    input: {
                        disableUnderline: true,
                        startAdornment: (
                            <InputAdornment position="start">
                                <SearchIcon color="action" />
                            </InputAdornment>
                        ),
                        endAdornment: (
                            <InputAdornment position="end">
                                <Chip
                                    size="small"
                                    label={isMac ? "⌘K" : "Ctrl+K"}
                                    variant="outlined"
                                />
                            </InputAdornment>
                        )
                    }
                }}
                sx={{ px: 2.5, py: 2 }}
            />

            <List
                sx={{
                    maxHeight: 420,
                    overflowY: "auto",
                    borderTop: "1px solid",
                    borderColor: "divider",
                    py: 0
                }}
            >
                {isFetching && query.trim().length >= 2 && (
                    <Typography variant="caption" color="text.secondary" sx={{ px: 2.5, py: 1, display: "block" }}>
                        Searching...
                    </Typography>
                )}

                {allItems.length === 0 && (
                    <Typography variant="body2" color="text.secondary" sx={{ px: 2.5, py: 3, textAlign: "center" }}>
                        No matches found.
                    </Typography>
                )}

                {allItems.map((item, index) => {
                    const showHeader = item.group !== renderedGroup;
                    renderedGroup = item.group;

                    return (
                        <Stack key={item.id}>
                            {showHeader && (
                                <Typography
                                    variant="overline"
                                    color="text.secondary"
                                    sx={{ px: 2.5, pt: 1.5, pb: 0.5, display: "block" }}
                                >
                                    {item.group}
                                </Typography>
                            )}

                            <ListItemButton
                                selected={index === selectedIndex}
                                onMouseEnter={() => setSelectedIndex(index)}
                                onClick={() => item.onSelect()}
                                sx={{ px: 2.5, py: 1 }}
                            >
                                <ListItemIcon sx={{ minWidth: 36 }}>
                                    {item.icon}
                                </ListItemIcon>

                                <ListItemText
                                    primary={item.label}
                                    secondary={item.description}
                                    slotProps={{
                                        primary: { sx: { fontWeight: 500 } }
                                    }}
                                />
                            </ListItemButton>
                        </Stack>
                    );
                })}
            </List>
        </Dialog>
    );
}
