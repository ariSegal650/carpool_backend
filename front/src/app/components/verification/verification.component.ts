import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { Login, VerificationDto } from 'src/app/models/Verification';
import { MessageServiceClient } from 'src/app/services/message-service-client.service';
import { VerificationService } from 'src/app/services/verification.service';

@Component({
  selector: 'app-verification',
  templateUrl: './verification.component.html',
  styleUrls: ['./verification.component.css']
})
export class VerificationComponent implements OnInit {

  code: string;
  seconds = 35;
  @Input() InpuForm: Login;
  @Output() onClose: EventEmitter<boolean> = new EventEmitter<boolean>();

  Channel: string;


  constructor(private _VerificationService: VerificationService,
    private router: Router,
    private _messegeService: MessageServiceClient) { }


  ngOnInit(): void {
    this.SendVerification('sms');
  }


  StartTimer() {
    setInterval(() => {
      if (this.seconds > 0) {
        this.seconds--;
      }
    }, 1000);
  }

  async SendVerification(channel: string) {
    console.log(this.InpuForm);

    this.formatPhoneNumber();
    this.StartTimer();
    this.seconds=35;
   
    this.Channel = channel;
     await this._VerificationService.GetVerification(new VerificationDto(channel, this.InpuForm.Phone, this.InpuForm.NameOrg));
  }

  async checkCode() {

    this._messegeService.showLoading();

    this._VerificationService.CheckCode(new VerificationDto(this.Channel, this.InpuForm.Phone, this.InpuForm.NameOrg, this.code)).subscribe(
      value => {
        this._messegeService.hideLoading();

        console.log(value);
        localStorage.setItem("token", value?.token);
        this.router.navigate(['/admin'])
      },
      err => {
        this._messegeService.hideLoading();
        this._messegeService.showError(err.error?.errorText);
      }
    );

  }

  formatPhoneNumber() {
    // Check if the input starts with '0'
    if (this.InpuForm.Phone.startsWith('0')) {
      // Remove the leading '0' and prepend '+972'
      this.InpuForm.Phone = '+972' + this.InpuForm.Phone.slice(1);
    }
  }

  close() {
    this.onClose.emit();
  }

}

