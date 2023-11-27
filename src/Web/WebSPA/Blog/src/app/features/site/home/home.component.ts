import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { RecipeResponse } from 'src/app/core/models/recipe/recipe.model';
import { RecipeService } from 'src/app/shared/services/recipe/recipe.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  title: string = '';
  showNoRecipesMessage: boolean = false;
  recipes$ = new Observable<RecipeResponse[]>();
  recipe = {} as RecipeResponse;
  search: string = '';
  constructor(private recipeService: RecipeService, private router: Router) { }

  ngOnInit(): void {
    this.title = 'Últimas Receitas';
    this.getRecipes();
  }

  getRecipes(){
    this.recipeService.getPublicRecipes().subscribe(recipes => {
      this.recipes$ = of(recipes);
      this.hasRecipes(recipes);
    });
  }
  getRecipe(recipe: RecipeResponse){
    this.router.navigate([`receita/${recipe.id}`]);
  }
  onSearch(){
    this.title =  `Resultado da busca por: ${this.search}`;
    this.getRecipeBySearch(this.search);
    this.search = '';
  }
  getRecipeBySearch(search: string){
    this.recipeService.getPublicRecipesBySearch(search).subscribe(recipes => {
      this.recipes$ = of(recipes);
      this.hasRecipes(recipes);
    });
  }
  hasRecipes(recipes: RecipeResponse[]){
    this.showNoRecipesMessage = !recipes || recipes.length === 0;
  }
}
