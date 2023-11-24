import { Injectable } from '@angular/core';
import { BaseService } from '../base/base.service';
import { HttpClient } from '@angular/common/http';
import { CategoryRequest, CategoryResponse } from 'src/app/core/models/category/category.model';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService extends BaseService {

  private publicUrl = `${this.PublicUrl}`+ '/categories';
  private adminUrl = `${this.AdminUrl}`+ '/categories';
  constructor(private httpClient: HttpClient) { super(); }

  getPublicCategories(): Observable<CategoryResponse[]> {
    return this.httpClient.get<{ data: CategoryResponse[] }>(this.publicUrl, this.getHeaderJson())
      .pipe(
        map((response: { data: CategoryResponse[] }) => response.data)
      );
  }
  getPublicCategoriesById(id: string): Observable<CategoryResponse> {
    return this.httpClient.get<{ data: CategoryResponse }>(this.publicUrl + '/' + id, this.getHeaderJson())
      .pipe(
        map((response: { data: CategoryResponse }) => response.data)
      );
  }
  postAuthCategory(category: CategoryRequest){
    return this.httpClient.post<void>(this.adminUrl, category, this.getAuthHeaderJson());
  }
  putAuthCategory(id: string, category: CategoryRequest){
    return this.httpClient.put<void>(this.adminUrl + '/' + id, category, this.getAuthHeaderJson());
  }
  deleteAuthCategory(id: string){
    return this.httpClient.delete<void>(this.adminUrl + '/' + id, this.getAuthHeaderJson());
  }
}
