import { EDifficulty } from "../difficulty/difficulty.enum";
import { Ingredient } from "../ingredient/ingredient.model";


export interface RecipeAdd {
    title: string;
    preparationSteps: string[];
    blogId: string;
    categoryId: string;
    difficulty: number;
    preparationTime: string;
    servings: number;
    ingredients: Ingredient;
}