import {
    useMutation,
    useQueryClient
} from "@tanstack/react-query";

import { toggleTaskPin } from "../api/tasksApi";

export function useToggleTaskPin() {

    const queryClient =
        useQueryClient();

    return useMutation({

        mutationFn: toggleTaskPin,

        onSuccess: () => {

            queryClient.invalidateQueries({
                queryKey: ["tasks"]
            });

        }

    });

}
