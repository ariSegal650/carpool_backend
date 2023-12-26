export class UserInfo {
    id?: string;
    name?: string;
    nickname?: string;
    phone: string = '';
    image?: string;
    car?: Car;
}

class Car {
    year?: number;
    typeCar?: string;
    seats?: string;
    trunkSize?: string;
    image?: string;
}
