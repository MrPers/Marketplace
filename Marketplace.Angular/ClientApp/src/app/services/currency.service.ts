import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { URLpath } from './constants.service';
// import { CSVRecord } from '../modules/admin/pages/add-data/add-data.component';

@Injectable({
  providedIn: 'root'
})

export class CurrencyService {

  constructor(private http:HttpClient) {}

  getProductById(id: string){
    return this.http.get(URLpath + 'get-product-by-id/' + id);
  };

  getProductAll(){
    return this.http.get(URLpath + 'get-product-all');
  };

}
