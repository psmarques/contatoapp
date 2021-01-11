import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { contatoService } from '../services/contato.service';
import { ErrorHandlerService } from '../services/errorHandler.service';
import { ApplicationInsights } from '@microsoft/applicationinsights-web';
import { appInsightsLogService } from '../services/logging.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
    ])
  ],
  providers: [contatoService, appInsightsLogService,
    {
      provide: ErrorHandler, useClass: ErrorHandlerService
    }],

  bootstrap: [AppComponent]
})
export class AppModule { }
