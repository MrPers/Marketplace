import { OidcHelperService } from './../../../../services/oidc-helper.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
// import { AlertService } from '../../../../services/alert.service';
import { AuthService } from '../../../../services/auth.service';
import {ConstantsService, DBkeys, LoginResponse, User } from '../../../../services/constants.service';
import { CurrencyService } from '../../../../services/currency.service';
import { map } from 'rxjs/operators';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  error: boolean = false;
  returnUrl: string ="";
  userRegistration: User = new User();
  userAuthentication: User = new User();
  submitted: boolean = false;
  visibility: boolean = true;
  // rememberMe: boolean;

  constructor(
    private constantsService: ConstantsService,
    private http: HttpClient,
    private route: ActivatedRoute,
    private currencyService: CurrencyService,
    private oidcHelperService: OidcHelperService,
    private router: Router,
    // private alertService: AlertService,
    private authService: AuthService) { }

  ngOnInit(): void {
    // const routeParams = this.route.snapshot.paramMap;
    // this.returnUrl = String(routeParams.get('ReturnUrl')).split(':/')[1];
  }

  toggleOn(){
    this.visibility=true;
  }

  toggleOff(){
    this.visibility=false;
  }

  registrationSubmit() {
    this.currencyService.addUser(this.userRegistration)
      .subscribe(
        result => {
          this.visibility = true;
        },
        error => {
          if(error['status'] == 400) {
            this.error = true;
          }
        });
  }

  async authenticationSubmit() {
    this.authService.loginWithPassword(this.userAuthentication.userName, this.userAuthentication.password)
      .subscribe(
        user => {
          this.currencyService.authUser(this.userAuthentication)
            .subscribe(
             (data) =>{
               this.router.navigate(['']);
             },
             (error) => {
               // debugger;
             });
        },
        error => {
          debugger;
        });
  }

}
