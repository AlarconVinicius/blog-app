import { AuthorResponse } from "../author/author.model";
import { CategoryResponse } from "../category/category.model";
import { Difficulty } from "../difficulty/difficulty.model";

export interface RecipeRequest {
  title: string;
  preparationSteps: string;
  blogId: string;
  categoryId: string;
  difficulty: number;
  preparationTime: string;
  servings: number;
  ingredients: string;
}

export interface RecipeResponse {
  id: string;
  title: string;
  preparationSteps: string;
  author: AuthorResponse;
  category: CategoryResponse;
  difficulty: Difficulty;
  preparationTime: string;
  servings: number;
  ingredients: string;
  url: string;
  createdAt: string;
  updatedAt: string;
}