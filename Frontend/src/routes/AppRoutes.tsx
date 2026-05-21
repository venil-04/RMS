import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import { appRoutes } from "../constants/appRoutes";
import MenuCategoryListPage from "../features/menuCategories/pages/MenuCategoryListPage";

export default function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to={appRoutes.dashboard} />} />

        <Route path={appRoutes.dashboard} element={<h1>Dashboard</h1>} />

        <Route
          path={appRoutes.menuCategories}
          element={<MenuCategoryListPage />}
        />

        <Route path="*" element={<h1>Page Not Found</h1>} />
      </Routes>
    </BrowserRouter>
  );
}