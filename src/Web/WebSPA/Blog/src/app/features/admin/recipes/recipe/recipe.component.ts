import { Component, OnInit } from '@angular/core';
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
  
  constructor(private recipeService: RecipeService, private localStorage: LocalStorageUtils) { }

  ngOnInit(): void {
    this.getRecipes();
  }
  getRecipes(){
    var userId = this.localStorage.getUserId();
    this.recipes$ = this.recipeService.getRecipes(userId);
  }
}
