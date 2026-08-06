import { useQuery } from "@tanstack/react-query";

import { getUserLookup } from "../api/usersApi";

export function useUserLookup() {
    return useQuery({
        queryKey: ["user-lookup"],
        queryFn: getUserLookup
    });
}
