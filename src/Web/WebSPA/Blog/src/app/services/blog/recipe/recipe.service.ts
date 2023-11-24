import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Recipe } from 'src/app/models/blog/recipe/recipe.model';
import { Observable, map } from 'rxjs';
import { RecipeAdd } from 'src/app/models/blog/recipe/recipe-add';
import { BaseService } from '../../base/base.service';
@Injectable({
  providedIn: 'root'
})
export class RecipeService extends BaseService {

  private publicUrl = `${environment.api}`+ '/recipes';
  private adminUrl = `${environment.api}`+ '/admin/recipes';
  constructor(private httpClient: HttpClient) { super(); }

  getPublicRecipes(): Observable<Recipe[]> {
    return this.httpClient.get<{ data: Recipe[] }>(this.publicUrl, this.getHeaderJson())
      .pipe(
        map((response: { data: Recipe[] }) => response.data)
      );
  }
  getPublicRecipesById(id: string): Observable<Recipe> {
    return this.httpClient.get<{ data: Recipe }>(this.publicUrl + '/' + id, this.getHeaderJson())
      .pipe(
        map((response: { data: Recipe }) => response.data)
      );
  }
  postAuthRecipe(recipe: RecipeAdd){
    return this.httpClient.post<void>(this.adminUrl, recipe, this.getAuthHeaderJson());
  }
}
