import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { RecipeResponse } from 'src/app/core/models/recipe/recipe.model';
import { RecipeService } from 'src/app/shared/services/recipe/recipe.service';

@Component({
  selector: 'app-site-recipes',
  templateUrl: './site-recipes.component.html',
  styleUrls: ['./site-recipes.component.css']
})
export class SiteRecipesComponent implements OnInit {
  title: string = '';
  search: string = '';
  recipes$ = new Observable<RecipeResponse[]>();
  showNoRecipesMessage: boolean = false;

  constructor(private recipeService: RecipeService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.getInitRecipes();
  }
  getInitRecipes(){
    this.route.params.subscribe(params => {
      if (params['busca']) {
        this.search = params['busca'];
        this.title = `Resultado da busca por: ${this.search}`;
        this.getRecipeBySearch(this.search);
        this.search = '';
      } else if (params['categoria']) {
        this.search = params['categoria'];
        this.title = `Receitas relacionadas a categoria: ${this.search}`;
        this.getRecipeByCategory(this.search);
        this.search = '';
      } else {
        this.title = 'Últimas Receitas';
        this.getRecipes();
      }
    });
  }
  getRecipes(){
    this.recipeService.getPublicRecipes().subscribe(recipes => {
      this.recipes$ = of(recipes);
      this.hasRecipes(recipes);
    });
  }
  getRecipeBySearch(search: string){
    this.recipeService.getPublicRecipesBySearch(search).subscribe(recipes => {
      this.recipes$ = of(recipes);
      this.hasRecipes(recipes);
    });
  }
  getRecipeByCategory(category: string){
    this.recipeService.getPublicRecipesByCategory(category).subscribe(recipes => {
      this.recipes$ = of(recipes);
      this.hasRecipes(recipes);
    });
  }
  hasRecipes(recipes: RecipeResponse[]){
    this.showNoRecipesMessage = !recipes || recipes.length === 0;
  }
  getRecipe(recipe: RecipeResponse){
    this.router.navigate([`receita/${recipe.id}`]);
  }
  onSearch(){
    this.router.navigate([`receitas/busca/${this.search}`]);
  }
}