import { useMutation, useQueryClient } from "@tanstack/react-query";
import { uploadAttachment } from "../api/tasksApi";

export function useUploadAttachment() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ taskId, file }: { taskId: string; file: File }) =>
      uploadAttachment(taskId, file),

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["task-attachments"],
      });
    },
  });
}
