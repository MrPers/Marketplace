import { ActivatedRoute } from '@angular/router';
import { ConstantsService, Product, ProductGroup, Shop } from './../../../../services/constants.service';
import { Component, OnInit } from '@angular/core';
import { CurrencyService } from '../../../../services/currency.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {
  shop: Shop = new Shop();
  groups: ProductGroup[]=[];
  products: Product[]=[];
  productsAll: Product[]=[];
  filters: string[]=[];

  constructor(private constantsService: ConstantsService, private currencyService:CurrencyService, private route: ActivatedRoute){}

  ngOnInit() {
    const routeParams = this.route.snapshot.paramMap;
    const productIdFromRoute = String(routeParams.get('Id'));
    let timeId = atob(productIdFromRoute);
    this.currencyService.getProductByShopId(timeId)
      .subscribe((data: any) =>
      {
        this.shop = data['shopsResult'];
        this.products = data['productsResult'];
        let groups = data['productGroupResult'];
        for (let index = 0; index < groups.length; index++) {
          this.groups.push({
            id: groups[index].id,
            name: groups[index].name,
            isChecked: false
          });
        }
        this.productsAll = this.products;

          this.groups.filter(group => group.id == timeId)[0].isChecked = true;
          this.displayProductGroups(timeId);

      });

  }

  displayProductGroups(id:string) {
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
