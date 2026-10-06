import { useQuery } from "@tanstack/react-query";

import { getMyProfile } from "../api/profileApi";

export function useMyProfile() {
    return useQuery({
        queryKey: ["my-profile"],
        queryFn: getMyProfile
    });
}
