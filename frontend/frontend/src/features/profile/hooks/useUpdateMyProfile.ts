import { useMutation, useQueryClient } from "@tanstack/react-query";

import { updateMyProfile } from "../api/profileApi";

export function useUpdateMyProfile() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: updateMyProfile,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["my-profile"] });
        }
    });
}
