// import { AuthService } from '../../services/auth.service';
import { Component, OnInit } from '@angular/core';
// import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styles: []
})
export class AuthCallbackComponent implements OnInit {

  constructor(
    // private router: Router,
    // private authService: AuthService
    ) { }

  ngOnInit() {
    // debugger;
    // this.authService.completeAuthentication()
    // .then(() =>{
    //   this.router.navigate(['']);
    // });
  }

}
