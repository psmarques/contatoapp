import { Component } from '@angular/core';
import { appInsightsLogService } from '../services/logging.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  constructor(private myMonitoringService: appInsightsLogService) {

    myMonitoringService.logEvent("Test");

  }
}
