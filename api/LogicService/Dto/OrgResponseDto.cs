using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicService.Dto
{
    public class OrgResponseDto:SimpelResponse
    {
        public OrgResponseDto(bool sucsses, string? errorText) : base(sucsses, errorText)
        {   
        }

        public OrgResponseDto(bool sucsses, string? errorText, string id, string token) : base(sucsses, errorText)
        {
            Id = id ;
            Token = token;
        }


        public string Id { get; set; } = string.Empty;
        public string Token { get; set; }=string.Empty;



    }
}
