import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit{

  constructor(private _dataservice:DataService){}
  ngOnInit(): void {
    
    console.log(55);
    this._dataservice.getAllRequsr().subscribe(
      res=>{
        console.log(res);
        res
      },
      er=>{
        console.log(er);
        
      }
    )
  }
}
