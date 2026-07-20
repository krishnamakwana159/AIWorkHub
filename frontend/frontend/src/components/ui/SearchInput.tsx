import SearchIcon from "@mui/icons-material/Search";

import {

    InputAdornment,

    TextField

} from "@mui/material";

type Props = {

    value: string;

    onChange(value: string): void;

    placeholder?: string;

};

export default function SearchInput({

    value,

    onChange,

    placeholder = "Search..."

}: Props) {

    return (

        <TextField

            fullWidth

            size="small"

            value={value}

            placeholder={placeholder}

            onChange={(e) => onChange(e.target.value)}

            slotProps={{
                input: {
                    startAdornment: (
                        <InputAdornment position="start">
                            <SearchIcon />
                        </InputAdornment>
                    )
                }
            }}

        />

    );

}
