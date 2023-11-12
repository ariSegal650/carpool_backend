export class VerificationDto{
    
      Channel:string
       Phone:string
      Time?:Date
      Code?:string
      constructor(channel:string,phone:string,time?:Date,code?:string){
            this.Channel=channel;
            this.Phone=phone;
            this.Time=time;
            this.Code=code;
      }
}