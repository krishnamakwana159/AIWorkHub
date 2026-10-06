import { useMutation } from "@tanstack/react-query";

import { suggestTaskPriority } from "../api/aiApi";

export function useSuggestTaskPriority() {
    return useMutation({
        mutationFn: suggestTaskPriority
    });
}
