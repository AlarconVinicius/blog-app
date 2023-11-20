import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AlertsComponent } from './components/alerts/alerts.component';
import { PagesContactComponent } from './pages/pages-contact/pages-contact.component';
import { PagesError404Component } from './pages/pages-error404/pages-error404.component';
import { PagesFaqComponent } from './pages/pages-faq/pages-faq.component';
import { UsersProfileComponent } from './pages/users-profile/users-profile.component';

import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { CategoryComponent } from './pages/admin-pages/category/category.component';
import { LoginComponent } from './pages/auth-pages/login/login.component';
import { RegisterComponent } from './pages/auth-pages/register/register.component';
import { RecipeComponent } from './pages/admin-pages/recipe/recipe.component';
import { AddUpdRecipeComponent } from './pages/admin-pages/add-upd-recipe/add-upd-recipe.component';
import { HomeComponent } from './pages/site-pages/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'auth/login', component: LoginComponent },
  { path: 'auth/registrar', component: RegisterComponent },
  { path: 'admin',component: DashboardComponent},
  { path: 'admin/dashboard', component: DashboardComponent },
  { path: 'admin/receitas', component: RecipeComponent },
  { path: 'admin/receitas/adicionar', component: AddUpdRecipeComponent },
  { path: 'admin/receitas/editar/:id', component: AddUpdRecipeComponent },
  { path: 'admin/categorias', component: CategoryComponent },
  { path: 'admin/perfil', component: UsersProfileComponent },
  { path: 'admin/contato', component: PagesContactComponent },
  { path: 'admin/faq', component: PagesFaqComponent },
  { path: 'erro/404', component: PagesError404Component },
  { path: 'alerts', component: AlertsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
