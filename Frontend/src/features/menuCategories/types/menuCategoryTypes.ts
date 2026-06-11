export interface MenuCategory {
  categoryId: number;
  restaurantId: number;
  categoryName: string;
  description?: string;
  displayOrder: number;
  isActive: boolean;
}

export interface CreateMenuCategoryRequest {
  restaurantId: number;
  categoryName: string;
  description?: string;
  displayOrder: number;
  isActive: boolean;
}

export interface UpdateMenuCategoryRequest {
  categoryId: number;
  categoryName: string;
  description?: string;
  displayOrder: number;
  isActive: boolean;
}

export interface MenuCategoryFormValues {
  categoryId?: number;
  categoryName: string;
  description: string;
  displayOrder: number;
  isActive: boolean;
}