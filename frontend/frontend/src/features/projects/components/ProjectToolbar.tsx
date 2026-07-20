import AddIcon from "@mui/icons-material/Add";
import SearchIcon from "@mui/icons-material/Search";

import {
    Button,
    InputAdornment,
    Stack,
    TextField
} from "@mui/material";

type Props = {
    search: string;
    onSearch(value: string): void;
    onCreate(): void;
};

export default function ProjectToolbar({
    search,
    onSearch,
    onCreate
}: Props) {
    return (
        <Stack
            direction={{
                xs: "column",
                md: "row"
            }}
            sx={{spacing:2,
              justifyContent:"space-between"
            }}
        >
            <TextField
                size="small"
                placeholder="Search projects..."
                value={search}
                onChange={(e) =>
                    onSearch(e.target.value)
                }
                sx={{ width: 350 }}
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

            <Button
                startIcon={<AddIcon />}
                variant="contained"
                onClick={onCreate}
            >
                New Project
            </Button>
        </Stack>
    );
}
