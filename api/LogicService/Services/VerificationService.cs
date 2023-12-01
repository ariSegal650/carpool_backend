using LogicService.Dto;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace LogicService.Services
{
    public class VerificationService
    {

        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;
        private readonly OrganizationService _OrganizationService;

        public VerificationService(IConfiguration iconfiguration,
         TokenService tokenService, OrganizationService organizationService)
        {
            _configuration = iconfiguration;
            TwilioClient.Init(_configuration["accountSid"], _configuration["authToken"]);
            _tokenService = tokenService;
            _OrganizationService = organizationService;
        }

        public SimpelResponse GetVerification(VerificationRequstDto requst)
        {
            try
            {
                requst.Phone = System.Text.RegularExpressions.Regex.Replace(requst.Phone, @"\s+", " ");
                var verification = VerificationResource.Create(
                    pathServiceSid: _configuration["_pathServiceSid"],
                    to: requst.Phone,
                    channel: requst.Channel,
                    locale: "he"
                );

                if (verification.Status != "canceled")
                    return new(true, verification.DateCreated.ToString());
                return new(false, verification.DateCreated.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new(false, "somthing went worng");
            }

        }

        public async Task<OrgResponseDto> ChecCode(VerificationRequstDto requst)
        {
            try
            {
                requst.Phone = System.Text.RegularExpressions.Regex.Replace(requst.Phone, @"\s+", " ");

                var verificationCheck = VerificationCheckResource.Create(
                to: requst.Phone,
                code: requst.Code,
                pathServiceSid: _configuration["_pathServiceSid"]
                 );

                if (verificationCheck.Status == "approved")
                {

                    var organization = await _OrganizationService.GetOrganization(requst.Phone, requst.NameOrg);

                    if (organization != null)
                    {
                        var admin = await _OrganizationService.GetAdmin(organization, requst.Phone);

                        if (admin != null)
                        {
                            admin.Confirmed = true;
                          

                            var a = _tokenService.GenerateJwtToken(organization.Id, admin.Phone, admin.Role);
                            return new OrgResponseDto(true, "approved", requst.Phone, a);

                        }
                    }
                }
                else
                {
                    return new OrgResponseDto(false, "סיסמה לא נכונה");

                }
            }
            catch (Exception)
            {

            }
            return new OrgResponseDto(false, "קרתה שגיאה");

        }
    }
}
