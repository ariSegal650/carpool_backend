export class RequestAdmin {
    Id?: string;
    Name?: string;
    Phone?: string;
    Type?: string;
    Count: number=0;
    CarSize?: string;
    Origin?: Place;
    Destination?: Place;
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

export class Place 
{
    Name:string="";
    City:string="";
    Lat:number=0;
    Lng:number=0;
    constructor(name:string,city:string,_lat:number,_long:number){
        this.Name=name;
        this.City=city;
        this.Lat=_lat;
        this.Lng=_long;
    }
}