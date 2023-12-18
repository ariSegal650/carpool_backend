


export class OrganizationInfoDto {
  public id?:string;
  public name: string;
  public logo?: string;
  public phone?: string;
  public email?: string;
  public website?: string;

  public admin: OrganizationUser | null;

  constructor(
    Name: string,
    Logo: string,
    Phone: string,
    Email: string,
    Website: string,
    Users: OrganizationUser
  ) {
    this.name = Name;
    this.logo = Logo;
    this.phone = Phone;
    this.email = Email;
    this.website = Website;
    this.admin = Users;
  }
}

export class OrganizationUser {
  public name: string = '';
  public phone: string = '';
  public email: string = '';
  public levelAdmin?: number = 0;

  constructor(name: string, phone: string, email: string) {
    this.name = name;
    this.phone = phone;
    this.email = email;
  }
}
