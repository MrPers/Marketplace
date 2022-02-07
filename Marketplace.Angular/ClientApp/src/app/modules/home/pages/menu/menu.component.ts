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
        this.menuProducts = data;
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
