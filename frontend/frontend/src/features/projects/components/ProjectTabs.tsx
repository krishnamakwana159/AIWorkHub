import { Tabs, Tab } from "@mui/material";
import { useLocation, useNavigate } from "react-router-dom";

type Props = {
  projectId: string;
};

export default function ProjectTabs({
  projectId,
}: Props) {
  const navigate = useNavigate();
  const location = useLocation();
  const basePath = `/projects/${projectId}`;

  const tabs = [
    { label: "Overview", path: basePath },
    { label: "Tasks", path: `${basePath}/tasks` },
    { label: "Members", path: `${basePath}/members` },
    { label: "Activity", path: `${basePath}/activity` },
  ];

  const value = tabs.findIndex((tab) => tab.path === location.pathname);

  return (
    <Tabs
      value={value === -1 ? 0 : value}
      onChange={(_, nextValue) => navigate(tabs[nextValue].path)}
      sx={{ mb: 3 }}
    >
      {tabs.map((tab) => (
        <Tab key={tab.path} label={tab.label} />
      ))}
    </Tabs>
  );
}
