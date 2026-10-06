import { useMutation } from "@tanstack/react-query";

import { generateTaskBreakdown } from "../api/aiApi";

export function useGenerateTaskBreakdown() {
    return useMutation({
        mutationFn: generateTaskBreakdown
    });
}
