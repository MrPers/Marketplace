import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserManager, UserManagerSettings, User } from 'oidc-client';
import {getClientSettings } from './constants.service';

@Injectable({
  providedIn: 'root'
})

// getUser : возвращает обещание загрузить объект User для текущего аутентифицированного пользователя.
// removeUser : возвращает обещание удалить из любого хранилища текущего аутентифицированного пользователя.
// signinRedirect : возвращает обещание вызвать перенаправление текущего окна на конечную точку авторизации.
// signinRedirectCallback : возвращает обещание обработать ответ от конечной точки авторизации. Результатом обещания является аутентифицированный пользователь.
// signinSilent : возвращает обещание инициировать тихий запрос (через iframe) к конечной точке авторизации. Результатом обещания является аутентифицированный Пользователь.
// signinSilentCallback : возвращает обещание уведомить родительское окно об ответе от конечной точки авторизации.
// signoutRedirect : возвращает обещание вызвать перенаправление текущего окна на конечную точку конечного сеанса.
// signoutRedirectCallback : возвращает обещание обработать ответ от конечной точки конечного сеанса.

export class AuthService {

  private manager = new UserManager(getClientSettings());
  private user!: User;

  constructor() {
    this.manager.getUser().then(user => {
      this.user = user!;

      this.manager.events.addUserLoaded(user => {
          this.user = user;
      });

      this.subscribeevents();
    });
  }

  public refreshCallBack(): void {
    this.manager.signinSilentCallback()
      .catch(err => {
          console.log("err callback");
      });
  }

  public subscribeevents(): void {
    this.manager.events.addSilentRenewError(() => {
        // console.log("error SilentRenew");
    });

    this.manager.events.addAccessTokenExpiring(() => {
        // console.log("access token expiring");
    });

    this.manager.events.addAccessTokenExpired(() => {
        // console.log("access token expired");
    });
  }

  isLoggedIn(): boolean {
    return this.user != null && !this.user.expired;
  }

  getClaims(): any {
    return this.user.profile;
  }

  getAuthorizationHeaderValue(): HttpHeaders {
    let headers = new HttpHeaders({ 'Authorization': `${this.user.token_type} ${this.user.access_token}`});
    return headers;
  }

  //getUser(): Promise<void> {
  //  return this.manager.getUser().then((user) => {
  //    this.user = user!;
  //  });
  //}

  startAuthentication(): Promise<void> {
    return this.manager.signinRedirect();
  }

  signoutRedirect() {
    return this.manager.signoutRedirect();
  }

  completeAuthentication(): Promise<void> {
    return this.manager.signinRedirectCallback().then(user => {
      this.user = user;
    });
  }
}

