import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { URLpath, User} from './constants.service';
// import { CSVRecord } from '../modules/admin/pages/add-data/add-data.component';

@Injectable({
  providedIn: 'root'
})

export class CurrencyService {

  constructor(private http:HttpClient) {}

  getProductById(id: string){
    return this.http.get(URLpath + 'get-product-by-id/' + id);
  };

  getProductByShopId(id: string){
    return this.http.get(URLpath + 'get-product-by-shop-id/' + id);
  };

  getProductAll(){
    return this.http.get(URLpath + 'get-product-all');
  };

  authCallback(path:string){
    return this.http.get(URLpath + path);
  };

  addUser(user: User){
    return this.http.post(URLpath + "Account/register", user);
  };

  userAuthentication(user: User){
    return this.http.post(URLpath + "Account/authentication", user);
  };

}

