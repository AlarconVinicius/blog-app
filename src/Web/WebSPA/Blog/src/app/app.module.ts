import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { EditorModule } from '@tinymce/tinymce-angular';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './core/components/admin/header/header.component';
import { FooterComponent } from './core/components/admin/footer/footer.component';
import { SidebarComponent } from './core/components/admin/sidebar/sidebar.component';
import { AlertsComponent } from './core/components/alerts/alerts.component';

import { HomeComponent } from './features/site/home/home.component';

import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';

import { DashboardComponent } from './features/admin/dashboard/dashboard.component';
import { RecipeComponent } from './features/admin/recipes/recipe/recipe.component';
import { AddUpdRecipeComponent } from './features/admin/recipes/add-upd/add-upd-recipe.component';
import { CategoryComponent } from './features/admin/categories/category/category.component';
import { UsersProfileComponent } from './features/admin/users-profile/users-profile.component';
import { ContactComponent } from './features/admin/contacts/contact/contact.component';
import { FaqComponent } from './features/admin/faq/faq.component';

import { Error404Component } from './features/error/error404/error404.component';
import { RecipeDetailsComponent } from './features/site/recipe-details/recipe-details.component';
import { SiteHeaderComponent } from './core/components/site/header/site-header.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    SidebarComponent,
    AlertsComponent,
    UsersProfileComponent,
    
    SiteHeaderComponent,
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
    HttpClientModule,
    EditorModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
