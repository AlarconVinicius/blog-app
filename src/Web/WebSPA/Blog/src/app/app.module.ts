import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './layouts/header/header.component';
import { FooterComponent } from './layouts/footer/footer.component';
import { SidebarComponent } from './layouts/sidebar/sidebar.component';
import { AlertsComponent } from './components/alerts/alerts.component';
import { UsersProfileComponent } from './pages/users-profile/users-profile.component';
import { PagesFaqComponent } from './pages/pages-faq/pages-faq.component';
import { PagesContactComponent } from './pages/pages-contact/pages-contact.component';
import { PagesError404Component } from './pages/pages-error404/pages-error404.component';

import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { RecipeComponent } from './pages/admin-pages/recipe/recipe.component';
import { AddUpdRecipeComponent } from './pages/admin-pages/add-upd-recipe/add-upd-recipe.component';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './pages/auth-pages/login/login.component';
import { CategoryComponent } from './pages/admin-pages/category/category.component';
import { RegisterComponent } from './pages/auth-pages/register/register.component';
import { HomeComponent } from './pages/site-pages/home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    SidebarComponent,
    AlertsComponent,
    UsersProfileComponent,
    PagesFaqComponent,
    PagesContactComponent,
    PagesError404Component,
    
    DashboardComponent,
    RecipeComponent,
    AddUpdRecipeComponent,
    LoginComponent,
    CategoryComponent,
    RegisterComponent,
    HomeComponent
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
