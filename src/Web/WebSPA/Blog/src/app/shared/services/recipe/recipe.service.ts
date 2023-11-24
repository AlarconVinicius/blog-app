import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { RecipeResponse, RecipeRequest } from 'src/app/core/models/recipe/recipe.model';
import { Observable, map } from 'rxjs';
import { BaseService } from '../base/base.service';
@Injectable({
  providedIn: 'root'
})
export class RecipeService extends BaseService {

  private publicUrl = `${environment.api}`+ '/recipes';
  private adminUrl = `${environment.api}`+ '/admin/recipes';
  constructor(private httpClient: HttpClient) { super(); }

  getPublicRecipes(): Observable<RecipeResponse[]> {
    return this.httpClient.get<{ data: RecipeResponse[] }>(this.publicUrl, this.getHeaderJson())
      .pipe(
        map((response: { data: RecipeResponse[] }) => response.data)
      );
  }
  getPublicRecipesById(id: string): Observable<RecipeResponse> {
    return this.httpClient.get<{ data: RecipeResponse }>(this.publicUrl + '/' + id, this.getHeaderJson())
      .pipe(
        map((response: { data: RecipeResponse }) => response.data)
      );
  }
  postAuthRecipe(recipe: RecipeRequest){
    return this.httpClient.post<void>(this.adminUrl, recipe, this.getAuthHeaderJson());
  }
}
