import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { RecipeResponse } from 'src/app/core/models/recipe/recipe.model';
import { LocalStorageUtils } from 'src/app/shared/helpers/localstorage/localstorage';
import { RecipeService } from 'src/app/shared/services/recipe/recipe.service';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.css']
})
export class RecipeComponent implements OnInit {

  recipes$ = new Observable<RecipeResponse[]>();
  recipe = {} as RecipeResponse;
  userId: string = '';
  
  constructor(private recipeService: RecipeService, private localStorage: LocalStorageUtils, private router: Router) { }

  ngOnInit(): void {
    this.userId = this.localStorage.getUserId();
    this.getRecipes();
  }
  getRecipes(){
    var userId = this.localStorage.getUserId();
    this.recipes$ = this.recipeService.getRecipes(userId);
  }
  
  onDelete(id: string = ''){
    if(id != ''){
      this.getRecipesById(id);
    }

  }
  getRecipesById(id:string){
    this.recipeService.getRecipeById(id, this.userId).subscribe(data => {
      this.recipe = data;
    });
  } 
  deleteRecipe(recipeId: string){
    this.recipeService.deleteRecipe(recipeId).subscribe(_ => {
      this.recipeService.getRecipes(this.userId);
      this.clearRecipeFields();
      this.router.navigate([`admin/receitas`]);
    });
  }

  clearRecipeFields(){
    this.recipe = {} as RecipeResponse;
  }
}
