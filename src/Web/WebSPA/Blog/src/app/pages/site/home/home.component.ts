import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Recipe } from 'src/app/models/blog/recipe/recipe.model';
import { RecipeService } from 'src/app/services/blog/recipe/recipe.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  recipes$ = new Observable<Recipe[]>();
  // recipes: Recipe[] =[]
  recipe = {} as Recipe;
  constructor(private recipeService: RecipeService, private router: Router) { }

  ngOnInit(): void {
    this.getRecipes();
  }

  getRecipes(){
    this.recipes$ = this.recipeService.getPublicRecipes();
  }
  getRecipe(recipe: Recipe){
    this.router.navigate([`receita/${recipe.id}`]);
  }
  // getRecipesById(id:string){
  //   this.recipe$ = this.recipeService.getPublicRecipesById(id);
    
  //     console.log(this.recipe$);
  //   // this.router.navigate([`receita/${this.recipe.title}`]);
  // }
}
