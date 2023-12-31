import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AlertsComponent } from './core/components/alerts/alerts.component';

import { HomeComponent } from './features/site/home/home.component';

import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';

import { RecipeComponent } from './features/admin/recipes/recipe/recipe.component';
import { AddUpdRecipeComponent } from './features/admin/recipes/add-upd/add-upd-recipe.component';
import { CategoryComponent } from './features/admin/categories/category/category.component';
import { UsersProfileComponent } from './features/admin/users-profile/users-profile.component';
import { Error404Component } from './features/error/error404/error404.component';
import { RecipeDetailsComponent } from './features/site/recipe-details/recipe-details.component';
import { AuthGuard } from './core/guards/auth.guard';
import { SiteRecipesComponent } from './features/site/site-recipes/site-recipes.component';
import { SiteCategoriesComponent } from './features/site/site-categories/site-categories.component';
import { IsAdminGuard } from './core/guards/is-admin.guard';
import { SiteFavoriteRecipesComponent } from './features/site/site-favorite-recipes/site-favorite-recipes.component';

const authRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'registrar', component: RegisterComponent }
];
const adminRoutes: Routes = [
  { path: '', component: RecipeComponent },
  { path: 'receitas', component: RecipeComponent },
  { path: 'receitas/adicionar', component: AddUpdRecipeComponent },
  { path: 'receitas/editar/:id', component: AddUpdRecipeComponent },
  { path: 'categorias', component: CategoryComponent },
  { path: 'perfil', component: UsersProfileComponent }
];

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'receitas', component: SiteRecipesComponent },
  { path: 'receitas/favoritas', component: SiteFavoriteRecipesComponent, canActivate: [AuthGuard] },
  { path: 'receitas/busca/:busca', component: SiteRecipesComponent },
  { path: 'receitas/categoria/:categoria', component: SiteRecipesComponent },
  { path: 'categorias', component: SiteCategoriesComponent },
  { path: 'receita/:id', component: RecipeDetailsComponent },
  { path: 'auth', children: authRoutes },
  { path: 'admin', children: adminRoutes, canActivate: [AuthGuard, IsAdminGuard] },
  { path: 'perfil', component: UsersProfileComponent },
  { path: 'erro/404', component: Error404Component },
  { path: 'alerts', component: AlertsComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
