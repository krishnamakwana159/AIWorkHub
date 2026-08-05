import { QueryClientProvider } from "@tanstack/react-query";
import { SnackbarProvider } from "notistack";
import { AuthProvider } from "@/contexts/AuthContext";
import type { PropsWithChildren } from "react";
import { queryClient } from "@/shared/api/queryClient";

export default function AppProviders({
    children
}: PropsWithChildren) {
    return (
        <QueryClientProvider client={queryClient}>
            <SnackbarProvider>

                <AuthProvider>

                    {children}

                </AuthProvider>

            </SnackbarProvider>
        </QueryClientProvider>
    );
}
