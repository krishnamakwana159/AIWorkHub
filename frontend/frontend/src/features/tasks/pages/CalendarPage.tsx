import { useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";

import dayjs, { type Dayjs } from "dayjs";

import {
    Box,
    Chip,
    IconButton,
    Paper,
    Stack,
    Tooltip,
    Typography
} from "@mui/material";

import ChevronLeftIcon from "@mui/icons-material/ChevronLeft";
import ChevronRightIcon from "@mui/icons-material/ChevronRight";
import TodayIcon from "@mui/icons-material/Today";

import PageContainer from "@/components/common/PageContainer";
import PageTitle from "@/components/common/PageTitle";
import AppLoader from "@/components/ui/AppLoader";
import EmptyState from "@/components/ui/EmptyState";

import { TaskPriorityInfo } from "@/shared/constants/task";

import { useCalendarTasks } from "../hooks/useCalendarTasks";
import type { CalendarTask } from "../types/calendarTask";

const WEEKDAY_LABELS = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
const MAX_VISIBLE_PER_DAY = 3;

function buildMonthGrid(monthStart: Dayjs) {
    const gridStart = monthStart.startOf("week");
    const daysInMonth = monthStart.daysInMonth();
    const totalCells =
        Math.ceil((monthStart.day() + daysInMonth) / 7) * 7;

    return Array.from({ length: totalCells }, (_, index) =>
        gridStart.add(index, "day")
    );
}

export default function CalendarPage() {
    const navigate = useNavigate();
    const { data: tasks = [], isPending, isError } = useCalendarTasks();

    const [monthCursor, setMonthCursor] = useState(() => dayjs().startOf("month"));

    const tasksByDate = useMemo(() => {
        const map = new Map<string, CalendarTask[]>();

        for (const task of tasks) {
            const key = dayjs(task.dueDateUtc).format("YYYY-MM-DD");
            const existing = map.get(key) ?? [];
            existing.push(task);
            map.set(key, existing);
        }

        return map;
    }, [tasks]);

    const days = useMemo(() => buildMonthGrid(monthCursor), [monthCursor]);
    const today = dayjs().format("YYYY-MM-DD");

    return (
        <PageContainer>
            <Stack
                direction="row"
                sx={{ justifyContent: "space-between", alignItems: "center", flexWrap: "wrap", gap: 2 }}
            >
                <PageTitle
                    title="Calendar"
                    subtitle="Tasks by due date across your projects"
                />

                <Stack direction="row" spacing={1} sx={{ alignItems: "center" }}>
                    <IconButton onClick={() => setMonthCursor((prev) => prev.subtract(1, "month"))}>
                        <ChevronLeftIcon />
                    </IconButton>

                    <Typography variant="h6" sx={{ minWidth: 160, textAlign: "center" }}>
                        {monthCursor.format("MMMM YYYY")}
                    </Typography>

                    <IconButton onClick={() => setMonthCursor((prev) => prev.add(1, "month"))}>
                        <ChevronRightIcon />
                    </IconButton>

                    <Tooltip title="Jump to today">
                        <IconButton onClick={() => setMonthCursor(dayjs().startOf("month"))}>
                            <TodayIcon fontSize="small" />
                        </IconButton>
                    </Tooltip>
                </Stack>
            </Stack>

            {isPending && <AppLoader />}

            {isError && (
                <EmptyState message="Unable to load calendar tasks." />
            )}

            {!isPending && !isError && (
                <Paper variant="outlined" sx={{ mt: 3, overflow: "hidden" }}>
                    <Box
                        sx={{
                            display: "grid",
                            gridTemplateColumns: "repeat(7, 1fr)",
                            borderBottom: "1px solid",
                            borderColor: "divider"
                        }}
                    >
                        {WEEKDAY_LABELS.map((label) => (
                            <Box
                                key={label}
                                sx={{
                                    py: 1,
                                    textAlign: "center",
                                    bgcolor: "action.hover"
                                }}
                            >
                                <Typography variant="caption" sx={{ fontWeight: 700, color: "text.secondary" }}>
                                    {label}
                                </Typography>
                            </Box>
                        ))}
                    </Box>

                    <Box
                        sx={{
                            display: "grid",
                            gridTemplateColumns: "repeat(7, 1fr)"
                        }}
                    >
                        {days.map((day) => {
                            const key = day.format("YYYY-MM-DD");
                            const isCurrentMonth = day.month() === monthCursor.month();
                            const isToday = key === today;
                            const dayTasks = tasksByDate.get(key) ?? [];
                            const overflow = dayTasks.length - MAX_VISIBLE_PER_DAY;

                            return (
                                <Box
                                    key={key}
                                    sx={{
                                        minHeight: 110,
                                        p: 1,
                                        borderRight: "1px solid",
                                        borderBottom: "1px solid",
                                        borderColor: "divider",
                                        bgcolor: isCurrentMonth ? "background.paper" : "action.hover",
                                        opacity: isCurrentMonth ? 1 : 0.5
                                    }}
                                >
                                    <Box
                                        sx={{
                                            display: "inline-flex",
                                            alignItems: "center",
                                            justifyContent: "center",
                                            width: 24,
                                            height: 24,
                                            borderRadius: "50%",
                                            mb: 0.5,
                                            bgcolor: isToday ? "primary.main" : "transparent",
                                            color: isToday ? "primary.contrastText" : "text.primary"
                                        }}
                                    >
                                        <Typography variant="caption" sx={{ fontWeight: isToday ? 700 : 500 }}>
                                            {day.date()}
                                        </Typography>
                                    </Box>

                                    <Stack spacing={0.5}>
                                        {dayTasks.slice(0, MAX_VISIBLE_PER_DAY).map((task) => {
                                            const isOverdue =
                                                dayjs(task.dueDateUtc).isBefore(dayjs(), "day") &&
                                                key !== today;

                                            return (
                                                <Tooltip
                                                    key={task.id}
                                                    title={`${task.title} · ${task.projectName}${
                                                        task.assigneeName ? ` · ${task.assigneeName}` : ""
                                                    }`}
                                                >
                                                    <Chip
                                                        size="small"
                                                        label={task.title}
                                                        onClick={() => navigate(`/tasks/${task.id}`)}
                                                        color={
                                                            isOverdue
                                                                ? "error"
                                                                : TaskPriorityInfo[task.priority].color
                                                        }
                                                        variant={isOverdue ? "filled" : "outlined"}
                                                        sx={{
                                                            justifyContent: "flex-start",
                                                            width: "100%",
                                                            "& .MuiChip-label": {
                                                                overflow: "hidden",
                                                                textOverflow: "ellipsis"
                                                            }
                                                        }}
                                                    />
                                                </Tooltip>
                                            );
                                        })}

                                        {overflow > 0 && (
                                            <Typography variant="caption" color="text.secondary">
                                                +{overflow} more
                                            </Typography>
                                        )}
                                    </Stack>
                                </Box>
                            );
                        })}
                    </Box>
                </Paper>
            )}
        </PageContainer>
    );
}
