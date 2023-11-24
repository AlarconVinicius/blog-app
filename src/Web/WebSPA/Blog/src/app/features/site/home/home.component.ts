import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { RecipeResponse } from 'src/app/core/models/recipe/recipe.model';
import { RecipeService } from 'src/app/shared/services/recipe/recipe.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  recipes$ = new Observable<RecipeResponse[]>();
  recipe = {} as RecipeResponse;
  constructor(private recipeService: RecipeService, private router: Router) { }

  ngOnInit(): void {
    this.getRecipes();
  }

  getRecipes(){
    this.recipes$ = this.recipeService.getPublicRecipes();
  }
  getRecipe(recipe: RecipeResponse){
    this.router.navigate([`receita/${recipe.id}`]);
  }
}
