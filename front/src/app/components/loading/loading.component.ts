import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { MessageServiceClient } from 'src/app/services/message-service-client.service';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.css']
})
export class LoadingComponent {

  isLoading = false;
  private subscription: Subscription;

  constructor(private loadingService: MessageServiceClient) {
    this.subscription = this.loadingService.loading.subscribe((loading) => {
      this.isLoading = loading;
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
