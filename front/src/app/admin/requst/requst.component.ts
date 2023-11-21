import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RequestAdmin } from '../models/request';
import { DataService } from '../services/data.service';

interface dropDown {
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
  genders: dropDown[] | undefined;
  typeTask:dropDown[]=[];
  CarSize:dropDown[]=[];
  @Input() running_over: boolean=false;
  @Output() onClose: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(private data:DataService) {}

  ngOnInit(): void {
    
    this.requestForm = new FormGroup({
      Name: new FormControl('', Validators.required),
      Phone: new FormControl(''),
      Type: new FormControl(null, Validators.required),
      Count: new FormControl(0, Validators.required),
      CarSize: new FormControl(''),
      Origin: new FormControl('', Validators.required),
      Destination: new FormControl('', Validators.required),
      Date: new FormControl( Validators.required),
      DateEnd: new FormControl( Validators.required),
      Phone_org: new FormControl(''),
      Notes: new FormControl('')
    });
    this.genders = [
      { name: 'זכר', code: 1 },
      { name: 'נקבה', code:2 },
      { name: 'אחר', code: 3 },
    ];
    this.typeTask=[
      { name: 'הסעה', code: 1 },
      { name: 'משלוח', code:2 },
      { name: 'אחר', code: 3 },
    ];
    this.CarSize=[
      { name: 'רכב קטן', code: 1 },
      { name: 'רכב משפחתי', code:2 },
      { name: 'רכב גדול (7+ מקומות)', code: 3 },
      { name: 'מסחרי', code: 4 },
      { name: 'משאית', code: 5 },

    ]
  }

  onSubmit() {
    
    if(!this.requestForm.valid) return;

    console.log(this.requestForm.value);
    
    const request: RequestAdmin = {
      Name: this.requestForm.value.Name,
      Phone: this.requestForm.value.Phone,
      Type: this.requestForm.value.Type.name,
      Count:this.requestForm.value.Count ,
      CarSize: this.requestForm.value.CarSize.name,
      Origin: this.requestForm.value.Origin,
      Destination: this.requestForm.value.Destination,
      Date: this.requestForm.value.Date,
      DateEnd: this.requestForm.value.DateEnd,
      Phone_org: this.requestForm.value.Phone_org,
      Executed_Time: this.requestForm.value.Executed_Time,
      Notes: this.requestForm.value.Notes
    };
    console.log(request);
    
    this.data.addReqqust(request).subscribe(
      res=>console.log(res),
      ero=>console.log(ero),
    )
  }
  close(){
    this.onClose.emit();
  }
  
}
