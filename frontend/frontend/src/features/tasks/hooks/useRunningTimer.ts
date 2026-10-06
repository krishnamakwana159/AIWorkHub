import { useQuery } from "@tanstack/react-query";

import { getRunningTimer } from "../api/timeApi";

export function useRunningTimer() {
    return useQuery({
        queryKey: ["running-timer"],
        queryFn: getRunningTimer,
        refetchInterval: 30000
    });
}
