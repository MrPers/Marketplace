import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../../services/auth.service';
import {ConstantsService, User } from '../../../../services/constants.service';
import { CurrencyService } from '../../../../services/currency.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  error: boolean = false;
  returnUrl: string ="";
  userRegistration: User = new User();
  userAuthentication: User = new User();
  submitted: boolean = false;
  visibility: boolean = true;
  t = "";

  constructor(private constantsService: ConstantsService, private route: ActivatedRoute, private currencyService: CurrencyService, private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    this.returnUrl = String(routeParams.get('ReturnUrl')).split(':/')[1];
  }

  toggleOn(){
    this.visibility=true;
  }

  toggleOff(){
    this.visibility=false;
  }

  authenticationSubmit() {
    this.currencyService.userAuthentication(this.userAuthentication)
      .subscribe((data: any) =>
      {
        this.currencyService.authCallback(this.returnUrl)
        .subscribe(
          (data) =>{
            // debugger;
          },
          (error) => {
            // var y: string = error.url.split('https://localhost:5001/')[1];
            this.authService.completeAuthentication();
            // .then(() =>{
            // });
            this.router.navigate(['']);
          });
      });
  }

  registrationSubmit() {
    this.currencyService.addUser(this.userRegistration)
      .subscribe(
        result => {
          this.visibility = true;
        },
        error => {
          if(error['status'] == 400) {
            this.error = true;
          }
        });
  }

}
