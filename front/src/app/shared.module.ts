import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms'; 
import { CardModule } from 'primeng/card';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { HttpClientModule } from '@angular/common/http';
import { FileUploadModule } from 'primeng/fileupload';
import { ToastModule } from 'primeng/toast';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';

@NgModule({
  declarations: [   
  ],
  exports: [
    ReactiveFormsModule,
    CardModule,
    DropdownModule,
    InputTextModule,
    HttpClientModule,
    FileUploadModule,
    ToastModule,
    FormsModule,
    ButtonModule,
    
],
  providers: []
})
export class SharedModule {}