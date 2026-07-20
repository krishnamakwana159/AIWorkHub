import type { PropsWithChildren } from "react";

import { QueryClient } from "@tanstack/react-query";
import { QueryClientProvider } from "@tanstack/react-query";

import { ThemeProvider } from "@mui/material/styles";

import CssBaseline from "@mui/material/CssBaseline";

import { Toaster } from "sonner";
import { theme } from "../../shared/theme/theme";
import { AuthProvider } from "../../contexts/AuthContext";

const queryClient = new QueryClient();

export default function AppProviders({
    children
}: PropsWithChildren) {

    return (

        <QueryClientProvider client={queryClient}>

            <ThemeProvider theme={theme}>

                <CssBaseline />

                <AuthProvider>

                    {children}

                </AuthProvider>

                <Toaster
                    richColors
                    position="top-right"
                />

            </ThemeProvider>

        </QueryClientProvider>

    );

}
