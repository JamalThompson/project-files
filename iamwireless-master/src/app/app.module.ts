import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { ProductComponent } from './product/product.component';
import {HttpClientModule} from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {RouterModule, Routes} from '@angular/router';


import { ProductCreateComponent } from './product-create/product-create.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { HomeComponent } from './home/home.component';
import { RepairsComponent } from './repairs/repairs.component';
import {FinancingComponent} from './financing/financing.component';
import { LoginComponent } from './login/login.component';
import { CreateLoginComponent } from './create-login/create-login.component';
import {AuthenticationService} from './authentication.service';
import {AuthGuardService} from './authguard.service';
import { LogoutComponent } from './logout/logout.component';
import { SalesComponent } from './sales/sales.component';
import { SmcComponent } from './smc/smc.component';
import { ServicesComponent } from './services/services.component';
import {SearchCreateComponent} from './search-create/search-create.component';
import {from} from 'rxjs/observable/from';
import {AuthServiceConfig, FacebookLoginProvider, SocialLoginModule} from 'angular5-social-login';


const appRoutes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
    data: {title: 'Home'}
  },
  {
    path: 'products',
    component: ProductComponent,
    data: {title: 'product List'}
  },
  {
    path: 'product-detail/:id',
    component: ProductDetailComponent,
    data: {title: 'Product Details'}
  },
  {
    path: 'product-create',
    component: ProductCreateComponent,
    data: {title: 'Create Product'}
  },
  {
  path: 'repairs',
   component: RepairsComponent,
   data: {title: 'Repairs'}
  },
  {
    path: 'financing',
    component: FinancingComponent,
    data: {title: 'No Credit Financing'}
  },
  {
  path: 'login',
  component: LoginComponent,
  data: {title: 'Login'}
  },
  {
    path: 'create-login',
    component: CreateLoginComponent,
    data: {title: 'Create-Login'}
  },
  {
    path: 'search-create',
    component: SearchCreateComponent,
    data: {title: 'Search-Create'}
  },
  {
    path: 'logout',
    component: LogoutComponent,
    data: {title: 'Logout'}
  },
  {
    path: 'sales',
    component: SalesComponent,
    data: {title: 'Sales'}
  },
  {
    path: 'services',
    component: ServicesComponent,
    data: {title: 'Services'}
  },
  {
    path: 'smc',
    component: SmcComponent,
    data: {title: 'Smc'}
  },
  { path: 'register', component: CreateLoginComponent },
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
];

export function getAuthServiceConfigs() {
  const config = new AuthServiceConfig(
    [
      {
        id: FacebookLoginProvider.PROVIDER_ID,
        provider: new FacebookLoginProvider('1611891148901668')
      },

    ]
  );
  return config;
}

@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    ProductCreateComponent,
    ProductDetailComponent,
    HomeComponent,
    LoginComponent,
    RepairsComponent,
    FinancingComponent,
    CreateLoginComponent,
    LogoutComponent,
    SalesComponent,
    SmcComponent,
    ServicesComponent,
    SearchCreateComponent

  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    SocialLoginModule,
    RouterModule.forRoot(
      appRoutes,
      {enableTracing: true}
    )
  ],
  providers: [AuthenticationService,
    AuthGuardService, {
      provide: AuthServiceConfig,
      useFactory: getAuthServiceConfigs
    }],
  bootstrap: [AppComponent],
})




export class AppModule { }
