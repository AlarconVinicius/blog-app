import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RecipeResponse, RecipeRequest } from 'src/app/core/models/recipe/recipe.model';
import { Observable, map } from 'rxjs';
import { BaseService } from '../base/base.service';
@Injectable({
  providedIn: 'root'
})
export class RecipeService extends BaseService {

  private publicUrl = `${this.PublicUrl}`+ '/recipes';
  private adminUrl = `${this.AdminUrl}`+ '/recipes';
  constructor(private httpClient: HttpClient) { super(); }

  getRecipeById(id: string, userId?: string): Observable<RecipeResponse> {
    const url = userId 
                ? `${this.publicUrl}/${id}?userId=${userId}` 
                : `${this.publicUrl}/${id}`;
    return this.httpClient.get<{ data: RecipeResponse }>(url, this.getAuthHeaderJson())
      .pipe(
        map((response: { data: RecipeResponse }) => response.data)
      );
  }
  getRecipeByUrl(postUrl: string, userId?: string): Observable<RecipeResponse> {
    const url = userId 
                ? `${this.publicUrl}/url/${postUrl}?userId=${userId}` 
                : `${this.publicUrl}/url/${postUrl}`;
    return this.httpClient.get<{ data: RecipeResponse }>(url, this.getAuthHeaderJson())
      .pipe(
        map((response: { data: RecipeResponse }) => response.data)
      );
  }
  getRecipes(userId?: string): Observable<RecipeResponse[]> {
    const url = userId 
                ? `${this.publicUrl}?userId=${userId}` 
                : this.publicUrl;
    return this.httpClient.get<{ data: RecipeResponse[] }>(url, this.getAuthHeaderJson())
      .pipe(
        map((response: { data: RecipeResponse[] }) => response.data)
      );
  }
  getRecipesBySearch(search: string, userId?: string): Observable<RecipeResponse[]> {
    const url = userId 
                ? `${this.publicUrl}/search/${search}?userId=${userId}` 
                : `${this.publicUrl}/search/${search}`;
    return this.httpClient.get<{ data: RecipeResponse[] }>(url, this.getAuthHeaderJson())
      .pipe(
        map((response: { data: RecipeResponse[] }) => response.data)
      );
  }
  getRecipesByCategory(category: string, userId?: string): Observable<RecipeResponse[]> {
    const url = userId 
    ? `${this.publicUrl}/category/${category}?userId=${userId}` 
    : `${this.publicUrl}/category/${category}`;
    return this.httpClient.get<{ data: RecipeResponse[] }>(url, this.getAuthHeaderJson())
      .pipe(
        map((response: { data: RecipeResponse[] }) => response.data)
      );
  }
  postRecipe(recipe: RecipeRequest){
    return this.httpClient.post<void>(this.adminUrl, recipe, this.getAuthHeaderJson());
  }
  putRecipe(recipeId: string, recipe: RecipeRequest){
    return this.httpClient.put<void>(this.adminUrl+'/'+recipeId, recipe, this.getAuthHeaderJson());
  }
  deleteRecipe(recipeId: string){
    return this.httpClient.delete<void>(this.adminUrl+'/'+recipeId, this.getAuthHeaderJson());
  }
}
