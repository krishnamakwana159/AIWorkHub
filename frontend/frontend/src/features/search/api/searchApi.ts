import apiClient from "@/shared/api/apiClient";

import type { SearchResponse } from "../types/search";

export async function globalSearch(query: string) {
    const { data } = await apiClient.get<SearchResponse>("/search", {
        params: { q: query }
    });

    return data;
}
