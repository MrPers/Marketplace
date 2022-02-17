import { Injectable } from '@angular/core';
import { CurrencyService } from "./currency.service";

@Injectable({
  providedIn: 'root'
})
export class ConstantsService {

  constructor(private currencyService:CurrencyService) {
  }

}

export class User{

  constructor(id?: string, userName?: string, email?: string, roles?: string[]) {
    this.id = id;
    this.userName = userName;
    this.email = email;
    this.roles = roles;
  }

  id:any;
  userName:string = "";
  password:string = "";
  email:string = "";
  roles:string[] = [];
}
export class Product{
  id:any;
  name:string = "";
  netPrice:number = 0;
  photo:string = "";
  productGroupId:any;
  pricesAverage:boolean = false;
}
export class PageProduct{
  description:string = "";
  id:any;
  name:string = "";
  photo:string = "";
  productGroupId:any;
  productGroupName:string = "";
}
export class PagePriceProduct{
  id:any;
  netPrice:number = 0;
  numberProduct:number = 0;
  shopName:string = "";
}
export class CommentProduct{
  id:any;
  productId:any;
  departureDate: string = "";
  userId:any;
  userName:string = "";
  text:string = "";
}
export class ProductGroup {
  id:any;
  name:string = "";
  isChecked: boolean = false;
}
export class Shop {
  id:number = 0;
  name:string = "";
}
export class DBkeys {

  public static readonly URLpath = "https://localhost:5001/";

  public static readonly CURRENT_USER = 'current_user';
  public static readonly USER_PERMISSIONS = 'user_permissions';
  public static readonly ACCESS_TOKEN = 'access_token';
  public static readonly REFRESH_TOKEN = 'refresh_token';
  public static readonly TOKEN_EXPIRES_IN = 'expires_in';

  public static readonly REMEMBER_ME = 'remember_me';

  public static readonly LANGUAGE = 'language';
  public static readonly HOME_URL = 'home_url';
  public static readonly THEME_ID = 'themeId';
  public static readonly SHOW_DASHBOARD_STATISTICS = 'show_dashboard_statistics';
  public static readonly SHOW_DASHBOARD_NOTIFICATIONS = 'show_dashboard_notifications';
  public static readonly SHOW_DASHBOARD_TODO = 'show_dashboard_todo';
  public static readonly SHOW_DASHBOARD_BANNER = 'show_dashboard_banner';
}
export interface AccessToken {
  nbf: number;
  exp: number;
  iss: string;
  aud: string | string[];
  client_id: string;
  sub: string;
  auth_time: number;
  idp: string;
  role: string | string[];
  permission: PermissionValues | PermissionValues[];
  name: string;
  email: string;
  phone_number: string;
  fullname: string;
  jobtitle: string;
  configuration: string;
  scope: string | string[];
  amr: string[];
}
export interface AppTheme {
    id: number;
    name: string;
    href: string;
    isDefault?: boolean;
    background: string;
    color: string;
    isDark?: boolean;
}
export interface LoginResponse {
    access_token: string;
    refresh_token: string;
    expires_in: number;
    token_type: string;
}
export type PermissionValues =
    'users.view' | 'users.manage' |
    'roles.view' | 'roles.manage' | 'roles.assign';

// export class PageMenuProduct{
//   productId:string = "";
//   price:number = 0;
//   pricesBoolean:boolean = true;
//   productGroupID:number = 0;
//   productName:string = "";
//   photo:string = "";
// }
// export class CommentProduct {
//   id:any;
//   departureDate: string = "";
//   productId:number = 0;
//   userId:number = 0;
//   text:string = "";
// }
// export class Price {
//   id:string = "";
//   netPrice:number = 0;
//   numberProduct:number = 0;
//   productId:string = "";
//   shopId:number = 0;
// }
// export class Product {
//   id:string = "";
//   name:string = "";
//   photo:string = "";
//   productGroupID:number = 0;
// }

// export class PageShopProduct{
//   shopId:any;
//   shopName:string = "";
//   listShopProduct:ListShopProduct []=[];
// }
// export class PageCabinet{
//   userId:any;
//   userName:string = "";
//   email:string = "";
//   //list coment
// }


      //   this.productGroups =data["productGroupsResult"];
      //   this.commentProducts =data["commentProductsResult"];
      //   this.users =data["usersResult"];

      //   for (let index = 0; index < this.products.length; index++) {
      //     var price:number = 0;
      //     var prices:number[] = [];
      //     var pricesBoolean:boolean = false;



      //   for (let index = 0; index < this.commentProducts.length; index++) {
      //     var userName: string = "";

      //     this.pageShopProducts.push({
      //       shopId: this.shops[index].id,
      //       shopName: this.shops[index].name,
      //       listShopProduct:listShopProducts,
      //     });
      //   }
