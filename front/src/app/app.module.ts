import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RequstComponent } from './components/requst/requst.component';
import { ReactiveFormsModule } from '@angular/forms'; 
import { CardModule } from 'primeng/card';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { HttpClientModule } from '@angular/common/http';
import { NewOrganizationComponent } from './components/new-organization/new-organization.component';
import { FileUploadModule } from 'primeng/fileupload';

@NgModule({
  declarations: [
    AppComponent,
    RequstComponent,
    NewOrganizationComponent,
    
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    CardModule,
    DropdownModule,
    InputTextModule,
    HttpClientModule,
    FileUploadModule
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
