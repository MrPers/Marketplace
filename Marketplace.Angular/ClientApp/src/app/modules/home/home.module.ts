import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { Directive, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home-routing.module';
import { ScrollToModule } from '@nicky-lenaers/ngx-scroll-to';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MenuComponent } from './pages/menu/menu.component';
import { ConstantsService } from '../../services/constants.service';
import { CurrencyService } from '../../services/currency.service';
import { ProductComponent } from './pages/product/product.component';
import { ShopComponent } from './pages/shop/shop.component';
import { CabinetComponent } from './pages/cabinet/cabinet.component';
import { CartComponent } from './pages/cart/cart.component';
import { AddproductComponent } from '../auth/pages/addproduct/addproduct.component';

@NgModule({
  declarations: [
    HomeComponent,
    MenuComponent,
    ProductComponent,
    ShopComponent,
    CabinetComponent,
    CartComponent,
  ],
  imports: [
    TooltipModule,
    CommonModule,
    HomeRoutingModule,  // надо для Router локального(в этом модуле)
    ScrollToModule,      //add scrol
    CarouselModule,    //add carusel
    FormsModule,    //add [(ngModel)]

  ],
  providers: [
    CurrencyService,
    ConstantsService
  ],
  bootstrap: [
    HomeComponent,
  ]
})
export class HomeModule { }
