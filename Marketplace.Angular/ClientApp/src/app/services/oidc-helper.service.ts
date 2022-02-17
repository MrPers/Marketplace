
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { from } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { OAuthService } from 'angular-oauth2-oidc';

import { LocalStoreManager } from './local-store-manager.service';
import { ConfigurationService } from './configuration.service';
import { DBkeys, LoginResponse } from './constants.service';

@Injectable()
export class OidcHelperService {

  private get baseUrl() { return this.configurations.baseUrl; }
  private clientId = 'client_angular_id';
  // private scope = 'openid email phone profile offline_access roles Order';
  // private scope = 'openid profile Order';
  private scope = 'openid profile';

  constructor(
    private http: HttpClient,
    private oauthService: OAuthService,
    private configurations: ConfigurationService,
    private localStorage: LocalStoreManager) {

}

  refreshLogin() {
      const header = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' });
      const params = new HttpParams()
          .append('refresh_token', this.refreshToken)
          .append('client_id', this.clientId)
          .append('grant_type', 'refresh_token');

      this.oauthService.issuer = this.baseUrl;

      return from(this.oauthService.loadDiscoveryDocument())
          .pipe(mergeMap(() => {
              return this.http.post<LoginResponse>(this.oauthService.tokenEndpoint  as string, params, { headers: header });
          }));
  }

  get refreshToken(): string {
      return this.localStorage.getData(DBkeys.REFRESH_TOKEN);
  }

  get accessToken(): string {
      return this.localStorage.getData(DBkeys.ACCESS_TOKEN);
  }

  loginWithPassword(userName: string, password: string) {

    const header = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' });
    const params = new HttpParams()
        .append('username', userName)
        .append('password', password)
        .append('client_id', this.clientId)
        .append('grant_type', 'password')//code id_token
        .append('scope', this.scope);
    this.oauthService.issuer = this.baseUrl;
    return from(this.oauthService.loadDiscoveryDocument())
      .pipe(mergeMap(() => {
            return this.http.post<LoginResponse>(this.oauthService.tokenEndpoint, params, { headers: header });
        }));
  }

  get accessTokenExpiryDate(): Date {
      return this.localStorage.getDataObject<Date>(DBkeys.TOKEN_EXPIRES_IN, true);
  }

  get isSessionExpired(): boolean {
      if (this.accessTokenExpiryDate == null) {
          return true;
      }

      return this.accessTokenExpiryDate.valueOf() <= new Date().valueOf();
  }

}
