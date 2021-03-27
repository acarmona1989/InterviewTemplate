import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { YellowcardListComponent } from './yellowcard-list/yellowcard-list.component';
import { YellowcardItemComponent } from './yellowcard-list/yellowcard-item/yellowcard-item.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
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
