import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { appInsightsLogService } from '../services/logging.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';
  versao = '1.0.1';
  package = require('../../package.json');

  constructor(private myMonitoringService: appInsightsLogService,) {

    this.versao = this.package.version;
    myMonitoringService.logEvent("App Startup");
  }

  ngOnInit() {
  }
}
