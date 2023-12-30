namespace LogicService.Dto
{
    public class OrganizationDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Logo { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public OrganizationAdminDto? admin { get; set; }

    }



}
