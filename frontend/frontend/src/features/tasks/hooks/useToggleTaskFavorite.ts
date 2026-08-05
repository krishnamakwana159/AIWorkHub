import {
    useMutation,
    useQueryClient
} from "@tanstack/react-query";

import { toggleTaskFavorite } from "../api/tasksApi";

export function useToggleTaskFavorite() {

    const queryClient =
        useQueryClient();

    return useMutation({

        mutationFn: toggleTaskFavorite,

        onSuccess: () => {

            queryClient.invalidateQueries({
                queryKey: ["tasks"]
            });

        }

    });

}
