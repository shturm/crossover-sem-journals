import { NgModule }      from '@angular/core';
import { HttpModule }      from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';

import { AppComponent }  from './app.component';
import { ProductComponent }  from './components/product/product.component';
import { AuthService } from './auth.service';
import { AuthGuard, AdminGuard } from './auth.guard';

import { routing } from './app.routes';
import { NewProductPageComponent } from './pages/newProduct/newProductPage.component';
import { BrowsePageComponent } from './pages/browse/browsePage.component';
import { LoginPageComponent } from './pages/login/loginPage.component';
import { RegisterPageComponent } from './pages/register/registerPage.component';
import { ProductDetailsPageComponent } from './pages/productDetails/productDetailsPage.component';
import { PaperDetailsPageComponent } from './pages/paperDetails/paperDetailsPage.component';
import { ProfilePageComponent } from './pages/profile/profilePage.component';
import { UsersPageComponent } from './pages/users/usersPage.component';


@NgModule({
  imports: [ BrowserModule, FormsModule, HttpModule, routing],
  providers: [AuthService, AuthGuard, AdminGuard],
  declarations: [ 
    // main
    AppComponent,
    ProductComponent,
    // AuthService,
    
    // page components
    PaperDetailsPageComponent,
    NewProductPageComponent,
    BrowsePageComponent,
    LoginPageComponent,
    RegisterPageComponent,
    ProductDetailsPageComponent,
    ProfilePageComponent,
    UsersPageComponent
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }