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
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { VerificationComponent } from './components/verification/verification.component';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';

@NgModule({
  declarations: [
    AppComponent,
    RequstComponent,
    NewOrganizationComponent,
    VerificationComponent,
    
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
    FileUploadModule,
    ToastModule,
    FormsModule,
    ButtonModule
    
  ],
  providers: [MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
