import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Place, RequestAdmin } from '../models/request';
import { DataService } from '../services/data.service';
import { } from "googlemaps";
import { MessageServiceClient } from 'src/app/services/message-service-client.service';

class dropDown {
  name: string;
  code: number;
}


@Component({
  selector: 'app-requst',
  templateUrl: './requst.component.html',
  styleUrls: ['./requst.component.css']
})

export class RequstComponent implements OnInit {

  @ViewChild('origin') origin: any;
  @ViewChild('destination') destination: any;
  originResult: Place;
  destinationResult: Place;

  SelectTypeT: string = "בחר סוג משימה";
  CarSizeT: string = "גודל רכב";
  destinationT = "הזנת מיקום";
  originT = "הזנת מיקום";

  requestForm: FormGroup;
  genders: dropDown[] = [];
  typeTask: dropDown[] = [];
  CarSize: dropDown[] = [];

  @Input() running_over: boolean = false;
  @Input() taskEdit: RequestAdmin;

  @Output() onClose: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() RequstSended: EventEmitter<boolean> = new EventEmitter<boolean>();


  constructor(private dataService: DataService,
    private _messegeService: MessageServiceClient) { }

  ngOnInit(): void {

    this.genders = [
      { name: 'זכר', code: 1 },
      { name: 'נקבה', code: 2 },
      { name: 'אחר', code: 3 },
    ];
    this.typeTask = [
      { name: 'הסעה', code: 1 },
      { name: 'משלוח', code: 2 },
      { name: 'אחר', code: 3 },
    ];
    this.CarSize = [
      { name: 'רכב קטן', code: 1 },
      { name: 'רכב משפחתי', code: 2 },
      { name: 'רכב גדול (7+ מקומות)', code: 3 },
      { name: 'מסחרי', code: 4 },
      { name: 'משאית', code: 5 },

    ];

    if (this.taskEdit != null) {
      this.initializeRequst();
      return;
    }
    const currentDatePlus24Hours = new Date();
    currentDatePlus24Hours.setHours(currentDatePlus24Hours.getHours() + 5);

    this.requestForm = new FormGroup({
      Name: new FormControl('', Validators.required),
      Phone: new FormControl('', [Validators.required, this.israeliPhoneValidator.bind(this)]),
      Type: new FormControl(null, Validators.required),
      Count: new FormControl(0, Validators.required),
      CarSize: new FormControl(''),
      Origin: new FormControl('', Validators.required),
      Destination: new FormControl('', Validators.required),
      Date: new FormControl(new Date(), [Validators.required]),
      DateHour: new FormControl(new Date(), [Validators.required]),
      DateEnd: new FormControl(currentDatePlus24Hours, Validators.required),
      DateEndHour: new FormControl(currentDatePlus24Hours, Validators.required),
      Phone_org: new FormControl(''),
      Notes: new FormControl('')
    });


  }

  initializeRequst() {
    this.requestForm = new FormGroup({
      Name: new FormControl(this.taskEdit.name, Validators.required),
      Phone: new FormControl(this.taskEdit.phone, [Validators.required, this.israeliPhoneValidator.bind(this)]),
      Type: new FormControl(Validators.required),
      Count: new FormControl(this.taskEdit.count, Validators.required),
      CarSize: new FormControl(),
      Origin: new FormControl(this.taskEdit.origin.name, Validators.required),
      Destination: new FormControl(this.taskEdit.destination.name, Validators.required),
      Date: new FormControl(this.getDate(this.taskEdit.date.toString()), [Validators.required]),
      DateHour: new FormControl(this.getDate(this.taskEdit.date.toString()), [Validators.required]),
      DateEnd: new FormControl(this.getDate(this.taskEdit.dateEnd.toString()), Validators.required),
      DateEndHour: new FormControl(this.getDate(this.taskEdit.dateEnd.toString()), Validators.required),
      Phone_org: new FormControl(this.taskEdit.phone_org),
      Notes: new FormControl(this.taskEdit.notes)
    });

    const selectedType = this.typeTask.find(type => type.name === this.taskEdit.type);
    this.requestForm.get("Type").setValue(selectedType);
    this.SelectTypeT = this.taskEdit.type;
    this.CarSizeT = this.taskEdit.carSize;

    if (this.taskEdit.carSize != null) {
      const selectedCarSize = this.CarSize.find(car => car.name === this.taskEdit.carSize);
      this.requestForm.get("CarSize").setValue(selectedCarSize);
      console.log(selectedCarSize);
    }

    this.originT = this.taskEdit.origin.name ?? "55";
    this.destinationT = this.taskEdit.destination.name;



  }

