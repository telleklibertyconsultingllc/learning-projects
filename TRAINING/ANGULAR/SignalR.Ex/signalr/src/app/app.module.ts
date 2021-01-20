import { NgModule, APP_INITIALIZER } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CustomerComponent } from './components/customers.component';
import { SignalRService } from './services/signalr/signalr.service';

@NgModule({
  declarations: [
    AppComponent,
    CustomerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    StoreModule.forRoot({

    }),
    EffectsModule.forRoot([

    ])
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: (signalRSvc: SignalRService) => signalRSvc.setupConnection(),
      deps: [SignalRService],
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
