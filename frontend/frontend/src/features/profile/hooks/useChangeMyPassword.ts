import { useMutation } from "@tanstack/react-query";

import { changeMyPassword } from "../api/profileApi";

export function useChangeMyPassword() {
    return useMutation({
        mutationFn: changeMyPassword
    });
}
