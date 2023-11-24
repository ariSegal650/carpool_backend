import { Component, OnInit, ViewChild } from '@angular/core';
import { DataService } from '../services/data.service';
import { RequestAdmin } from 'src/app/admin/models/request';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  tasks: RequestAdmin[] = []
  showRequstComponent: boolean = false;
  @ViewChild('addresstext') addresstext: any;

  constructor(private _dataservice: DataService) { }

  ngOnInit(): void {

    this._dataservice.getAllRequsr().subscribe(
      res => {
        this.tasks = res;
        console.log(res);

      },
      er => {
        console.log(er);
      }
    );
  }

  addTask() {
    this.showRequstComponent = !this.showRequstComponent;
  }

 
}
