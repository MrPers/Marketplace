import { Injectable } from '@angular/core';
import { UserManagerSettings } from "oidc-client";
import * as Oidc from 'oidc-client';
import { CurrencyService } from "./currency.service";

const Authority = "https://localhost:5001";//
//const Silent_redirect_uri = "http://localhost:4200/refresh";
//const Redirect_uri = 'http://localhost:4200/auth-callback';//
//const Post_logout_redirect_uri = 'http://localhost:4200/';//
const Silent_redirect_uri = "http://localhost:5001/refresh";
const Redirect_uri = 'http://localhost:5001/auth-callback';//
const Post_logout_redirect_uri = 'http://localhost:5001/';//
const Response_type = "code";//
const AutomaticSilentRenew = true;//
const LoadUserInfo = true;
const Scope = "openid profile";//
const Client_id = 'client_angular_id';//
export const URLpath = "https://localhost:5001/";

export function getClientSettings(): UserManagerSettings {
  return {
    userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }), //чтоб хронилась сесия localStore
    authority: Authority,
    silent_redirect_uri : Silent_redirect_uri,
    redirect_uri: Redirect_uri,
    post_logout_redirect_uri: Post_logout_redirect_uri,
    response_type: Response_type,
    automaticSilentRenew: AutomaticSilentRenew, //указывающий, должна ли быть автоматическая попытка обновить токен доступа до истечения срока его действия
    scope: Scope,
    client_id: Client_id,
    loadUserInfo: LoadUserInfo, // загрузкой дополнительных идентификационных данных, чтобы заполнить пользователя profile
    // mergeClaims: MergeClaims,
    // filterProtocolClaims: FilterProtocolClaims, //следует ли удалять утверждения протокола OIDC из profile
    // checkSessionInterval: 50000, //Интервал в мс для проверки сеанса пользователя
    // silentRequestTimeout: 50000, //количество миллисекунд ожидания возврата беззвучного
  };

}

@Injectable({
  providedIn: 'root'
})
export class ConstantsService {
  public pageCommentProduct: PageCommentProduct[]=[];
  public pageMenuProducts: PageMenuProduct[]=[];
  public products : Product []=[];
  public shops : Shop []=[];
  public prices : Price []=[];
  public productGroups : ProductGroup []=[];
  public commentProducts : CommentProduct []=[];
  public users : User []=[];

  constructor(private currencyService:CurrencyService) {
  }

  definitelyThereProducts: Promise<void>= new Promise(resolve =>
  {
    if(this.products.length == 0){
      this.currencyService.getAllProductPriceShop()
      .subscribe((data: any) =>
      {
        this.products =data["productsResult"];
        this.shops =data["shopsResult"];
        this.prices =data["pricesResult"];
        this.productGroups =data["productGroupsResult"];
        this.commentProducts =data["commentProductsResult"];
        this.users =data["usersResult"];

        for (let index = 0; index < this.products.length; index++) {
          var price:number = 0;
          var prices:number[] = [];
          var pricesBoolean:boolean = false;

          for (let i = 0; i < this.prices .length; i++) {
            if(this.products [index].id ==this.prices [i].productId){
              var sum:number = 0;
              prices.push(this.prices [i].netPrice);
              if(prices.length !== 1){
                for (let t = 0; t < prices.length; t++) {
                  sum = prices[t] + sum;
                }
                price = sum/prices.length;
                pricesBoolean = true;
              }
              else{
                price =prices[0];
              }
            }
          }

          this.pageMenuProducts.push({
            productId : this.products [index].id,
            price : price,
            pricesBoolean : pricesBoolean,
            productGroupID : this.products [index].productGroupID,
            productName : this.products [index].name,
            photo : "../../../../../assets/images/" + this.products [index].photo,
          });
        }

        for (let index = 0; index < this.commentProducts.length; index++) {
          var userName: string = "";

          for (let i = 0; i < this.users.length; i++) {
            if(this.commentProducts[index].userId == this.users[i].id){
              userName = this.users[i].userName;
            }
          }

          this.pageCommentProduct.push({
            productId: this.commentProducts[index].productId,
            commentId: this.commentProducts[index].id,
            departureDate: this.commentProducts[index].departureDate,
            userId: this.commentProducts[index].userId,
            userName: userName,
            text: this.commentProducts[index].text,
          });
        }
        // this.lineChartLabels = resArray.map((el:any)=>{ return ((el.data).split('T')[0]+ ' ' + (el.data).split('T')[1]); });

        resolve();
      });
    }
    else{
      resolve();
    }
  });

}
export class PageCommentProduct{
  productId:any;
  commentId:any;
  departureDate: string = "";
  userId:any;
  userName:string = "";
  text:string = "";
}
export class PageMenuProduct{
  productId:string = "";
  price:number = 0;
  pricesBoolean:boolean = true;
  productGroupID:number = 0;
  productName:string = "";
  photo:string = "";
}
export class ProductGroup {
  Id:number = 0;
  name:string = "";
}
export class CommentProduct {
  id:any;
  departureDate: string = "";
  productId:number = 0;
  userId:number = 0;
  text:string = "";
}
export class Price {
  id:string = "";
  netPrice:number = 0;
  numberProduct:number = 0;
  productId:string = "";
  shopId:number = 0;
}
export class Product {
  id:string = "";
  name:string = "";
  photo:string = "";
  productGroupID:number = 0;
}
export class Shop {
  id:number = 0;
  name:string = "";
}
export class User{
  id:any;
  userName:string = "";
  password:string = "";
  email:string = "";
}
