

import { Injectable } from '@angular/core';
import { Router, NavigationExtras } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { ConfigurationService } from './configuration.service';
import { DBkeys, LoginResponse, AccessToken } from './constants.service';
import { JwtHelper } from './jwt-helper';

import { LocalStoreManager } from './local-store-manager.service';
import { OidcHelperService } from './oidc-helper.service';
import { Utilities } from './utilities';
import { PermissionValues } from './permission.model';
// import { ConfigurationService } from './configuration.service';
// import { DBkeys } from './db-keys';
// import { JwtHelper } from './jwt-helper';
// import { AccessToken, LoginResponse } from '../models/login-response.model';
// import { User } from '../models/user.model';
// import { PermissionValues } from '../models/permission.model';

@Injectable()
export class AuthService {

  public get loginUrl() { return this.configurations.loginUrl; }
  public loginRedirectUrl: string ="";
  private previousIsLoggedInCheck = false;
  private loginStatus = new Subject<boolean>();
  // public reLoginDelegate: () => void;


  get currentUser(): User  {

    const user = this.localStorage.getDataObject<User>(DBkeys.CURRENT_USER);
    this.reevaluateLoginStatus(user);

    return user as User;
  }

  get isLoggedIn(): boolean {
      return this.currentUser != null;
    }

    get rememberMe(): boolean {
      return this.localStorage.getDataObject<boolean>(DBkeys.REMEMBER_ME) === true;
    }

    constructor(private router:Router,private localStorage: LocalStoreManager, private oidcHelperService: OidcHelperService,
      private configurations: ConfigurationService) {
        this.initializeLoginStatus();
    }

    //endpoint
    refreshLogin() {
      return this.oidcHelperService.refreshLogin()
        .pipe(map((resp: any) => this.processLoginResponse(resp, this.rememberMe)));
    }

    get isSessionExpired(): boolean {
      return this.oidcHelperService.isSessionExpired;
    }

    redirectLoginUser() {
      // let user = this.currentUser as User;
      let redirect  = this.loginRedirectUrl && this.loginRedirectUrl !== '/' && this.loginRedirectUrl !== ConfigurationService.defaultHomeUrl ? this.loginRedirectUrl : null;
      // debugger;
        redirect = "https://localhost:5001/auth-callback";


      // if (!redirect && user.roles.find(el => el === Roles.SuperAdministrator)) {
      //   redirect = ConfigurationService.adminAreaUrl;
      // }
      // if (!redirect && user.roles.find(el => el === Roles.Customer)) {
      //   redirect = ConfigurationService.clientAreaUrl;
      // }
      // this.loginRedirectUrl = null;
      // if (redirect === null) {
      //   redirect = ConfigurationService.defaultHomeUrl;
      //     throw new Error("User doesn't have permission!");
      // }
      const urlParamsAndFragment = Utilities.splitInTwo(redirect, '#');
      const urlAndParams = Utilities.splitInTwo(urlParamsAndFragment.firstPart, '?');

      const navigationExtras: NavigationExtras = {
        fragment: urlParamsAndFragment.secondPart,
        queryParams: Utilities.getQueryParamsFromString(urlAndParams.secondPart),
        queryParamsHandling: 'merge'
      };

      this.router.navigate([urlAndParams.firstPart], navigationExtras);
    }

    // reLogin() {
    //   if (this.reLoginDelegate) {
    //     this.reLoginDelegate();
    //   } else {
    //     this.redirectForLogin();
    //   }
    // }

    loginWithPassword(userName: string, password: string, rememberMe?: boolean) {
      // if (this.isLoggedIn) {
      //   this.logout();
      // }

      return this.oidcHelperService.loginWithPassword(userName, password)
        .pipe(map((resp : any) => this.processLoginResponse(resp, rememberMe)));
    }

    logout(): void {
      this.localStorage.deleteData(DBkeys.ACCESS_TOKEN);
      this.localStorage.deleteData(DBkeys.REFRESH_TOKEN);
      this.localStorage.deleteData(DBkeys.TOKEN_EXPIRES_IN);
      this.localStorage.deleteData(DBkeys.USER_PERMISSIONS);
      this.localStorage.deleteData(DBkeys.CURRENT_USER);

      this.configurations.clearLocalChanges();

      this.reevaluateLoginStatus();
    }

    get accessToken(): string {
      return this.oidcHelperService.accessToken;
    }

    redirectForLogin() {
      this.loginRedirectUrl = this.router.url;
      this.router.navigate([this.loginUrl]);
    }


