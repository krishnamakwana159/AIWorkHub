import {
    createContext,
    useContext,
    useMemo,
    useState
} from "react";

import type { PropsWithChildren } from "react";

import { storage } from "../shared/utils/storage";

type AuthContextType = {

    isAuthenticated: boolean;

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

    const value = useMemo(
        () => ({

            isAuthenticated: authenticated,

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
            }

        }),
        [authenticated]
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
