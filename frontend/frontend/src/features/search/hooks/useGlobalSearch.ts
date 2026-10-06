import { useEffect, useState } from "react";
import { useQuery } from "@tanstack/react-query";

import { globalSearch } from "../api/searchApi";

export function useGlobalSearch(query: string) {
    const [debouncedQuery, setDebouncedQuery] = useState(query);

    useEffect(() => {
        const timeout = setTimeout(() => setDebouncedQuery(query), 300);

        return () => clearTimeout(timeout);
    }, [query]);

    return useQuery({
        queryKey: ["global-search", debouncedQuery],
        queryFn: () => globalSearch(debouncedQuery),
        enabled: debouncedQuery.trim().length >= 2
    });
}
