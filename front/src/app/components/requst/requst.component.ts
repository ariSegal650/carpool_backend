import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RequestOrg } from 'src/app/models/request';
import { DataService } from 'src/app/services/data.service';

interface Ganders {
  name: string;
  code: number;
}

@Component({
  selector: 'app-requst',
  templateUrl: './requst.component.html',
  styleUrls: ['./requst.component.css']
})

export class RequstComponent implements OnInit {

  requestForm: FormGroup;
  genders: Ganders[] | undefined;
  constructor(private data:DataService) {

    this.requestForm = new FormGroup({
      Id: new FormControl(),
      Id_Org: new FormControl(''),
      Name: new FormControl('', Validators.required),
      Phone: new FormControl(''),
      Gender: new FormControl('', Validators.required),
      Type: new FormControl(''),
      Count: new FormControl('', Validators.required),
      CarSize: new FormControl(''),
      Origin: new FormControl('', Validators.required),
      Destination: new FormControl('', Validators.required),
      Date: new FormControl('', Validators.required),
      DateEnd: new FormControl('', Validators.required),
      Phone_org: new FormControl(''),
      Id_Org_Admin: new FormControl(''),
      Executed: new FormControl(false, Validators.required),
      Id_User: new FormControl(''),
      Executed_Time: new FormControl(''),
      Notes: new FormControl('')
    });
  }

  ngOnInit(): void {
    this.genders = [
      { name: 'זכר', code: 1 },
      { name: 'נקבה', code:2 },
      { name: 'אחר', code: 3 },
    ]
  }
  onBasicUploadAuto(e:any){
    console.log("55");
    
  }
  onSubmit() {
    const request: RequestOrg = {
      Id_Org: this.requestForm.value.Id_Org,
      Name: this.requestForm.value.Name,
      Phone: this.requestForm.value.Phone,
      Gender: this.requestForm.value.Gender.code,
      Type: this.requestForm.value.Type,
      Count: this.requestForm.value.Count,
      CarSize: this.requestForm.value.CarSize,
      Origin: this.requestForm.value.Origin,
      Destination: this.requestForm.value.Destination,
      Date: this.requestForm.value.Date,
      DateEnd: this.requestForm.value.DateEnd,
      Phone_org: this.requestForm.value.Phone_org,
      Id_Org_Admin: this.requestForm.value.Id_Org_Admin,
      Executed: this.requestForm.value.Executed,
      Id_User: this.requestForm.value.Id_User,
      Executed_Time: this.requestForm.value.Executed_Time,
      Notes: this.requestForm.value.Notes
    };
    this.data.SendRequest(request);
  }
}
