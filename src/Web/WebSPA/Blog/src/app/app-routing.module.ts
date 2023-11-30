import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AlertsComponent } from './core/components/alerts/alerts.component';
import { ContactComponent } from './features/admin/contacts/contact/contact.component';

import { HomeComponent } from './features/site/home/home.component';

import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';

import { DashboardComponent } from './features/admin/dashboard/dashboard.component';
import { RecipeComponent } from './features/admin/recipes/recipe/recipe.component';
import { AddUpdRecipeComponent } from './features/admin/recipes/add-upd/add-upd-recipe.component';
import { CategoryComponent } from './features/admin/categories/category/category.component';
import { UsersProfileComponent } from './features/admin/users-profile/users-profile.component';
import { FaqComponent } from './features/admin/faq/faq.component';
import { Error404Component } from './features/error/error404/error404.component';
import { RecipeDetailsComponent } from './features/site/recipe-details/recipe-details.component';
import { AuthGuard } from './core/guards/auth.guard';
import { SiteRecipesComponent } from './features/site/site-recipes/site-recipes.component';

const authRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'registrar', component: RegisterComponent }
];
const adminRoutes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'receitas', component: RecipeComponent },
  { path: 'receitas/adicionar', component: AddUpdRecipeComponent },
  { path: 'receitas/editar/:id', component: AddUpdRecipeComponent },
  { path: 'categorias', component: CategoryComponent },
  { path: 'perfil', component: UsersProfileComponent },
  { path: 'contato', component: ContactComponent },
  { path: 'faq', component: FaqComponent }
];

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'receitas', component: SiteRecipesComponent },
  { path: 'receitas/busca/:busca', component: SiteRecipesComponent },
  { path: 'receita/:id', component: RecipeDetailsComponent },
  { path: 'auth', children: authRoutes },
  { path: 'admin', children: adminRoutes, canActivate: [AuthGuard] },
  { path: 'erro/404', component: Error404Component },
  { path: 'alerts', component: AlertsComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
