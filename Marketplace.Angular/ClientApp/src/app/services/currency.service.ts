import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from './auth.service';
// import { CSVRecord } from '../modules/admin/pages/add-data/add-data.component';
import { URLpath, User} from './constants.service';

@Injectable({
  providedIn: 'root'
})

export class CurrencyService {

  constructor(private http:HttpClient, private authService: AuthService ) {}

  getAllProductPriceShop(){
    return this.http.get(URLpath + 'get-all-product-price-shop');
  };

  getProductAll(){
    return this.http.get(URLpath + 'get-product-all');
  };

}
