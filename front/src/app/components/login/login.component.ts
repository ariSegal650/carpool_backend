import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { VerificationDto } from 'src/app/models/Verification';
import { MessageServiceClient } from 'src/app/services/message-service-client.service';
import { VerificationService } from 'src/app/services/verification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  LoginForm: FormGroup
  Sended: boolean;

  constructor(
    private _verificationService: VerificationService,
    private _messegeService: MessageServiceClient,
    private router: Router) {

  }

  ngOnInit(): void {
    this.router.navigate(['admin'])
    this.LoginForm = new FormGroup({
      NameOrg: new FormControl('', Validators.required),
      Phone: new FormControl('', [Validators.required, Validators.minLength(8)]),
    })
  }

  onSubmit() {
    this.Sended = false;
    
    if (!this.LoginForm.valid) {
      this._messegeService.showError("חובה למלא את כל הפרטים");
      return;
    }

    this.Sended = true
  }

  handleClose() {
    this.Sended = false;
  }


}
