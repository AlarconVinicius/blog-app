import { Component, OnInit } from '@angular/core'
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryResponse } from 'src/app/core/models/category/category.model';
import { EDifficulty } from 'src/app/core/models/difficulty/difficulty.model';
import { ImageRequest } from 'src/app/core/models/image/image.model';
import { RecipeRequest } from 'src/app/core/models/recipe/recipe.model';
import { LocalStorageUtils } from 'src/app/shared/helpers/localstorage/localstorage';
import { CategoryService } from 'src/app/shared/services/category/category.service';
import { RecipeService } from 'src/app/shared/services/recipe/recipe.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-add-upd-recipe',
  templateUrl: './add-upd-recipe.component.html',
  styleUrls: ['./add-upd-recipe.component.css']
})
export class AddUpdRecipeComponent implements OnInit {

  userId: string = '';
  tinyApi: string = environment.tinyMceApi;
  recipeId: string = '';
  categoryDb: string = '';
  routeRecipeId: string = '';

  pageTitle: string = '';

  recipe = {} as RecipeRequest;
  uploadImage = {} as ImageRequest;
  categories: CategoryResponse[] = [];
  difficulties: { id: number; nome: string }[] = [
    { id: Number(EDifficulty.Fácil), nome: 'Fácil' },
    { id: Number(EDifficulty.Médio), nome: 'Médio' },
    { id: Number(EDifficulty.Difícil), nome: 'Difícil' }
  ];

  constructor(private localStorage: LocalStorageUtils, private recipeService: RecipeService, private categoryService: CategoryService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.userId = this.localStorage.getUserId();
    this.getCategories();
    this.isUpdate();
  }

  isUpdate(){
    this.route.params.subscribe(params => {
      if (params['id']) {
        this.routeRecipeId = params['id'];
        this.getRecipesById(this.routeRecipeId);
      }
    });
    this.getPageTitle();
    this.routeRecipeId = '';
  }
  getRecipesById(id:string){
    this.recipeService.getRecipeById(id, this.userId).subscribe(data => {
      this.recipeId = data.id;
      this.recipe.title = data.title;
      this.recipe.categoryId = data.category.id;
      this.categoryDb = data.category.name;
      this.recipe.difficulty = data.difficulty.id;
      this.recipe.preparationTime = data.preparationTime;
      this.recipe.preparationSteps = data.preparationSteps;
      this.recipe.ingredients = data.ingredients;
      this.recipe.servings = data.servings;
      this.uploadImage.name = data.coverImage.name;
      this.uploadImage.file = data.coverImage.file;
    });
  } 
  getImageUrl(base64: string) {
    return 'data:image/jpeg;base64,' + base64;
  }
  getPageTitle(){
    if (this.routeRecipeId != ''){
      this.pageTitle = "Editar Receita"
    } else {
      this.pageTitle = "Adicionar Receita "
    }
  }
  saveRecipe() {
    const recipe: RecipeRequest = {
      title: this.recipe.title,
      preparationSteps: this.recipe.preparationSteps,
      blogId: "2a2ff613-6f3b-4dd8-9fd6-a2f824b67b62",
      categoryId: this.recipe.categoryId,
      difficulty: Number(this.recipe.difficulty),
      preparationTime: this.recipe.preparationTime,
      servings: this.recipe.servings,
      ingredients: this.recipe.ingredients,
      image: this.uploadImage
    };
    var recipeJson = JSON.stringify(recipe);
    if(this.recipeId != ''){
      this.recipeService.putRecipe(this.recipeId, recipe).subscribe(_ => {
      this.recipeService.getRecipes(this.userId)
      this.router.navigate([`admin/receitas`]);
      });
    } else {
      this.recipeService.postRecipe(recipe).subscribe(_ => {
      this.recipeService.getRecipes(this.userId)
      this.router.navigate([`admin/receitas`]);
    });
    }
    console.log(recipeJson);
  }
  getCategories(){
    this.categoryService.getPublicCategories().subscribe(data => {
      this.categories = data;
    })
  }
  upload(event: any) {
    const file = event.target.files[0];
    const name = event.target.files[0].name;
    const reader = new FileReader();

    reader.onload = () => {
        if (typeof reader.result === 'string') {
            let base64Image = reader.result as string;
            
            if (base64Image.startsWith('data:image/jpeg;base64,')) {
                base64Image = base64Image.replace('data:image/jpeg;base64,', '');
            }
            
            this.uploadImage.file = base64Image;
            this.uploadImage.name = name;
        } else {
            console.error('Não foi possível ler o arquivo como uma string.');
        }
    };

    if (file) {
        reader.readAsDataURL(file);
    }
  }
}
