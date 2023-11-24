import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CategoryRequest, CategoryResponse } from 'src/app/core/models/category/category.model';
import { CategoryService } from 'src/app/shared/services/category/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  categories$ = new Observable<CategoryResponse[]>();

  categoryId: string = '';
  categoryModalId: string = '';
  categorySave: CategoryRequest = {} as CategoryRequest;
  modalTitle: string = '';
  constructor(private categoryService: CategoryService, private router: Router) { }

  ngOnInit(): void {
    this.getCategories();
  }
  getCategories(){
    this.categories$ = this.categoryService.getPublicCategories();
  }

  getModalTitle(){
    if (this.categoryModalId != ''){
      this.modalTitle = "Editar Categoria"
      this.categoryModalId = '';
    } else {
      this.modalTitle = "Adicionar Categoria"
    }
  }

  onCategory(id: string = ''){
    if(id == ''){
    } else {
      this.getCategoryById(id);
    }
    this.getModalTitle();

  }
  saveCategory(){
    if(this.categoryId == ''){
      this.createCategory();
    } else {
      this.updateCategory(this.categoryId);
    }
  }
  createCategory(){
    const category: CategoryRequest = {
      name: this.categorySave.name
    }
    this.categoryService.postAuthCategory(category).subscribe(_ => {
      this.clearCategoryFields();
      this.getCategories();
      this.router.navigate(['admin/categorias']);
    });
  }

  updateCategory(id: string){
    this.categoryId = id;
    const category: CategoryRequest = {
      name: this.categorySave.name
    }
    this.categoryService.putAuthCategory(id, category).subscribe(_ => {
      this.clearCategoryFields();
      this.getCategories();
      this.router.navigate(['admin/categorias']);
    });
  }
  deleteCategory(id: string){
    this.categoryId = id;
    this.categoryService.deleteAuthCategory(id).subscribe(_ => {
      this.clearCategoryFields();
      this.getCategories();
      this.router.navigate(['admin/categorias']);
    });
  }
  getCategoryById(id:string){
    this.categoryService.getPublicCategoriesById(id).subscribe(data => {
      this.categoryId = data.id;
      this.categoryModalId = data.id;
      this.categorySave.name = data.name;
      
      this.getModalTitle();
    });
  } 
  clearCategoryFields() {
    this.modalTitle = "";
    this.categoryId = "";
    this.categorySave.name = '';
  }
}
