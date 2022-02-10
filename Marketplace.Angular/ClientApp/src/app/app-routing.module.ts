import { ErrorComponent } from './pages/error/error.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AuthCallbackComponent } from './pages/auth-callback/auth-callback.component';
import { RefreshComponent } from './pages/refresh/refresh.component';

const routes: Routes = [
  {
    path: 'auth-callback', component: AuthCallbackComponent
  },
  {
    path: 'refresh', component: RefreshComponent
  },
  {
    path: 'auth',
    loadChildren: () => import('./modules/auth/auth.module').then((el)=>el.AuthModule)
  },
  {
    path: '',
    loadChildren: () => import('./modules/home/home.module').then((el)=>el.HomeModule)
  },
  { path: '**', component: ErrorComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
