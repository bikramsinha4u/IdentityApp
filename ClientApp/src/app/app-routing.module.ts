import { NgModule, model } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './shared/components/errors/not-found/not-found.component';
import { AccountModule } from './account/account.module';

const routes: Routes = [
  {
    path: '', component: HomeComponent
  },
  {
    path: 'account', loadChildren: () => import('./account/account.module').then(module => module.AccountModule)
  },
  {
    path: 'not-found', component: NotFoundComponent
  },
  {
    path: '**', component: NotFoundComponent, pathMatch: "full"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
