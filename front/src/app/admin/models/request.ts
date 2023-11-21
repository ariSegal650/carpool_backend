export class RequestAdmin {
    Id?: string;
    Name?: string;
    Phone?: string;
    Type?: string;
    Count: number=0;
    CarSize?: string;
    Origin?: string;
    Destination?: string;
    Date?: Date=null;
    DateEnd?: Date;
    Phone_org?: string;
    Admin_id?: string;
    Admin_Phone?: string;
    Executed?: boolean;
    Id_User?: string;
    Executed_Time?: Date;
    Notes?: string;
}
export class OrganizationAdmin {
    UserId: number = 1;
    Name: string = '';
    Phone: string = '';
    Email: string = '';
}