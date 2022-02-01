import { ConstantsService, PageMenuProduct } from './../../../../services/constants.service';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CurrencyService } from '../../../../services/currency.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  menuProducts: PageMenuProduct[]=[];

  constructor(private constantsService: ConstantsService, private currencyService:CurrencyService){}

  ngOnInit() {
    this.constantsService.definitelyThereProducts.then((t:any) =>
    {
      this.menuProducts = this.constantsService.pageMenuProducts;
    });

  }

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
