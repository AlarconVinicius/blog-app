import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { RecipeResponse } from 'src/app/core/models/recipe/recipe.model';
import { UserService } from 'src/app/shared/services/user/user.service';

@Component({
  selector: 'app-site-favorite-recipes',
  templateUrl: './site-favorite-recipes.component.html',
  styleUrls: ['./site-favorite-recipes.component.css']
})
export class SiteFavoriteRecipesComponent implements OnInit {
  title: string = '';
  search: string = '';
  recipes$ = new Observable<RecipeResponse[]>();
  showNoRecipesMessage: boolean = false;

  constructor(private userService: UserService, private route: ActivatedRoute, private router: Router, private titleService: Title) { }

  ngOnInit() {
    this.titleService.setTitle("Receitas Favoritas | Receitas de Casal");
    this.title = 'Receitas Favoritas';
    this.getFavoriteRecipes();
  }
  getFavoriteRecipes(){
    this.userService.getFavoriteRecipes().subscribe(recipes => {
      this.recipes$ = of(recipes);
      this.hasRecipes(recipes);
    });
  }
  hasRecipes(recipes: RecipeResponse[]){
    this.showNoRecipesMessage = !recipes || recipes.length === 0;
  }
  getRecipe(recipe: RecipeResponse){
    this.router.navigate([`receita/${recipe.id}`]);
  }
}