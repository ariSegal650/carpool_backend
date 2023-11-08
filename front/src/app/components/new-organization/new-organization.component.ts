import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { OrganizationInfoDto, OrganizationUser } from 'src/app/models/organization';
import { DataService } from 'src/app/services/data.service';


@Component({
  selector: 'app-new-organization',
  templateUrl: './new-organization.component.html',
  styleUrls: ['./new-organization.component.css']
})
export class NewOrganizationComponent implements OnInit {

  organizationForm: FormGroup;
  logoFile: File;
  imageToShow: any;

  constructor(private data: DataService, private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.organizationForm = new FormGroup({
      Name: new FormControl(''),
      Logo: new FormControl(''),
      Phone: new FormControl('', [this.israeliPhoneValidator]),
      Email: new FormControl('', Validators.email),
      Website: new FormControl(''),
      Users:new FormGroup({
        Name: new FormControl('',Validators.required),
        Phone: new FormControl('', [Validators.required, this.israeliPhoneValidator]),
        Email: new FormControl('', Validators.email),
      })
      
    });
  }

  //presents the image on html and save it as string
  onBasicUploadAuto(eventImage: any) {

    this.logoFile = eventImage.files[0];
    if (this.logoFile) {
      const reader = new FileReader();
      reader.readAsDataURL(this.logoFile);

      reader.onload = (event) => {
        this.organizationForm.get('Logo')?.setValue(reader.result.toString());
        console.log(this.organizationForm);

        this.imageToShow = event.target?.result;
      };

    }
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

      console.log(organization);
      this.data.CreateNewOrganization(organization);

    } else {
      console.log('Form is invalid. Please fill in all required fields.');
    }
  }

  israeliPhoneValidator(control: AbstractControl): ValidationErrors | null {
    const isValid = /^(?:(?:(\+?972|\(\+?972\)|\+?\(972\))(?:\s|\.|-)?([1-9]\d?))|(0[23489]{1})|(0[57]{1}[0-9]))(?:\s|\.|-)?([^0\D]{1}\d{2}(?:\s|\.|-)?\d{4})$/.test(control.value);

    return isValid ? null : { israeliPhone: true };
  }

}
