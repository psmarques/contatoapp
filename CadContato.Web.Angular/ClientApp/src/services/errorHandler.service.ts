import { ErrorHandler, Injectable } from "@angular/core";
import { appInsightsLogService } from "./logging.service";

@Injectable()
export class ErrorHandlerService extends ErrorHandler {

  constructor(private myMonitoringService: appInsightsLogService) {
    super();
  }

  handleError(error: Error) {
    this.myMonitoringService.logException(error); // Manually log exception
  }
}
