import { Tabs, Tab } from "@mui/material";

type Props = {
  value: number;

  onChange(value: number): void;
};

export default function ProjectTabs({
  value,

  onChange,
}: Props) {
  return (
    <Tabs value={value} onChange={(_, value) => onChange(value)} sx={{ mb: 3 }}>
      <Tab label="Overview" />

      <Tab label="Tasks" />

      <Tab label="Members" />

      <Tab label="Activity" />
    </Tabs>
  );
}
