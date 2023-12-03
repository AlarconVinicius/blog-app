import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { RecipeResponse } from 'src/app/core/models/recipe/recipe.model';
import { LocalStorageUtils } from 'src/app/shared/helpers/localstorage/localstorage';
import { RecipeUtils } from 'src/app/shared/helpers/recipe/recipe-utils';
import { RecipeService } from 'src/app/shared/services/recipe/recipe.service';
import { UserService } from 'src/app/shared/services/user/user.service';

@Component({
  selector: 'app-recipe-details',
  templateUrl: './recipe-details.component.html',
  styleUrls: ['./recipe-details.component.css']
})
export class RecipeDetailsComponent implements OnInit {
  recipeId: string = '';
  recipeData = {} as RecipeResponse;
  createdAt: any;
  difficultyMapped: string = '';
  constructor(
    private recipeUtils: RecipeUtils, 
    private recipeService: RecipeService, 
    private route: ActivatedRoute, 
    private router: Router, 
    private titleService: Title,
    private localStorage: LocalStorageUtils,
    private userService: UserService) { }

  ngOnInit(): void {
    this.titleService.setTitle("Receita | Receitas de Casal");
    this.recipeId = this.recipeUtils.getIdFromCurrentUrl(this.route.snapshot.url);
    this.getRecipesById(this.recipeId);
  }
  getRecipesById(id:string){
    this.recipeService.getRecipeById(id).subscribe(data => {
      this.recipeData = data;
      this.createdAt = data.createdAt;
      this.difficultyMapped = this.recipeUtils.mapDifficulty(data.difficulty.id);
    });
  }  
  onFavorite(){
    if(!this.localStorage.isLoggedIn()){
      alert("Faça login para favoritar essa receita!");
      this.router.navigate([`auth/login`]);
      return;
    }
    this.userService.postFavoriteRecipe(this.recipeData.id).subscribe(_ => {
      alert("Receita favoritada!");
    });
    
  }
}
