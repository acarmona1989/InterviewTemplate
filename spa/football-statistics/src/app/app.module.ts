import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { YellowcardListComponent } from './components/yellowcard-list/yellowcard-list.component';
import { YellowcardItemComponent } from './components/yellowcard-item/yellowcard-item.component';
import { DropdownDirective } from './components/shared/dropdown.directive';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    DropdownDirective,
    YellowcardListComponent,
    YellowcardItemComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
