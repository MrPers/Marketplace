import { ActivatedRoute } from '@angular/router';
import { ConstantsService } from './../../../../services/constants.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {

  // pageShopProduct: PageShopProduct = new PageShopProduct();
  // product: PageMenuProduct = new PageMenuProduct();
  // pageCommentProduct: PageCommentProduct[]=[];

  constructor(private constantsService: ConstantsService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const productIdFromRoute = String(routeParams.get('Id'));
    // this.constantsService.definitelyThereProducts.then((t:any) =>
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
