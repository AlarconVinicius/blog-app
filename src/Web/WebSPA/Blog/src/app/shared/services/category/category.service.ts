import { Injectable } from '@angular/core';
import { BaseService } from '../base/base.service';
import { HttpClient } from '@angular/common/http';
import { CategoryRequest, CategoryResponse } from 'src/app/core/models/category/category.model';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService extends BaseService {

  private categoryUrl = `${this.BlogApi}`+ '/categories';
  constructor(private httpClient: HttpClient) { super(); }

  getPublicCategories(): Observable<CategoryResponse[]> {
    return this.httpClient.get<{ data: CategoryResponse[] }>(this.categoryUrl, this.getHeaderJson())
      .pipe(
        map((response: { data: CategoryResponse[] }) => response.data)
      );
  }
  getPublicCategoriesById(id: string): Observable<CategoryResponse> {
    return this.httpClient.get<{ data: CategoryResponse }>(this.categoryUrl + '/' + id, this.getHeaderJson())
      .pipe(
        map((response: { data: CategoryResponse }) => response.data)
      );
  }
  postAuthCategory(category: CategoryRequest){
    return this.httpClient.post<void>(this.categoryUrl, category, this.getAuthHeaderJson());
  }
  putAuthCategory(id: string, category: CategoryRequest){
    return this.httpClient.put<void>(this.categoryUrl + '/' + id, category, this.getAuthHeaderJson());
  }
  deleteAuthCategory(id: string){
    return this.httpClient.delete<void>(this.categoryUrl + '/' + id, this.getAuthHeaderJson());
  }
}
