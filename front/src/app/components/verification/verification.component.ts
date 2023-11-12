import { Component, Input, OnInit } from '@angular/core';
import { VerificationDto } from 'src/app/models/Verification';
import { VerificationService } from 'src/app/services/verification.service';

@Component({
  selector: 'app-verification',
  templateUrl: './verification.component.html',
  styleUrls: ['./verification.component.css']
})
export class VerificationComponent implements OnInit {
  code: string;
  seconds = 35;
  @Input() InpuPhone: string;
  Channel: string;

  constructor(private _VerificationService: VerificationService) { }


  ngOnInit(): void {
    this.SendVerification('sms');
  }

  StartTimer() {
    const interval = setInterval(() => {
      if (this.seconds > 0) {
        this.seconds--;
      }
    }, 1000);
  }

  async SendVerification(channel: string) {
    this.formatPhoneNumber()
    this.StartTimer()
    this.Channel = channel;
    var result = await this._VerificationService.GetVerification(new VerificationDto(channel, this.InpuPhone));
    result ? console.log("sucess") : console.log("erro");

  }

  async checkCode() {
    var result = await this._VerificationService.CheckCode(new VerificationDto(this.Channel, this.InpuPhone, null, this.code));
    result ? console.log("sucess") : console.log("erro");
  }
  
   formatPhoneNumber() {
    // Check if the input starts with '0'
    if (this.InpuPhone.startsWith('0')) {
      // Remove the leading '0' and prepend '+972'
      this.InpuPhone= '+972' + this.InpuPhone.slice(1);
    }   
  }
}

