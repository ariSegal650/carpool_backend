
namespace LogicService.Dto
{
    public class VerificationRequstDto
    {
        public string Channel { get; set; }=string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string NameOrg { get; set; } = string.Empty;
        public string? Code { get; set; }
        public DateTime? Time { get; set; }

    }
    
}
