import { TranslateService } from '@ngx-translate/core';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './pages/login/login.component';
import { FormsModule } from '@angular/forms';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { AuthRoutingModule } from './auth-routing.module';
import { ConstantsService, DBkeys } from '../../services/constants.service';
import { CurrencyService } from '../../services/currency.service';
import { AuthComponent } from './auth.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { ConfigurationService } from '../../services/configuration.service';
// import { AppTranslationService } from '../../services/app-translation.service';
import { OidcHelperService } from '../../services/oidc-helper.service';
import { AuthService } from '../../services/auth.service';
import { AddproductComponent } from './pages/addproduct/addproduct.component';

@NgModule({
  declarations: [
    LoginComponent,
    AddproductComponent,
  ],
  imports: [
    TooltipModule,    //add bootstrap
    CommonModule,     // надо для Router
    AuthRoutingModule,  // надо для Router локального(в этом модуле)
    FormsModule,    //add [(ngModel)]
    // AuthModule
  ],
  providers: [
    CurrencyService,
    ConstantsService,
    OidcHelperService,//
    ConfigurationService,//
    AuthService,
    DBkeys
    // TranslateService,//
  ],
  bootstrap: [
    AuthComponent
  ]
})
export class AuthModule { }
