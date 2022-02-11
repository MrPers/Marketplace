import { ActivatedRoute } from '@angular/router';
import { ConstantsService } from './../../../../services/constants.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {

  constructor(private constantsService: ConstantsService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    // const routeParams = this.route.snapshot.paramMap;
    // const shopId = String(routeParams.get('Id'));

    // this.currencyService.getShopProducts(shopId)
    //   .subscribe((data: any) =>
    //   {
    //     this.menuProducts = data;
    //   });

    // this.constantsService.shopProducts.then((t:any) =>
    // {
    //   for (let index = 0; index < this.constantsService.pageMenuProducts.length; index++) {
    //     if(this.constantsService.pageMenuProducts[index].productId == productIdFromRoute){
    //       this.product = this.constantsService.pageMenuProducts[index];
    //       break;
    //     }
    //   }
    // });



  }
}
