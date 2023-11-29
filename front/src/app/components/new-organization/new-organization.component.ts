import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { FileSelectEvent } from 'primeng/fileupload';
import { Login } from 'src/app/models/Verification';
import { OrganizationInfoDto, OrganizationUser } from 'src/app/models/organization';
import { DataService } from 'src/app/services/data.service';
import { MessageServiceClient } from 'src/app/services/message-service-client.service';


@Component({
  selector: 'app-new-organization',
  templateUrl: './new-organization.component.html',
  styleUrls: ['./new-organization.component.css'],
})
export class NewOrganizationComponent implements OnInit {

  organizationForm: FormGroup;
  logoFile: File;
  imageToShow: any;
  FormFiled = false;
  @ViewChild('fileUpload') fileUpload: any;
  VerificationForm: Login;

  constructor(private data: DataService, private _messegeService: MessageServiceClient) { }

  ngOnInit(): void {

    //initializing form 
    this.organizationForm = new FormGroup({
      Name: new FormControl('', Validators.required),
      Logo: new FormControl(''),
      Phone: new FormControl('', [this.israeliPhoneValidator]),
      Email: new FormControl('', Validators.email),
      Website: new FormControl(''),
      Users: new FormGroup({
        Name: new FormControl('', Validators.required),
        Phone: new FormControl('', [Validators.required]),
        Email: new FormControl('', Validators.email),
      })
    });
  }

  //presents the image on html and save it as string
  onUploadImage(eventImage: FileSelectEvent) {

    this.logoFile = eventImage.files[eventImage.files.length - 1];
    if (this.logoFile) {
      const reader = new FileReader();
      reader.readAsDataURL(this.logoFile);

      reader.onload = (event) => {
        this.organizationForm.get('Logo')?.setValue(reader.result.toString());

        this.imageToShow = event.target?.result;
      };
    }
    this.fileUpload.clear();
  }

  onSubmit() {
    if (this.organizationForm.valid) {
      // Handle the form submission here
      console.log(this.organizationForm.value);
      const usersControl = this.organizationForm.get('Users');

      const user = new OrganizationUser(
        usersControl.get('Name')?.value || '',
        usersControl.get('Phone')?.value || '',
        usersControl.get('Email')?.value || '',
      );

      const organization: OrganizationInfoDto = new OrganizationInfoDto(
        this.organizationForm.value.Name,
        this.organizationForm.value.Logo,
        this.organizationForm.value.Phone,
        this.organizationForm.value.Email,
        this.organizationForm.value.Website,
        user
      );

      this.VerificationForm = new Login(this.organizationForm.value.Name, this.organizationForm.get('Users').get('Phone').value)
      this._messegeService.showLoading();

      this.data.CreateNewOrganization(organization).subscribe({
        next: (value) => {
          this._messegeService.hideLoading();
          this.FormFiled = true
        },
        error: (err) => {
          this._messegeService.hideLoading();

          this._messegeService.showError(err.error?.errorText ? err.error?.errorText : "שגיאה בלתי צפויה קרתה נא לנסות שנית בעוד מספר דקות ")
          console.log(err);
        },
      })

    }
    else {
      this._messegeService.showError("אנא מלא את כל הפרמטרים שמסומנים עם כוכבית");

    }
  }

  handleClose() {
    this.FormFiled = false;
  }
  israeliPhoneValidator(control: AbstractControl): ValidationErrors | null {
    const isValid = /^(?:(?:(\+?972|\(\+?972\)|\+?\(972\))(?:\s|\.|-)?([1-9]\d?))|(0[23489]{1})|(0[57]{1}[0-9]))(?:\s|\.|-)?([^0\D]{1}\d{2}(?:\s|\.|-)?\d{4})$/.test(control.value);

    return isValid || control.value == "" ? null : { israeliPhone: true };
  }

}
