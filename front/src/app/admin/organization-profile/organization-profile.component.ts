import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/data.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { OrganizationInfoDto, OrganizationUser } from 'src/app/models/organization';
import { FileSelectEvent } from 'primeng/fileupload';
import { MessageServiceClient } from 'src/app/services/message-service-client.service';

@Component({
  selector: 'app-organization-profile',
  templateUrl: './organization-profile.component.html',
  styleUrls: ['./organization-profile.component.css']
})
export class OrganizationProfileComponent implements OnInit {

  organization: any;
  imageToShow: any;
  logoFile: File;
  dataLoaded = false;
  constructor(private _dataService: DataService, private _messegeService: MessageServiceClient) { }

  ngOnInit(): void {
    var response: OrganizationInfoDto;
    this._dataService.getOrganization().subscribe({
      next: (value) => {
        console.log(value);

        response = value;
        console.log(response.name);

        this.organization = new FormGroup({
          Name: new FormControl(response.name, Validators.required),
          Logo: new FormControl(response.logo),
          Phone: new FormControl(response.phone,),
          Email: new FormControl(response.email, Validators.email),
          Website: new FormControl(response.website),
          Users: new FormGroup({
            Name: new FormControl(response.admin.name, Validators.required),
            Phone: new FormControl(response.admin.phone, [Validators.required]),
            Email: new FormControl(response.admin.email, Validators.email),
          })
        });
        this.imageToShow = this.organization.get('Logo').value;

        this.dataLoaded = true;
      },
      error(err) {

      },
    });


  }

  onSubmit() {
    console.log(this.organization.value);
    if (this.organization.valid) {
      // Handle the form submission here
      console.log(this.organization.value);
      const usersControl = this.organization.get('Users');

      const user = new OrganizationUser(
        usersControl.get('Name')?.value || '',
        usersControl.get('Phone')?.value || '',
        usersControl.get('Email')?.value || '',
      );

      const organization: OrganizationInfoDto = new OrganizationInfoDto(
        this.organization.value.Name,
        this.organization.value.Logo,
        this.organization.value.Phone,
        this.organization.value.Email,
        this.organization.value.Website,
        user
      );


      this._messegeService.showLoading();

      this._dataService.updateOrganization(organization).subscribe({
        next: (value) => {
          this._messegeService.hideLoading();
          this._messegeService.showSuccess("העדכון בוצעה בהצלחה")
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

  onUploadImage(eventImage: FileSelectEvent) {

    this.logoFile = eventImage.files[eventImage.files.length - 1];
    if (this.logoFile) {
      console.log(this.logoFile);

      const reader = new FileReader();
      reader.readAsDataURL(this.logoFile);

      reader.onload = (event) => {
        this.organization.get('Logo')?.setValue(reader.result.toString());
        console.log(this.organization.get('Logo').value);
        this.imageToShow = this.organization.get('Logo').value;
      };
    }
  }

}
