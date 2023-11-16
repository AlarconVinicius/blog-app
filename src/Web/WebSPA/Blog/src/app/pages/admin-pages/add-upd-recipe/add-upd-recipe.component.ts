import { Component, OnInit } from '@angular/core';
import { Difficulty } from 'src/app/models/blog/difficulty/difficulty';
import { Ingredients } from 'src/app/models/blog/ingredient/ingredients';
import { RecipeAdd } from 'src/app/models/blog/recipe/recipe-add';

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
    content: '',
    blogId: "2a2ff613-6f3b-4dd8-9fd6-a2f824b67b62",
    categoryId: "6d66cbf6-8356-4a3b-bf2e-79b6cdd151ad",
    difficulty: Difficulty.Easy,
    preparationTime: '',
    servings: 1,
    ingredients: { ingredients: {} }
  };
  categories: string[] = ['Sobremesa', 'Almoço', 'Janta'];
  difficulties: { id: number; nome: string }[] = [
    { id: Number(Difficulty.Easy), nome: 'Fácil' },
    { id: Number(Difficulty.Medium), nome: 'Médio' },
    { id: Number(Difficulty.Hard), nome: 'Difícil' }
  ];
  recipeIngredients: Ingredients = { ingredients: {} };
  newGroup: string = '';
  newIngredient: string = '';
  newIngredients: string[] = [];
  groups: string[] = [];

  constructor() { }

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
      content: this.recipe.content,
      blogId: "2a2ff613-6f3b-4dd8-9fd6-a2f824b67b62",
      categoryId: "6d66cbf6-8356-4a3b-bf2e-79b6cdd151ad",
      difficulty: this.recipe.difficulty,
      preparationTime: this.recipe.preparationTime,
      servings: this.recipe.servings,
      ingredients: this.recipeIngredients
    };
    var recipeJson = JSON.stringify(recipe);
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
    const formattedIngredients: Ingredients = { ingredients: {} };
    for (const group of this.groups) {
      const groupIngredients = this.recipeIngredients.ingredients[group].map(ingredient => ingredient.trim());
      formattedIngredients.ingredients[group] = groupIngredients;
    }
    return JSON.stringify(formattedIngredients);
  }
  //#endregion
}
