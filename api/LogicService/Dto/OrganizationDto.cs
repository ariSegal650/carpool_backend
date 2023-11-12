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
        public OrganizationUser? User { get; set; }
        public OrganizationInfoEO convertToEo()
        {
            var users = new List<OrganizationUser>();
            if (this.User != null)
            {
                 users.Add(this.User);
            }
            var eo = new OrganizationInfoEO
            {
                Name = this.Name,
                Logo = this.Logo,
                Phone = this.Phone,
                Email = this.Email,
                Website = this.Website,
                Users = users,
            };
            return eo;
        }
    }



}
