import { useQuery } from "@tanstack/react-query";

import { getCalendarTasks } from "../api/tasksApi";

export function useCalendarTasks() {
    return useQuery({
        queryKey: ["calendar-tasks"],
        queryFn: getCalendarTasks
    });
}
