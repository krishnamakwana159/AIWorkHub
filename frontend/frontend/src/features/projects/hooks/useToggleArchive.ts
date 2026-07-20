import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toggleArchive } from "../api/projectsApi";

export function useToggleArchive() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: toggleArchive,
        onSuccess: () =>
            queryClient.invalidateQueries({
                queryKey: ["projects"]
            })
    });
}
