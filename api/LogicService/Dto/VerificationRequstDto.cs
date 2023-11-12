
namespace LogicService.Dto
{
    public class VerificationRequstDto
    {
        public string Channel { get; set; }=string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime? Time { get; set; }
        public string? Code { get; set; }

       
    }
    
}
