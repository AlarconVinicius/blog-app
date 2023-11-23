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
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'receita/:id', component: RecipeDetailsComponent },
  { path: 'auth/login', component: LoginComponent },
  { path: 'auth/registrar', component: RegisterComponent },
  { path: 'admin',component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'admin/dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'admin/receitas', component: RecipeComponent, canActivate: [AuthGuard] },
  { path: 'admin/receitas/adicionar', component: AddUpdRecipeComponent, canActivate: [AuthGuard] },
  { path: 'admin/receitas/editar/:id', component: AddUpdRecipeComponent, canActivate: [AuthGuard] },
  { path: 'admin/categorias', component: CategoryComponent, canActivate: [AuthGuard] },
  { path: 'admin/perfil', component: UsersProfileComponent, canActivate: [AuthGuard] },
  { path: 'admin/contato', component: ContactComponent, canActivate: [AuthGuard] },
  { path: 'admin/faq', component: FaqComponent, canActivate: [AuthGuard] },
  { path: 'erro/404', component: Error404Component },
  { path: 'alerts', component: AlertsComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
