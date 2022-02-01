import { ErrorComponent } from './pages/error/error.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./modules/home/home.module').then((el)=>el.HomeModule)
  },
  {
    path: 'auth',
    loadChildren: () => import('./modules/auth/auth.module').then((el)=>el.AuthModule)
  },
  { path: '**', component: ErrorComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  bootstrap:    [ AppComponent ]
})
export class AppRoutingModule { }
