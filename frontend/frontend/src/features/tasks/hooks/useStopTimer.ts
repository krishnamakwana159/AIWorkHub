import { useMutation, useQueryClient } from "@tanstack/react-query";

import { stopTimer } from "../api/timeApi";

export function useStopTimer() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: stopTimer,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["running-timer"] });
            queryClient.invalidateQueries({ queryKey: ["task-time-entries"] });
            queryClient.invalidateQueries({ queryKey: ["task"] });
        }
    });
}
