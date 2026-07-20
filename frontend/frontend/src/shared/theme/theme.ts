import { createTheme } from "@mui/material/styles";

export const theme = createTheme({

    palette: {

        mode: "light",

        primary: {

            main: "#2563eb"

        },

        secondary: {

            main: "#7c3aed"

        },

        background: {

            default: "#f5f7fb",
            paper: "#ffffff"
        }

    },

    shape: {

        borderRadius: 12

    },

    typography: {

        fontFamily: [
            "Inter",
            "Roboto",
            "Arial",
            "sans-serif"
        ].join(",")

    }

});
