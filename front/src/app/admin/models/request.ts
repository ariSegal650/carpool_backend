import { UserInfo } from "./userModel";

export class RequestAdmin {
    id?: string;
    name?: string;
    phone?: string;
    type?: string;
    count: number = 0;
    carSize?: string;
    origin?: Place;
    destination?: Place;
    date?: Date = null;
    dateEnd?: Date;
    phone_org?: string;
    executed?: boolean;
    executed_Time?: Date;
    status?: string;
    user?: UserInfo;
    notes?: string;
    lastModified?: Date;
}

export class OrganizationAdmin {
    Name: string = '';
    Phone: string = '';
    Email: string = '';
}

export class Place {
    name: string = "";
    city: string = "";
    lat: string = "";
    lng: string = "";
    constructor(name: string, city: string, _lat: string, _long: string) {
        this.name = name;
        this.city = city;
        this.lat = _lat;
        this.lng = _long;
    }
}