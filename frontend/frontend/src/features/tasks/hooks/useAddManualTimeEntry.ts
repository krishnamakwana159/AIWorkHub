import { useMutation, useQueryClient } from "@tanstack/react-query";

import { addManualTimeEntry } from "../api/timeApi";

export function useAddManualTimeEntry() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: addManualTimeEntry,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["task-time-entries"] });
            queryClient.invalidateQueries({ queryKey: ["task"] });
        }
    });
}
