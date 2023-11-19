


export class OrganizationInfoDto {
  public Name: string;
  public Logo: string | null = null;
  public Phone: string | null = null;
  public Email: string | null = null;
  public Website: string | null = null;
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
