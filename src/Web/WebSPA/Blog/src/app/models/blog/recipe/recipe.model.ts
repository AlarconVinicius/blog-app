import { Author } from "../author/author.model";
import { Category } from "../category/category.model";
import { Difficulty } from "../difficulty/difficulty.model";
import { Ingredient } from "../ingredient/ingredient.model";


export interface RecipePost {
  id: string;
  title: string;
  content: string;
  author: Author;
  category: Category;
  difficulty: Difficulty;
  preparationTime: string;
  servings: number;
  ingredients: Ingredient;
}
