import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Recipe } from 'src/app/models/blog/recipe/recipe.model';
import { Observable, map } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  private url = environment.api;
  constructor(private httpClient: HttpClient) { }

  // getPublicRecipes(){
  //   return this.httpClient.get<any>(this.url + '/recipes').pipe(data ? Object.values(data) : Recipe[]);
  // }
  getPublicRecipes(): Observable<Recipe[]> {
    return this.httpClient.get<{ data: Recipe[] }>(this.url + '/recipes')
      .pipe(
        map((response: { data: Recipe[] }) => response.data)
      );
  }
  getPublicRecipesById(id: string): Observable<Recipe> {
    return this.httpClient.get<{ data: Recipe }>(this.url + '/recipes/' + id)
      .pipe(
        map((response: { data: Recipe }) => response.data)
      );
  }
}
