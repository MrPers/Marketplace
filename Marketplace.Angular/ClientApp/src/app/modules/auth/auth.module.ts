import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './pages/login/login.component';
import { FormsModule } from '@angular/forms';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { AuthRoutingModule } from './auth-routing.module';
import { ConstantsService } from '../../services/constants.service';
import { CurrencyService } from '../../services/currency.service';
import { AuthComponent } from './auth.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { LogoutComponent } from './pages/logout/logout.component';



@NgModule({
  declarations: [
    LoginComponent,
    LogoutComponent
  ],
  imports: [
    TooltipModule,    //add bootstrap
    CommonModule,     // надо для Router
    AuthRoutingModule,  // надо для Router локального(в этом модуле)
    FormsModule,    //add [(ngModel)]
  ],
  providers: [
    CurrencyService,
    ConstantsService
  ],
  bootstrap: [
    AuthComponent
  ]
})
export class AuthModule { }
