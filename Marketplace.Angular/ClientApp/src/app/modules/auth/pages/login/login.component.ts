import { TokenService } from './../../../../services/token.service';
import { OidcHelperService } from './../../../../services/oidc-helper.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
// import { AlertService } from '../../../../services/alert.service';
import { AuthService } from '../../../../services/auth.service';
import {ConstantsService, DBkeys, LoginResponse, User } from '../../../../services/constants.service';
import { CurrencyService } from '../../../../services/currency.service';
import { JwtHelper } from '../../../../services/jwt-helper';
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
    private tokenService: TokenService,
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
    this.authService.loginWithPassword(this.userAuthentication.userName, this.userAuthentication.password, false)
      .subscribe(
        user => {
          debugger;
        },
        error => {
          debugger;
        });


    // const OAUTH_CLIENT = 'express-client';
    // const OAUTH_SECRET = 'express-secret';
    // const API_URL = 'http://localhost:5001/';
    // const HTTP_OPTIONS = {
    //   headers: new HttpHeaders({
    //     'Content-Type': 'application/x-www-form-urlencoded',
    //     Authorization: 'Basic ' + btoa(OAUTH_CLIENT + ':' + OAUTH_SECRET)
    //   })
    // };
    //     this.tokenService.removeToken();
    //     this.tokenService.removeRefreshToken();
    //     const body = new HttpParams()
    //       .set('username', this.userAuthentication.userName)
    //       .set('password', this.userAuthentication.password)
    //       .set('grant_type', 'password');

    //     return this.http.post<any>(API_URL + 'oauth/token', body, HTTP_OPTIONS)
    //       // .pipe(
    //       //   tap(res => {
    //       //     this.tokenService.saveToken(res.access_token);
    //       //     this.tokenService.saveRefreshToken(res.refresh_token);
    //       //   }),
    //       //   catchError(AuthService.handleError)
    //       // )
    //       .subscribe(() => {
    //         // this.isLoadingResults = false;
    //         debugger;
    //         this.router.navigate(['/secure']).then(_ => console.log('You are secure now!'));
    //       }, (err: any) => {
    //         debugger;
    //         console.log(err);
    //         // this.isLoadingResults = false;
    //       });


    // this.currencyService.userAuthentication(this.userAuthentication)
    //   .subscribe((data: any) =>
    //   {
    //     this.currencyService.authCallback(this.returnUrl)
    //     .subscribe(
    //       (data) =>{
    //         // debugger;
    //       },
    //       (error) => {
    //         // var y: string = error.url.split('https://localhost:5001/')[1];
    //         this.authService.completeAuthentication();
    //         // .then(() =>{
    //         // });
    //         this.router.navigate(['']);
    //       });
    //   });
  }

}
