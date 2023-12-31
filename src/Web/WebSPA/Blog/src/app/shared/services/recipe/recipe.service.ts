import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RecipeResponse, RecipeRequest } from 'src/app/core/models/recipe/recipe.model';
import { Observable, map } from 'rxjs';
import { BaseService } from '../base/base.service';
@Injectable({
  providedIn: 'root'
})
export class RecipeService extends BaseService {

  private blogApi = `${this.BlogApi}`+ '/recipes';
  constructor(private httpClient: HttpClient) { super(); }

  getRecipeById(id: string, userId?: string): Observable<RecipeResponse> {
    const url = userId 
                ? `${this.blogApi}/${id}?userId=${userId}` 
                : `${this.blogApi}/${id}`;
    return this.httpClient.get<{ data: RecipeResponse }>(url, this.getAuthHeaderJson())
      .pipe(
        map((response: { data: RecipeResponse }) => response.data)
      );
  }
  getRecipeByUrl(postUrl: string, userId?: string): Observable<RecipeResponse> {
    const url = userId 
                ? `${this.blogApi}/url/${postUrl}?userId=${userId}` 
                : `${this.blogApi}/url/${postUrl}`;
    return this.httpClient.get<{ data: RecipeResponse }>(url, this.getAuthHeaderJson())
      .pipe(
        map((response: { data: RecipeResponse }) => response.data)
      );
  }
  getRecipes(userId?: string): Observable<RecipeResponse[]> {
    const url = userId 
                ? `${this.blogApi}?userId=${userId}` 
                : this.blogApi;
    return this.httpClient.get<{ data: RecipeResponse[] }>(url, this.getAuthHeaderJson())
      .pipe(
        map((response: { data: RecipeResponse[] }) => response.data)
      );
  }
  getRecipesBySearch(search: string, userId?: string): Observable<RecipeResponse[]> {
    const url = userId 
                ? `${this.blogApi}/search/${search}?userId=${userId}` 
                : `${this.blogApi}/search/${search}`;
    return this.httpClient.get<{ data: RecipeResponse[] }>(url, this.getAuthHeaderJson())
      .pipe(
        map((response: { data: RecipeResponse[] }) => response.data)
      );
  }
  getRecipesByCategory(category: string, userId?: string): Observable<RecipeResponse[]> {
    const url = userId 
    ? `${this.blogApi}/category/${category}?userId=${userId}` 
    : `${this.blogApi}/category/${category}`;
    return this.httpClient.get<{ data: RecipeResponse[] }>(url, this.getAuthHeaderJson())
      .pipe(
        map((response: { data: RecipeResponse[] }) => response.data)
      );
  }
  postRecipe(recipe: RecipeRequest){
    return this.httpClient.post<void>(this.blogApi, recipe, this.getAuthHeaderJson());
  }
  putRecipe(recipeId: string, recipe: RecipeRequest){
    return this.httpClient.put<void>(this.blogApi+'/'+recipeId, recipe, this.getAuthHeaderJson());
  }
  deleteRecipe(recipeId: string){
    return this.httpClient.delete<void>(this.blogApi+'/'+recipeId, this.getAuthHeaderJson());
  }
}
