import {
    createContext,
    useContext,
    useEffect,
    useMemo,
    useState
} from "react";

import type { PropsWithChildren } from "react";

import apiClient from "@/shared/api/apiClient";
import { storage } from "../shared/utils/storage";

type AuthContextType = {

    isAuthenticated: boolean;

    isAdministrator: boolean;

    login(
        accessToken: string,
        refreshToken: string
    ): void;

    logout(): void;

};

const AuthContext =
    createContext<AuthContextType | null>(null);

export function AuthProvider({
    children
}: PropsWithChildren) {

    const [authenticated, setAuthenticated] =
        useState(
            !!storage.getAccessToken()
        );

    const [isAdministrator, setIsAdministrator] = useState(false);

    // The JWT itself isn't practical to decode client-side here (this API
    // writes .NET's long claim-type URIs into the token payload rather than
    // short keys), so role is derived from /users/me instead, which is
    // already needed for the Profile page.
    useEffect(() => {
        if (!authenticated) {
            return;
        }

        let cancelled = false;

        apiClient
            .get<{ roles: string[] }>("/users/me")
            .then(({ data }) => {
                if (!cancelled) {
                    setIsAdministrator(data.roles.includes("Administrator"));
                }
            })
            .catch(() => {
                if (!cancelled) {
                    setIsAdministrator(false);
                }
            });

        return () => {
            cancelled = true;
        };
    }, [authenticated]);

    const value = useMemo(
        () => ({

            isAuthenticated: authenticated,

            isAdministrator,

            login(
                accessToken: string,
                refreshToken: string
            ) {

                storage.setAccessToken(accessToken);

                storage.setRefreshToken(refreshToken);

                setAuthenticated(true);

            },

            logout() {
                storage.clear();
                setAuthenticated(false);
                setIsAdministrator(false);
            }

        }),
        [authenticated, isAdministrator]
    );

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth() {

    const context =
        useContext(AuthContext);

    if (!context) {
        throw new Error(
            "useAuth must be inside AuthProvider"
        );
    }
    return context;
}
