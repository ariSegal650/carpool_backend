using LogicService.EO;

namespace LogicService.Dto
{
    public class OrganizationDto
    { 
        public string? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Logo { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public OrganizationAdminDto? admin { get; set; }


        // public OrganizationInfoEO convertToEo()
        // {
        //    var admins = new List<OrganizationAdmin>();

        //     if (this.admin != null)
        //     {
        //         admins.Add(this.admin);
        //     }
        //     var eo = new OrganizationInfoEO
        //     {
        //         Name = this.Name,
        //         Logo = this.Logo,
        //         Phone = this.Phone,
        //         Email = this.Email,
        //         Website = this.Website,
        //         Admins = admins,
        //     };
        //     return eo;
        // }
    }



}
