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
