import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  listTitles: string[] = [];

  ngOnInit(): void {
    this.listTitles=['שם','טלפון','Email','Website','Confirmed','Admins']
  }
}
