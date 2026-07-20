import {
    BrowserRouter,
    Navigate,
    Route,
    Routes
} from "react-router-dom";

import ProtectedRoute from "@/components/auth/ProtectedRoute";
import MainLayout from "@/layouts/MainLayout";
import DashboardPage from "@/features/dashboard/pages/DashboardPage";
import LoginPage from "@/features/auth/pages/LoginPage";
import ProjectsPage from "@/features/projects/pages/ProjectsPage";

import TasksPage from "@/features/tasks/pages/TasksPage";
import ReportsPage from "@/features/reports/pages/ReportsPage";
import NotificationsPage from "@/features/notifications/pages/NotificationsPage";
import AIPage from "@/features/ai/pages/AIPage";
import SettingsPage from "@/features/settings/pages/SettingsPage";
import ProfilePage from "@/features/profile/pages/ProfilePage";

export default function AppRoutes() {
    return (
        <BrowserRouter>
            <Routes>
                {/* Public */}
                <Route
                    path="/login"
                    element={<LoginPage />}
                />

                {/* Protected */}
                <Route element={<ProtectedRoute />}>
                    <Route element={<MainLayout />}>
                        <Route
                            index
                            element={
                                <Navigate
                                    to="/dashboard"
                                    replace
                                />
                            }
                        />

                        <Route
                            path="/dashboard"
                            element={<DashboardPage />}
                        />

                        <Route
                            path="/projects"
                            element={<ProjectsPage />}
                        />

                        <Route
                            path="/tasks"
                            element={<TasksPage />}
                        />

                        <Route
                            path="/notifications"
                            element={<NotificationsPage />}
                        />

                        <Route
                            path="/reports"
                            element={<ReportsPage />}
                        />

                        <Route
                            path="/ai"
                            element={<AIPage />}
                        />

                        <Route
                            path="/settings"
                            element={<SettingsPage />}
                        />

                        <Route
                            path="/profile"
                            element={<ProfilePage />}
                        />
                    </Route>
                </Route>

                {/* Fallback */}
                <Route
                    path="*"
                    element={
                        <Navigate
                            to="/dashboard"
                            replace
                        />
                    }
                />
            </Routes>
        </BrowserRouter>
    );
}
