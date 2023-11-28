import { Component, OnInit } from '@angular/core'
import { ActivatedRoute, Router } from '@angular/router';
import { EDifficulty } from 'src/app/core/models/difficulty/difficulty.model';
import { RecipeRequest, RecipeResponse } from 'src/app/core/models/recipe/recipe.model';
import { RecipeUtils } from 'src/app/shared/helpers/recipe/recipe-utils';
import { CategoryService } from 'src/app/shared/services/category/category.service';
import { RecipeService } from 'src/app/shared/services/recipe/recipe.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-add-upd-recipe',
  templateUrl: './add-upd-recipe.component.html',
  styleUrls: ['./add-upd-recipe.component.css']
})
export class AddUpdRecipeComponent implements OnInit {

  tinyApi: string = environment.tinyMceApi;
  recipeId: string = '';
  recipeData = {} as RecipeResponse;
  createdAt: any;
  difficultyMapped: string = '';

  pageTitle: string = '';

  recipe = {} as RecipeRequest;
  prepStepInput: string = '';
  categories: string[] = ['Sobremesa', 'Almoço', 'Janta'];
  difficulties: { id: number; nome: string }[] = [
    { id: Number(EDifficulty.Fácil), nome: 'Fácil' },
    { id: Number(EDifficulty.Médio), nome: 'Médio' },
    { id: Number(EDifficulty.Difícil), nome: 'Difícil' }
  ];
  newGroup: string = '';
  newIngredient: string = '';
  newIngredients: string[] = [];
  groups: string[] = [];

  constructor(private recipeUtils: RecipeUtils, private recipeService: RecipeService, private categoryService: CategoryService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.recipeId = this.recipeUtils.getIdFromCurrentUrl(this.route.snapshot.url);
    this.getRecipesById(this.recipeId);
  }

  getRecipesById(id:string){
    this.recipeService.getAuthRecipesById(id).subscribe(data => {
      this.recipe.title = data.title;
      this.recipe.categoryId = data.category.id;
      this.recipe.difficulty = data.difficulty.id;
      this.recipe.preparationTime = data.preparationTime;
      this.recipe.servings = data.servings;
      
      this.getPageTitle();
      console.log(data)
      // this.difficultyMapped = this.recipeUtils.mapDifficulty(data.difficulty.id);
    });
  } 
  
  getPageTitle(){
    if (this.recipeId != ''){
      this.pageTitle = "Editar Receita"
    } else {
      this.pageTitle = "Adicionar Receita " + this.recipe.title
    }
  }
  saveRecipe() {
    const recipe: RecipeRequest = {
      title: this.recipe.title,
      preparationSteps: this.recipe.preparationSteps,
      // preparationSteps: this.spitPreparationStep(this.prepStepInput),
      blogId: "2a2ff613-6f3b-4dd8-9fd6-a2f824b67b62",
      categoryId: "50a325fd-19e0-4feb-bead-a7c60c0581aa",
      difficulty: Number(this.recipe.difficulty),
      preparationTime: this.recipe.preparationTime,
      servings: this.recipe.servings,
      ingredients: this.recipe.ingredients
      // ingredients: this.recipeIngredients
    };
    var recipeJson = JSON.stringify(recipe);
    this.recipeService.postAuthRecipe(recipe).subscribe(_ => this.recipeService.getPublicRecipes());
    console.log(recipeJson);
  }
}