  onSubmit() {

    console.log(this.requestForm.value);
    if (!this.requestForm.valid) {
      this._messegeService.showError("חובה למלאות את כל הערכים המסומנים בכוכב");
      return;
    };

    const request: RequestAdmin = {
      id:this.taskEdit?.id,
      name: this.requestForm.value.Name,
      phone: this.requestForm.value.Phone,
      type: this.requestForm.value.Type.name,
      count: this.requestForm.value.Count,
      carSize: this.requestForm.value.CarSize?.name,
      origin: this.originResult ?? this.taskEdit.origin,
      destination: this.destinationResult ??this.taskEdit.destination,
      date: this.requestForm.value.Date,
      dateEnd: this.requestForm.value.DateEnd,
      phone_org: this.requestForm.value.Phone_org,
      executed_Time: this.requestForm.value.Executed_Time,
      notes: this.requestForm.value.Notes
    };
    this._messegeService.showLoading();

    this.dataService.addReqqust(request).subscribe(
      res => {
        this._messegeService.hideLoading();
        this.close();
        this._messegeService.showSuccess("הבקשה נוספה בהצלחה ");
        this.RequstSended.emit();
      },
      err => {
        this._messegeService.hideLoading();
        this.close();
        this._messegeService.showError(err.error?.errorText);

      }
    )
  }

  close() {
    this.onClose.emit();
  }

  ngAfterViewInit() {
    this.getPlaceAutocomplete(this.destination, "destination");
    this.getPlaceAutocomplete(this.origin, "origin");
  }

  getDate(dateString: string): Date {
    return new Date(dateString);
  }

  parseTime(StartOrEnd: string, event: any) {

    if (StartOrEnd == "start") {

      var date1: Date;
      date1 = this.requestForm.get('Date').value;
      date1.setHours(event.getHours())
      date1.setMinutes(event.getMinutes())
      this.requestForm.get('Date').setValue(date1);
    }
    else {
      var date1: Date;
      date1 = this.requestForm.get('DateEnd').value;
      date1.setHours(event.getHours())
      date1.setMinutes(event.getMinutes())
      this.requestForm.get('DateEnd').setValue(date1);
    }
  }


  private getPlaceAutocomplete(htmlElemnt, direction: string) {

    const autocomplete = new google.maps.places.Autocomplete(htmlElemnt.nativeElement, {
      componentRestrictions: { country: 'il' },
      types: ['geocode', 'establishment'],
      fields: ['name', 'geometry', 'address_components',]
    });

    google.maps.event.addListener(autocomplete, 'place_changed', () => {
      const place = autocomplete.getPlace();
      console.log(place);

      // Extracting address components
      const addressComponents = place.address_components;

      // Extracting the street number
      const streetNameComponent = addressComponents.find(component =>
        component.types.includes('route')
      );
      const streetName = streetNameComponent ? streetNameComponent.long_name + "," : '';

      // Extracting the street number
      const streetNumberComponent = addressComponents.find(component =>
        component.types.includes('street_number')
      );
      const streetNumber = streetNumberComponent ? streetNumberComponent.long_name + "," : '';

      // Extracting the building name
      const buildingComponent = addressComponents.find(component =>
        component.types.includes('premise')
      );
      const buildingName = buildingComponent ? buildingComponent.long_name + "," : '';

      // Extracting the city
      const cityComponent = addressComponents.find(component =>
        component.types.includes('locality')
      );
      const city = cityComponent ? cityComponent.long_name + "," : '';

      // Extracting latitude and longitude
      const latitude = place.geometry.location.lat();
      const longitude = place.geometry.location.lng();

      if (direction == "origin") {
        this.originResult = new Place("", city, latitude.toString(), longitude.toString());
        this.originResult.name = streetName + streetNumber + buildingName + city
      }
      else {
        this.destinationResult = new Place("", city, latitude.toString(), longitude.toString());
        this.destinationResult.name = streetName + streetNumber + buildingName + city
      }
    });
  };

  israeliPhoneValidator(control: AbstractControl): ValidationErrors | null {
    const isValid = /^(?:(?:(\+?972|\(\+?972\)|\+?\(972\))(?:\s|\.|-)?([1-9]\d?))|(0[23489]{1})|(0[57]{1}[0-9]))(?:\s|\.|-)?([^0\D]{1}\d{2}(?:\s|\.|-)?\d{4})$/.test(control.value);

    return isValid || control.value == "" ? null : { israeliPhone: true };
  }


}

// dateValidator(): ValidatorFn {
//   return (control: AbstractControl): ValidationErrors | null => {
//     if (control.value === null || control.value === undefined || control.value === '') {
//       // Handle empty or undefined values if needed
//     }

//     const inputDate = new Date(control.value);

//     // Check if it's a valid date
//     if (isNaN(inputDate.getTime())) {
//       return { invalidDate: true };
//     }


//     return null; 
//   };
// }