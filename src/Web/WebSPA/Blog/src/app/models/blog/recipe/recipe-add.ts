import { EDifficulty } from "../difficulty/difficulty.enum";
import { Ingredient } from "../ingredient/ingredient.model";


export interface RecipeAdd {
    title: string;
    content: string;
    blogId: string;
    categoryId: string;
    difficulty: EDifficulty;
    preparationTime: string;
    servings: number;
    ingredients: Ingredient;
}