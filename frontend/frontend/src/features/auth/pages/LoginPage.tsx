import { useState } from "react";

import {
    Alert,
    Box,
    Paper,
    Stack,
    TextField,
    Typography
} from "@mui/material";

import AppButton from "../../../components/ui/AppButton";

import authService from "../services/authService";

import { useAuth } from "../../../contexts/AuthContext";

export default function LoginPage() {

    const auth = useAuth();

    const [email, setEmail] =
        useState("");

    const [password, setPassword] =
        useState("");

    const [loading, setLoading] =
        useState(false);

    const [error, setError] =
        useState("");

    async function login() {

        try {

            setLoading(true);

            setError("");

            const result =
                await authService.login({

                    email,

                    password

                });

            auth.login(

                result.accessToken,

                result.refreshToken

            );
          window.location.href= "/";
        }

        catch {

            setError(
                "Invalid credentials."
            );

        }

        finally {

            setLoading(false);

        }

    }

    return (

        <Box
            sx={{
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                minHeight: "100vh"
            }}
        >

            <Paper
                sx={{
                    width: 420,
                    p: 4
                }}
            >

                <Stack spacing={2}>

                    <Typography variant="h4">

                        AIWorkHub

                    </Typography>

                    {error &&
                        <Alert severity="error">

                            {error}

                        </Alert>
                    }

                    <TextField
                        label="Email"
                        value={email}
                        onChange={e =>
                            setEmail(e.target.value)
                        }
                    />

                    <TextField
                        label="Password"
                        type="password"
                        value={password}
                        onChange={e =>
                            setPassword(e.target.value)
                        }
                    />

                    <AppButton

                        onClick={login}

                        loading={loading}

                    >

                        Login

                    </AppButton>

                </Stack>

            </Paper>

        </Box>

    );

}
