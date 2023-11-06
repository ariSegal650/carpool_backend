using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicService.Dto
{
    public class ErrorResponse
    {
        public ErrorResponse(bool sucsses, string errorText)
        {
            this.sucsses = sucsses;
            this.errorText = errorText;
        }

        public bool sucsses { get; set; }
        public string errorText { get; set; } = string.Empty;
    }
}
