import { ModuleWithProviders }  from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard, AdminGuard } from './auth.guard';

import { NewProductPageComponent }      from './pages/newProduct/newProductPage.component';
import { BrowsePageComponent }      from './pages/browse/browsePage.component';
import { UsersPageComponent }      from './pages/users/usersPage.component';
import { ProfilePageComponent } from './pages/profile/profilePage.component';
import { RegisterPageComponent } from './pages/register/registerPage.component';
import { LoginPageComponent } from './pages/login/loginPage.component';
import { ProductDetailsPageComponent } from './pages/productDetails/productDetailsPage.component';
import { PaperDetailsPageComponent } from './pages/paperDetails/paperDetailsPage.component';

const appRoutes: Routes = [
    {path: 'browse', component: BrowsePageComponent, canActivate: [AuthGuard]  },
    {path: 'browse/search/:term', component: BrowsePageComponent, canActivate: [AuthGuard]  },
    
    {path: 'read/:paperId/:pageNumber', component: PaperDetailsPageComponent, canActivate: [AuthGuard]  },
    
    {path: 'users', component: UsersPageComponent, canActivate: [AuthGuard, AdminGuard]  },
    {path: 'new', component: NewProductPageComponent, canActivate: [AuthGuard, AdminGuard]  },
    {path: 'profile', component: ProfilePageComponent, canActivate: [AuthGuard]  },
    {path: '', component: BrowsePageComponent, canActivate: [AuthGuard] },
    {path: 'browse/:id', component: ProductDetailsPageComponent, canActivate: [AuthGuard]},

    {path: 'register', component: RegisterPageComponent },
    {path: 'login', component: LoginPageComponent }
    
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
