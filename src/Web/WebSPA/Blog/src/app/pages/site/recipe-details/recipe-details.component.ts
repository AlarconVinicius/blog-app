import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { EDifficulty } from 'src/app/models/blog/difficulty/difficulty.enum';
import { Recipe } from 'src/app/models/blog/recipe/recipe.model';
import { RecipeService } from 'src/app/services/blog/recipe/recipe.service';

@Component({
  selector: 'app-recipe-details',
  templateUrl: './recipe-details.component.html',
  styleUrls: ['./recipe-details.component.css']
})
export class RecipeDetailsComponent implements OnInit {
  recipeData$ = new Observable<Recipe>();
  recipeData = {} as Recipe;
  recipeId: string = '';
  createdAt: any;
  difficultyMapped: string = '';
  constructor(private recipeService: RecipeService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getCurrentUrl();
  }
  getRecipesById(id:string){
    this.recipeService.getPublicRecipesById(id).subscribe(data => {
      this.recipeData = data;
      this.createdAt = data.createdAt;
      this.difficultyMapped = this.mapDifficulty(data.difficulty.id);
    });
  }

  getCurrentUrl(){
    const url = this.route.snapshot.url; // Obtém os segmentos da URL
    if (url.length > 0) {
      const segments = url.map(segment => segment.path); // Obtém os caminhos da URL
      this.recipeId = segments[segments.length - 1]; // Pega o último segmento (que é o ID)
      this.getRecipesById(this.recipeId);
    }
  }
  mapDifficulty(difficulty: number): string {
    switch (difficulty) {
      case EDifficulty.Fácil:
        return EDifficulty[EDifficulty.Fácil];
      case EDifficulty.Médio:
        return EDifficulty[EDifficulty.Médio];
      case EDifficulty.Difícil:
        return EDifficulty[EDifficulty.Difícil];
      default:
        return EDifficulty[EDifficulty.Fácil]; // Valor padrão caso a correspondência não seja encontrada
    }
  }
}
