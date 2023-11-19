import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { Login, VerificationDto } from 'src/app/models/Verification';
import { VerificationService } from 'src/app/services/verification.service';

@Component({
  selector: 'app-verification',
  templateUrl: './verification.component.html',
  styleUrls: ['./verification.component.css']
})
export class VerificationComponent implements OnInit{

  code: string;
  seconds = 35;
  @Input() InpuForm: Login;
  @Output() onClose: EventEmitter<boolean> = new EventEmitter<boolean>();

  Channel: string;


  constructor(private _VerificationService: VerificationService) {}


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
    console.log(this.InpuForm);
       
    this.formatPhoneNumber()
    this.StartTimer()
    this.Channel = channel;
    var result = await this._VerificationService.GetVerification(new VerificationDto(channel, this.InpuForm.Phone,this.InpuForm.NameOrg));
  }

  async checkCode() {
    var result = await this._VerificationService.CheckCode(new VerificationDto(this.Channel, this.InpuForm.Phone,this.InpuForm.NameOrg,this.code));
    result ? console.log("sucess") : console.log("erro");

  }

  formatPhoneNumber() {
    // Check if the input starts with '0'
    if (this.InpuForm.Phone.startsWith('0')) {
      // Remove the leading '0' and prepend '+972'
      this.InpuForm.Phone = '+972' + this.InpuForm.Phone.slice(1);
    }
  }

  close(){
    this.onClose.emit();
  }
  
}

