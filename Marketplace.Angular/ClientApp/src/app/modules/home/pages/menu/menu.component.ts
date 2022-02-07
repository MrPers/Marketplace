import { ConstantsService, Product} from './../../../../services/constants.service';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CurrencyService } from '../../../../services/currency.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  menuProducts: Product[]=[];

  constructor(private constantsService: ConstantsService, private currencyService:CurrencyService){}

  ngOnInit() {
    this.currencyService.getProductAll()
      .subscribe((data: any) =>
      {
        debugger;
        this.menuProducts = data;
      });
  }

     //     for (let i = 0; i < this.prices .length; i++) {
      //       if(this.products [index].id ==this.prices [i].productId){
      //         var sum:number = 0;
      //         prices.push(this.prices [i].netPrice);
      //         if(prices.length !== 1){
      //           for (let t = 0; t < prices.length; t++) {
      //             sum = prices[t] + sum;
      //           }
      //           price = sum/prices.length;
      //           pricesBoolean = true;
      //         }
      //         else{
      //           price =prices[0];
      //         }
      //       }
      //     }

      //     this.pageMenuProducts.push({
      //       productId : this.products [index].id,
      //       price : price,
      //       pricesBoolean : pricesBoolean,
      //       productGroupID : this.products [index].productGroupID,
      //       productName : this.products [index].name,
      //       photo : "../../../../../assets/images/" + this.products [index].photo,
      //     });
      //   }


  // .subscribe((result : any) => { //при редоктировании
  //   for (let item of result) {
  //     this.constantsService.users.push({
  //       id: item.id,
  //       name: item.name,
  //       surname: item.surname,
  //       email: item.email
  //     });
  //   };
  //   this.onDisplayUsers(0);
  // });
}
