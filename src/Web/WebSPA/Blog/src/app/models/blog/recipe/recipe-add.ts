import { Difficulty } from "../difficulty/difficulty";
import { Ingredients } from "../ingredient/ingredients";

export interface RecipeAdd {
    title: string;
    content: string;
    blogId: string;
    categoryId: string;
    difficulty: Difficulty;
    preparationTime: string;
    servings: number;
    ingredients: Ingredients;
}