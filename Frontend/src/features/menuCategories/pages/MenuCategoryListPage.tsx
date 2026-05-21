import { useEffect, useState } from "react";
import { type MenuCategory } from "../types/menuCategoryTypes";
import { menuCategoryService } from "../services/menuCategoryService";

export default function MenuCategoryListPage() {
  const [menuCategories, setMenuCategories] = useState<MenuCategory[]>([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    loadMenuCategories();
  }, []);

  const loadMenuCategories = async () => {
    try {
      setLoading(true);
      const data = await menuCategoryService.getAll();
      setMenuCategories(data);
    } catch (error) {
      console.error("Failed to load menu categories", error);
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <h1>Menu Categories</h1>

      {menuCategories.map((category) => (
        <div key={category.menuCategoryId}>
          {category.categoryName}
        </div>
      ))}
    </div>
  );
}