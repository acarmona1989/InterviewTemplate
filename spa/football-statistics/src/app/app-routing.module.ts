import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { YellowcardListComponent } from './components/yellowcard-list/yellowcard-list.component';

const routes: Routes = [
  {path : 'statistics/yellowcard', component : YellowcardListComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
