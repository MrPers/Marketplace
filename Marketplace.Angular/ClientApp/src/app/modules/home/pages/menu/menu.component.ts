import { ConstantsService, Product, ProductGroup} from './../../../../services/constants.service';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CurrencyService } from '../../../../services/currency.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  groups: ProductGroup[]=[];
  products: Product[]=[];
  productsAll: Product[]=[];
  filters: string[]=[];

  constructor(private constantsService: ConstantsService, private currencyService:CurrencyService, private route: ActivatedRoute){}

  ngOnInit() {
    let timeId ="";
    let id: string = ""
    this.route.queryParams
    // .filter(params => params.id)
    .subscribe(params => {
      if(params.id){
        id = params.id;
        timeId = atob(id);
      }
    }
  );
  debugger;

    this.currencyService.getProductAll()
      .subscribe((data: any) =>
      {
        debugger;
        this.products = data['productsResult'];
        // this.groups = data['productGroupResult'];
        let groups = data['productGroupResult'];
        for (let index = 0; index < groups.length; index++) {
         this.groups.push({
           id: groups[index].id,
           name: groups[index].name,
           isChecked: false
         });
        }
        this.productsAll = this.products;

        if(id !== ""){
          this.groups.filter(group => group.id == timeId)[0].isChecked = true;
          this.displayProductGroups(timeId);
        }

      });

  }

  displayProductGroups(id:string) {
    debugger;
    if (this.filters.indexOf(id) == -1) {
      this.filters.push(id);
    }
    else {
      this.filters.splice(this.filters.indexOf(id), 1);
    }

    if(this.filters.length == 0 || this.filters.length == this.groups.length){
      this.products = this.productsAll;
    }
    else{
      for (let index = 0; index < this.filters.length; index++) {
        this.products = this.productsAll.filter(menuProduct => menuProduct.productGroupId == this.filters[index]);
      }
    }
  }

}
