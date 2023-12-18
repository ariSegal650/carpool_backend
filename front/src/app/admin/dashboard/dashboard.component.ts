import { Component, OnInit, ViewChild } from '@angular/core';
import { DataService } from '../services/data.service';
import { RequestAdmin } from 'src/app/admin/models/request';
import { MessageServiceClient } from 'src/app/services/message-service-client.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  tasks: RequestAdmin[] = []
  showRequstComponent: boolean = false;
  taskEdit: RequestAdmin = null;
  @ViewChild('addresstext') addresstext: any;

  constructor(private _dataservice: DataService,
    private _messegeService: MessageServiceClient) { }

  ngOnInit(): void {
    this.ReloadTheRequests();
  }

  addTask() {
    this.taskEdit=null;
    this.showRequstComponent = !this.showRequstComponent;
  }

  deleteRequst(task: RequestAdmin) {

  }
  editRequst(task: RequestAdmin) {
    this.taskEdit = task;

    console.log(this.taskEdit.name);
    console.log(task);

    this.showRequstComponent = !this.showRequstComponent;
  }

  ReloadTheRequests() {
    this._messegeService.showLoading();
    this._dataservice.getAllRequsr().subscribe(
      res => {
        this._messegeService.hideLoading();
        this.tasks = res;
      },
      er => {
        this._messegeService.hideLoading();
        this._messegeService.showError("something went wrong");
        console.log(er);
      }
    );
    console.log(this.tasks);

  }


}
