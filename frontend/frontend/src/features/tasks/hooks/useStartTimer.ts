import { useMutation, useQueryClient } from "@tanstack/react-query";

import { startTimer } from "../api/timeApi";

export function useStartTimer() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: startTimer,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["running-timer"] });
        }
    });
}
