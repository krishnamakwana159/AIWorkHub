import { Navigate, Outlet } from "react-router-dom";

import { useAuth } from "@/contexts/AuthContext";

export default function ProtectedRoute() {
    const { isAuthenticated } = useAuth();
  
    if (!isAuthenticated) {
        return <Navigate to="/login" replace />;
    }

    return <Outlet />;
}
// import { Navigate } from "react-router-dom";
// import type { PropsWithChildren } from "react";

// import { useAuth } from "../../contexts/AuthContext";

// export default function ProtectedRoute({
//     children
// }: PropsWithChildren) {

//     const { isAuthenticated } = useAuth();

//     if (!isAuthenticated) {
//         return <Navigate to="/login" replace />;
//     }

//     return children;
// }
