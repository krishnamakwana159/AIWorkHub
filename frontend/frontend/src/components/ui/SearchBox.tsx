import TextField from "@mui/material/TextField";

type Props = {

    value: string;

    onChange(value: string): void;

};

export default function SearchBox({
    value,
    onChange
}: Props) {

    return (

        <TextField
            fullWidth
            placeholder="Search..."
            value={value}
            onChange={e => onChange(e.target.value)}
        />

    );

}
