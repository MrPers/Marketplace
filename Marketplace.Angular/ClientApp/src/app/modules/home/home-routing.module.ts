import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MenuComponent } from './pages/menu/menu.component';
import { HomeComponent } from './home.component';
import { ProductComponent } from './pages/product/product.component';
import { ShopComponent } from './pages/shop/shop.component';
import { CabinetComponent } from './pages/cabinet/cabinet.component';
import { CartComponent } from './pages/cart/cart.component';

const routes: Routes = [
  {path:'', component: HomeComponent},
  {path:'menu', component: MenuComponent},
  {path:'cart', component: CartComponent},
  {path:'product/:Id', component: ProductComponent},
  {path:'shop/:Id', component: ShopComponent},
  {path:'cabinet/:Id', component: CabinetComponent},
  // canActivate: [AuthGuardService]
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
