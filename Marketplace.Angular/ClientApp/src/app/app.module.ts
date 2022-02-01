import { AuthGuardService } from './services/auth-guard.service';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HomeModule } from './modules/home/home.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ErrorComponent } from './pages/error/error.component';
import { AppRoutingModule } from './app-routing.module';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { ScrollToModule } from '@nicky-lenaers/ngx-scroll-to';
import { FooterComponent } from './pages/footer/footer.component';
import { HeaderComponent } from './pages/header/header.component';
import { AuthService } from './services/auth.service';
import { ConstantsService } from './services/constants.service';
import { CurrencyService } from './services/currency.service';
import { AuthModule } from './modules/auth/auth.module';
// import { CookieManagerService } from './services/cookie-manager.service';
import { AuthInterceptor, LogLevel, OidcConfigService } from 'angular-auth-oidc-client';

// let t: string[]=[];
// t.push("https://localhost:5001");
// export function configureAuth(oidcConfigService: OidcConfigService) {
//   return () =>
//     oidcConfigService.withConfigs({
//       authority:'http://localhost:4200/auth-callback' ,//
//       redirectUrl: 'http://localhost:4200/auth-callback',//
//       postLogoutRedirectUri: 'http://localhost:4200/',//
//       clientId: 'client_angular_id',//
//       scope: "openid profile",//
//       responseType: 'code',//
//       silentRenew: true,//
//       useRefreshToken: true,//.
//       renewTimeBeforeTokenExpiresInSeconds: 10,//.
//       logLevel: LogLevel.Debug,//.
//     });
// }

@NgModule({
  declarations: [
    AppComponent,
    ErrorComponent,
    FooterComponent,
    HeaderComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    RouterModule,// надо для Router
    AppRoutingModule,// надо для Router
    HomeModule,// новый роут для Router
    AuthModule,// новый роут для Router
    //CarouselModule,    //add carusel
    ScrollToModule.forRoot(), //add scrol, ндо чтобы была в App
    HttpClientModule,  //add работа с Http

    // HttpClientModule,
    // ReactiveFormsModule,
    // Directive
  ],
  providers: [
    CurrencyService,
    ConstantsService,
    AuthGuardService,
    AuthService,
    // CookieManagerService,
    // OidcConfigService,
    // {
    //   provide: APP_INITIALIZER,
    //   useFactory: configureAuth,
    //   deps: [OidcConfigService],
    //   multi: true,
    // },
    // {
    //   provide: HTTP_INTERCEPTORS,
    //   useClass: AuthInterceptor,
    //   multi: true,
    // },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
