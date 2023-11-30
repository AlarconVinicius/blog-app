import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { CategoryResponse } from 'src/app/core/models/category/category.model';
import { CategoryService } from 'src/app/shared/services/category/category.service';

@Component({
  selector: 'app-site-categories',
  templateUrl: './site-categories.component.html',
  styleUrls: ['./site-categories.component.css']
})
export class SiteCategoriesComponent implements OnInit {
  search: string = '';
  categories$ = new Observable<CategoryResponse[]>();

  constructor(private categoryService: CategoryService, private router: Router) { }

  ngOnInit(): void {
    this.getCategories();
  }

  getCategories(){
    this.categoryService.getPublicCategories().subscribe(categories => {
      this.categories$ = of(categories);
    });
  }
  onSearch(category: string){
    this.router.navigate([`receitas/categoria/${category}`]);
  }
}
