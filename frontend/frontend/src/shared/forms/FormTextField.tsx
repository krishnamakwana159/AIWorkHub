import TextField from "@mui/material/TextField";

import {
    Controller,
    useFormContext
} from "react-hook-form";

type Props = {
    name: string;
    label: string;
    multiline?: boolean;
    rows?: number;
    type?: string;
};

export default function FormTextField({
    name,
    label,
    multiline,
    rows,
    type = "text"
}: Props) {

    const {control} = useFormContext();

    return (

        <Controller
            name={name}
            control={control}
            render={({
                field,
                fieldState
            }) => (
                <TextField
                    {...field}
                    fullWidth
                    type={type}
                    label={label}
                    multiline={multiline}
                    rows={rows}
                    error={!!fieldState.error}
                    helperText={
                        fieldState.error?.message
                    }
                />
            )}
        />
    );
}
