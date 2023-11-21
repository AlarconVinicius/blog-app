import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AlertsComponent } from './components/alerts/alerts.component';
import { ContactComponent } from './pages/admin/contact/contact.component';

import { HomeComponent } from './pages/site/home/home.component';

import { LoginComponent } from './pages/auth/login/login.component';
import { RegisterComponent } from './pages/auth/register/register.component';

import { DashboardComponent } from './pages/admin/dashboard/dashboard.component';
import { RecipeComponent } from './pages/admin/recipe/recipe.component';
import { AddUpdRecipeComponent } from './pages/admin/add-upd-recipe/add-upd-recipe.component';
import { CategoryComponent } from './pages/admin/category/category.component';
import { UsersProfileComponent } from './pages/admin/users-profile/users-profile.component';
import { FaqComponent } from './pages/admin/faq/faq.component';
import { Error404Component } from './pages/error/error404/error404.component';
import { RecipeDetailsComponent } from './pages/site/recipe-details/recipe-details.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'receita/:site-url', component: RecipeDetailsComponent },
  { path: 'auth/login', component: LoginComponent },
  { path: 'auth/registrar', component: RegisterComponent },
  { path: 'admin',component: DashboardComponent},
  { path: 'admin/dashboard', component: DashboardComponent },
  { path: 'admin/receitas', component: RecipeComponent },
  { path: 'admin/receitas/adicionar', component: AddUpdRecipeComponent },
  { path: 'admin/receitas/editar/:id', component: AddUpdRecipeComponent },
  { path: 'admin/categorias', component: CategoryComponent },
  { path: 'admin/perfil', component: UsersProfileComponent },
  { path: 'admin/contato', component: ContactComponent },
  { path: 'admin/faq', component: FaqComponent },
  { path: 'erro/404', component: Error404Component },
  { path: 'alerts', component: AlertsComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
