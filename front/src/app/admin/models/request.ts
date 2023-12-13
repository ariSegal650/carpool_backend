import { OrganizationInfoDto } from "src/app/models/organization";

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
    Executed?: boolean;
    Id_User?: string;
    Executed_Time?: Date;
    Notes?: string;
    // organization:OrganizationInfoDto;
}
export class OrganizationAdmin {
    Name: string = '';
    Phone: string = '';
    Email: string = '';
}

export class Place 
{
    Name:string="";
    City:string="";
    Lat:string="";
    Lng:string="";
    constructor(name:string,city:string,_lat:string,_long:string){
        this.Name=name;
        this.City=city;
        this.Lat=_lat;
        this.Lng=_long;
    }
}