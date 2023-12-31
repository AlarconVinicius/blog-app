import { Injectable } from '@angular/core';
import { BaseService } from '../base/base.service';
import { HttpClient } from '@angular/common/http';
import { AuthorRequest, AuthorResponse, UserPasswordRequest } from 'src/app/core/models/author/author.model';
import { Observable, map } from 'rxjs';
import { RecipeResponse } from 'src/app/core/models/recipe/recipe.model';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService{

  private blogApi = `${this.BlogApi}`+ '/users';

  constructor(private httpClient: HttpClient) { super(); }

  getAuthUser(): Observable<AuthorResponse> {
    return this.httpClient.get<{ data: AuthorResponse }>(this.blogApi, this.getAuthHeaderJson())
      .pipe(
        map((response: { data: AuthorResponse }) => response.data)
      );
  }

  putAuthUser(user: AuthorRequest) {
    return this.httpClient.put<void>(this.blogApi, user, this.getAuthHeaderJson());
  }

  putChangeUserPassword(userPassword: UserPasswordRequest) {
    return this.httpClient.put<void>(`${this.blogApi}/change-password`, userPassword, this.getAuthHeaderJson());
  }

  postFavoriteRecipe(recipeId: string) {
    return this.httpClient.post<void>(`${this.blogApi}/favorite-recipes/${recipeId}`, {}, this.getAuthHeaderJson());
  }

  getFavoriteRecipes(): Observable<RecipeResponse[]> {
    return this.httpClient.get<{ data: RecipeResponse[] }>(`${this.blogApi}/favorite-recipes`, this.getAuthHeaderJson())
      .pipe(
        map((response: { data: RecipeResponse[] }) => response.data)
      );
  }
}