    private saveUserDetails(user: User, permissions: PermissionValues[], accessToken: string, refreshToken: string, expiresIn: Date, rememberMe: boolean) {
      if (rememberMe) {
        this.localStorage.savePermanentData(accessToken, DBkeys.ACCESS_TOKEN);
        this.localStorage.savePermanentData(refreshToken, DBkeys.REFRESH_TOKEN);
        this.localStorage.savePermanentData(expiresIn, DBkeys.TOKEN_EXPIRES_IN);
        this.localStorage.savePermanentData(permissions, DBkeys.USER_PERMISSIONS);
        this.localStorage.savePermanentData(user, DBkeys.CURRENT_USER);
      } else {
        this.localStorage.saveSyncedSessionData(accessToken, DBkeys.ACCESS_TOKEN);
        this.localStorage.saveSyncedSessionData(refreshToken, DBkeys.REFRESH_TOKEN);
        this.localStorage.saveSyncedSessionData(expiresIn, DBkeys.TOKEN_EXPIRES_IN);
        this.localStorage.saveSyncedSessionData(permissions, DBkeys.USER_PERMISSIONS);
        this.localStorage.saveSyncedSessionData(user, DBkeys.CURRENT_USER);
      }

      this.localStorage.savePermanentData(rememberMe, DBkeys.REMEMBER_ME);
    }

    private processLoginResponse(response: LoginResponse, rememberMe?: boolean) {
      const accessToken = response.access_token;

      if (accessToken == null) {
        throw new Error('accessToken cannot be null');
      }

      rememberMe = rememberMe || this.rememberMe;

      const refreshToken = response.refresh_token || this.refreshToken;
      const expiresIn = response.expires_in;
      const tokenExpiryDate = new Date();
      tokenExpiryDate.setSeconds(tokenExpiryDate.getSeconds() + expiresIn);
      const accessTokenExpiry = tokenExpiryDate;
      const jwtHelper = new JwtHelper();
      const decodedAccessToken = jwtHelper.decodeToken(accessToken) as AccessToken;

      const permissions: PermissionValues[] = Array.isArray(decodedAccessToken.permission) ? decodedAccessToken.permission : [decodedAccessToken.permission];

      if (!this.isLoggedIn) {
        this.configurations.import(decodedAccessToken.configuration);
      }
debugger;
      const user = new User(
        decodedAccessToken.sub,
        decodedAccessToken.name,
        decodedAccessToken.fullname,
        decodedAccessToken.email,
        "",
        decodedAccessToken.phone_number,
        Array.isArray(decodedAccessToken.role) ? decodedAccessToken.role : [decodedAccessToken.role]);
      user.isEnabled = true;

      this.saveUserDetails(user, permissions, accessToken, refreshToken, accessTokenExpiry, rememberMe);

      this.reevaluateLoginStatus(user);

      return user;
    }

    get refreshToken(): string {
      return this.oidcHelperService.refreshToken;
    }

    private initializeLoginStatus() {
        this.localStorage.getInitEvent().subscribe(() => {
          this.reevaluateLoginStatus();
        });
      }

      private reevaluateLoginStatus(currentUser?: User | null) {

        const user = currentUser || this.localStorage.getDataObject<User>(DBkeys.CURRENT_USER);
        const isLoggedIn = user != null;

        if (this.previousIsLoggedInCheck !== isLoggedIn) {
          setTimeout(() => {
            this.loginStatus.next(isLoggedIn);
          });
        }

        this.previousIsLoggedInCheck = isLoggedIn;
      }

      getLoginStatusEvent(): Observable<boolean> {
        return this.loginStatus.asObservable();
      }
}

export class User {
  // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
  constructor(id: string, userName: string, fullName: string, email: string, jobTitle: string, phoneNumber: string, roles: string[]) {

      this.id = id;
      this.userName = userName;
      this.fullName = fullName;
      this.email = email;
      this.jobTitle = jobTitle;
      this.phoneNumber = phoneNumber;
      this.roles = roles;
  }


  get friendlyName(): string {
      let name = this.fullName || this.userName;

      if (this.jobTitle) {
          name = this.jobTitle + ' ' + name;
      }

      return name;
  }


  public id: string ="";
  public userName: string="";
  public fullName: string="";
  public email: string="";
  public jobTitle: string="";
  public phoneNumber: string="";
  public isEnabled: boolean=false;
  public isLockedOut: boolean=false;
  public roles: string[] = [];
}
