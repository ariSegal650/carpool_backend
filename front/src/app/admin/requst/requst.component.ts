import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Place, RequestAdmin } from '../models/request';
import { DataService } from '../services/data.service';
import { } from "googlemaps";

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

  @ViewChild('origin') origin: any;
  @ViewChild('destination') destination: any;
  originResult: Place;
  destinationResult: Place;

  DateStart: Date = new Date();
  DateEnd: Date = new Date();

  requestForm: FormGroup;
  genders: dropDown[] | undefined;
  typeTask: dropDown[] = [];
  CarSize: dropDown[] = [];

  @Input() running_over: boolean = false;
  @Output() onClose: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(private dataService: DataService) { }

  ngOnInit(): void {

    this.requestForm = new FormGroup({
      Name: new FormControl('', Validators.required),
      Phone: new FormControl(''),
      Type: new FormControl(null, Validators.required),
      Count: new FormControl(0, Validators.required),
      CarSize: new FormControl(''),
      Origin: new FormControl('', Validators.required),
      Destination: new FormControl('', Validators.required),
      Date: new FormControl(new Date(), [Validators.required]),
      DateEnd: new FormControl(new Date() ,Validators.required),
      Phone_org: new FormControl(''),
      Notes: new FormControl('')
    });

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

  }

  onSubmit() {

    console.log(this.requestForm.value);
    //if (!this.requestForm.valid) return;

    const request: RequestAdmin = {
      Name: this.requestForm.value.Name,
      Phone: this.requestForm.value.Phone,
      Type: this.requestForm.value.Type.name,
      Count: this.requestForm.value.Count,
      CarSize: this.requestForm.value.CarSize.name,
      Origin: this.originResult,
       Destination: this.destinationResult,
      Date: this.requestForm.value.Date,
      DateEnd: this.requestForm.value.DateEnd,
      Phone_org: this.requestForm.value.Phone_org,
      Executed_Time: this.requestForm.value.Executed_Time,
      Notes: this.requestForm.value.Notes
    };
    console.log(request);

    this.dataService.addReqqust(request).subscribe(
      res => console.log(res),
      ero => console.log(ero),
    )
  }

  close() {
    this.onClose.emit();
  }

  ngAfterViewInit() {
   // console.log('nativeElement:', this.yourTemplateVariable.nativeElement); // Check if it's defined
   console.log(859);
    this.getPlaceAutocomplete(this.destination,"destination" );
    this.getPlaceAutocomplete(this.origin,"origin" );
   
  }


  parseTime(StartOrEnd: string, event: Date) {
    if (StartOrEnd == "start") {

      var date1: Date;
      date1 = this.requestForm.get('Date').value;
      date1.setHours(event.getHours())
      date1.setMinutes(event.getMinutes())
      this.requestForm.get('Date').setValue(date1);
      console.log(this.requestForm.get('Date').value);
    }
    else {
      var date1: Date;
      date1 = this.requestForm.get('DateEnd').value;
      date1.setHours(event.getHours())
      date1.setMinutes(event.getMinutes())
      this.requestForm.get('DateEnd').setValue(date1);
      console.log(this.requestForm.get('DateEnd').value);
    }
  }


  private getPlaceAutocomplete(htmlElemnt, direction:string) {

    const autocomplete = new google.maps.places.Autocomplete(htmlElemnt.nativeElement, {
      componentRestrictions: { country: 'il' },
      types: ['geocode','establishment'],
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

      console.log('Street Name:', streetName);
      console.log('Street Number:', streetNumber);
      console.log('Building Name:', buildingName);
      console.log('City:', city);
      console.log('Latitude:', latitude);
      console.log('Longitude:', longitude);
      //this.requestForm.get('Origin').setValue(streetName + streetNumber + buildingName + city)
     

      if(direction=="origin"){
        this.originResult = new Place("", city, latitude.toString(), longitude.toString());
        this.originResult.Name = streetName + streetNumber + buildingName + city
      }
      else{
        this.destinationResult = new Place("", city, latitude.toString(), longitude.toString());
        this.destinationResult.Name = streetName + streetNumber + buildingName + city
      }
    });
  };



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