import { Component, OnInit } from '@angular/core';
// import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-refresh',
  templateUrl: './refresh.component.html',
  styles: []
})
export class RefreshComponent {
  constructor(
    // private _authService:AuthService
    ) {
  }

  ngOnInit(): void {
    // this._authService.refreshCallBack();
  }

}
