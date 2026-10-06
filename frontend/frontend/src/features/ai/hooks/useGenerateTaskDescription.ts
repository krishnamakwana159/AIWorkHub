import { useMutation } from "@tanstack/react-query";

import { generateTaskDescription } from "../api/aiApi";

export function useGenerateTaskDescription() {
    return useMutation({
        mutationFn: generateTaskDescription
    });
}
