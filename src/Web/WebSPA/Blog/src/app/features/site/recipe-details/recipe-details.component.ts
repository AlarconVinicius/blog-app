import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RecipeResponse } from 'src/app/core/models/recipe/recipe.model';
import { RecipeUtils } from 'src/app/shared/helpers/recipe/recipe-utils';
import { RecipeService } from 'src/app/shared/services/recipe/recipe.service';

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
  constructor(private recipeUtils: RecipeUtils, private recipeService: RecipeService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.recipeId = this.recipeUtils.getIdFromCurrentUrl(this.route.snapshot.url);
    this.getRecipesById(this.recipeId);
  }
  getRecipesById(id:string){
    this.recipeService.getPublicRecipesById(id).subscribe(data => {
      this.recipeData = data;
      this.createdAt = data.createdAt;
      this.difficultyMapped = this.recipeUtils.mapDifficulty(data.difficulty.id);
    });
  }  
}
