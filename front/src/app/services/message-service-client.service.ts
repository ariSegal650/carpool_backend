import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MessageServiceClient {

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading = this.loadingSubject.asObservable();
  
  constructor(private messageService: MessageService) { }

  showSuccess(alert: string) {
    this.messageService.add({ severity: 'success', summary: 'מוצלח', detail: alert });
  }

  showInfo(alert: string) {
    this.messageService.add({ severity: 'info', summary: 'מידע', detail: alert });
  }

  showWarn(alert: string) {
    this.messageService.add({ severity: 'warn', summary: 'אזהרה', detail: alert });
  }

  showError(alert: string) {
    this.messageService.add({ severity: 'error', summary: 'שגיאה', detail: alert });
  }

  showLoading() {
    this.loadingSubject.next(true);
  }

  hideLoading() {
    this.loadingSubject.next(false);
  }
  
}
