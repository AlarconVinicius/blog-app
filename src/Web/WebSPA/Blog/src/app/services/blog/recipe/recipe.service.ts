import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Recipe } from 'src/app/models/blog/recipe/recipe.model';
import { Observable, map } from 'rxjs';
import { RecipeAdd } from 'src/app/models/blog/recipe/recipe-add';
@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  private publicUrl = `${environment.api}`+ '/recipes';
  private adminUrl = `${environment.api}`+ '/admin/recipes';
  constructor(private httpClient: HttpClient) { }

  // getPublicRecipes(){
  //   return this.httpClient.get<any>(this.url + '/recipes').pipe(data ? Object.values(data) : Recipe[]);
  // }
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  getPublicRecipes(): Observable<Recipe[]> {
    return this.httpClient.get<{ data: Recipe[] }>(this.publicUrl)
      .pipe(
        map((response: { data: Recipe[] }) => response.data)
      );
  }
  getPublicRecipesById(id: string): Observable<Recipe> {
    return this.httpClient.get<{ data: Recipe }>(this.publicUrl + '/' + id)
      .pipe(
        map((response: { data: Recipe }) => response.data)
      );
  }
  postRecipe(recipe: RecipeAdd){
    return this.httpClient.post<void>(this.adminUrl, recipe, this.httpOptions);
  }
}
