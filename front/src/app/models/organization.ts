


export class OrganizationInfoDto {
  public id?:string;
  public Name: string;
  public Logo?: string;
  public Phone?: string;
  public Email?: string;
  public Website?: string;

  public admin: OrganizationUser | null;

  constructor(
    Name: string,
    Logo: string,
    Phone: string,
    Email: string,
    Website: string,
    Users: OrganizationUser
  ) {
    this.Name = Name;
    this.Logo = Logo;
    this.Phone = Phone;
    this.Email = Email;
    this.Website = Website;
    this.admin = Users;
  }
}

export class OrganizationUser {
  public Name: string = '';
  public Phone: string = '';
  public Email: string = '';
  public LevelAdmin?: number = 0;

  constructor(name: string, phone: string, email: string) {
    this.Name = name;
    this.Phone = phone;
    this.Email = email;
  }
}
