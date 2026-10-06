import { Controller, useFormContext } from "react-hook-form";
import { MenuItem, TextField } from "@mui/material";

export interface SelectOption {
    label: string;
    value: string | number;
}

type Props = {
    name: string;
    label: string;
    options: SelectOption[];
};

export default function FormSelect({
    name,
    label,
    options
}: Props) {

    const {
        control
    } = useFormContext();

    const isNumeric = options.every(
        option => typeof option.value === "number"
    );

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
                    select
                    fullWidth
                    label={label}
                    error={!!fieldState.error}
                    helperText={
                        fieldState.error?.message
                    }
                    onChange={(e) => {
                        field.onChange(
                            isNumeric ? Number(e.target.value) : e.target.value
                        );
                    }}
                >
                    {options.map(option => (
                        <MenuItem
                            key={option.value}
                            value={option.value}
                        >
                            {option.label}
                        </MenuItem>
                    ))}
                </TextField>
            )}
        />
    );

}
