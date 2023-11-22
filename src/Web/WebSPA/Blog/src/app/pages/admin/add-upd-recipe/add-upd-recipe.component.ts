import { Component, OnInit } from '@angular/core'
import { EDifficulty } from 'src/app/models/blog/difficulty/difficulty.enum';
import { Ingredient } from 'src/app/models/blog/ingredient/ingredient.model';
import { RecipeAdd } from 'src/app/models/blog/recipe/recipe-add';
import { RecipeService } from 'src/app/services/blog/recipe/recipe.service';

@Component({
  selector: 'app-add-upd-recipe',
  templateUrl: './add-upd-recipe.component.html',
  styleUrls: ['./add-upd-recipe.component.css']
})
export class AddUpdRecipeComponent implements OnInit {

  title: string = '';
  recipeId: string = '';
  recipe: RecipeAdd = {
    title: '',
    preparationSteps: [],
    blogId: "2a2ff613-6f3b-4dd8-9fd6-a2f824b67b62",
    categoryId: "6d66cbf6-8356-4a3b-bf2e-79b6cdd151ad",
    difficulty: Number(EDifficulty.Fácil),
    preparationTime: '',
    servings: 1,
    ingredients: { ingredients: {} }
  };
  prepStepInput: string = '';
  categories: string[] = ['Sobremesa', 'Almoço', 'Janta'];
  difficulties: { id: number; nome: string }[] = [
    { id: Number(EDifficulty.Fácil), nome: 'Fácil' },
    { id: Number(EDifficulty.Médio), nome: 'Médio' },
    { id: Number(EDifficulty.Difícil), nome: 'Difícil' }
  ];
  recipeIngredients: Ingredient = { ingredients: {} };
  newGroup: string = '';
  newIngredient: string = '';
  newIngredients: string[] = [];
  groups: string[] = [];

  constructor(private recipeService: RecipeService) { }

  ngOnInit(): void {
    this.getPageTitle();
  }
  
  getPageTitle(){
    if (this.recipeId != ''){
      this.title = "Editar"
    } else {
      this.title = "Adicionar"
    }
  }
  saveRecipe() {
    const recipe: RecipeAdd = {
      title: this.recipe.title,
      preparationSteps: this.spitPreparationStep(this.prepStepInput),
      blogId: "2a2ff613-6f3b-4dd8-9fd6-a2f824b67b62",
      categoryId: "50a325fd-19e0-4feb-bead-a7c60c0581aa",
      difficulty: Number(this.recipe.difficulty),
      preparationTime: this.recipe.preparationTime,
      servings: this.recipe.servings,
      ingredients: this.recipeIngredients
    };
    var recipeJson = JSON.stringify(recipe);
    this.recipeService.postRecipe(recipe).subscribe(_ => this.recipeService.getPublicRecipes());
    console.log(recipeJson);
  }

  //#region Ingredients Field Methods
  addIngredientGroup() {
    if (this.newGroup.trim() !== '') {
      this.groups.push(this.newGroup);
      this.newGroup = '';
    }
  }
  addIngredient(group: string, newIngredient: string, index: number) {
    if (newIngredient.trim() !== '') {
      if (!this.recipeIngredients.ingredients[group]) {
        this.recipeIngredients.ingredients[group] = [];
      }
      this.recipeIngredients.ingredients[group].push(newIngredient);
      this.newIngredients[index] = '';
    }
  }
  
  addGroup() {
    const group = this.newGroup.trim();
    if (group.length > 0) {
      this.recipeIngredients.ingredients[group] = [];
    }
  }
  formatIngredients(): string {
    const formattedIngredients: Ingredient = { ingredients: {} };
    for (const group of this.groups) {
      const groupIngredients = this.recipeIngredients.ingredients[group].map(ingredient => ingredient.trim());
      formattedIngredients.ingredients[group] = groupIngredients;
    }
    return JSON.stringify(formattedIngredients);
  }
  //#endregion
  //#region Recipe Field Methods
  spitPreparationStep(prepSteps: string): string[] {
    return prepSteps.split("\n");
  }
  //#endregion
}
