import {
    useMutation,
    useQueryClient
} from "@tanstack/react-query";
import { deleteAttachment } from "../api/tasksApi";

export function useDeleteAttachment() {

    const queryClient =
        useQueryClient();

    return useMutation({
        mutationFn: deleteAttachment,
        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: [
                    "task-attachments"
                ]
            });
        }

    });

}
