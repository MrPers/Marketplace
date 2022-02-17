import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CurrencyService } from '../../services/currency.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(
    private currencyService: CurrencyService,
    private router: Router,
    private authService: AuthService,
    ) { }

  ngOnInit(): void {
  }

  logout(){
    this.authService.logout();
    this.currencyService.logout()
    .subscribe(
     (data) =>{
      //  this.router.navigate(['']);
     },
     (error) => {
       // debugger;
     });
  }

  // shop(){
  //   routerLink="/shop/:Id"
  //   // this.authService.startAuthentication();
  // }

}
