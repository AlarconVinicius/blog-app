import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './layouts/header/header.component';
import { FooterComponent } from './layouts/footer/footer.component';
import { SidebarComponent } from './layouts/sidebar/sidebar.component';
import { AlertsComponent } from './components/alerts/alerts.component';

import { HomeComponent } from './pages/site/home/home.component';

import { LoginComponent } from './pages/auth/login/login.component';
import { RegisterComponent } from './pages/auth/register/register.component';

import { DashboardComponent } from './pages/admin/dashboard/dashboard.component';
import { RecipeComponent } from './pages/admin/recipe/recipe.component';
import { AddUpdRecipeComponent } from './pages/admin/add-upd-recipe/add-upd-recipe.component';
import { CategoryComponent } from './pages/admin/category/category.component';
import { UsersProfileComponent } from './pages/admin/users-profile/users-profile.component';
import { ContactComponent } from './pages/admin/contact/contact.component';
import { FaqComponent } from './pages/admin/faq/faq.component';

import { Error404Component } from './pages/error/error404/error404.component';
import { RecipeDetailsComponent } from './pages/site/recipe-details/recipe-details.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    SidebarComponent,
    AlertsComponent,
    UsersProfileComponent,
    
    DashboardComponent,
    RecipeComponent,
    AddUpdRecipeComponent,
    LoginComponent,
    CategoryComponent,
    RegisterComponent,
    HomeComponent,
    ContactComponent,
    FaqComponent,
    Error404Component,
    RecipeDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
