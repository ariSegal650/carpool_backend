export class Login {
      Phone: string;
      NameOrg: string;

      constructor(nameOrg: string, phone: string) {
            this.NameOrg = nameOrg;
            this.Phone = phone;
      }
}
export class VerificationDto extends Login {

      Channel: string;
      Code?: string;
      Time?: Date;

      constructor(channel: string, phone: string, nameOrg: string, code?: string, time?: Date,) {
            super(nameOrg, phone);
            this.Time = time;
            this.Code = code;
            this.Channel = channel;
      }
}

