using LogicService.Dto;
using Microsoft.Extensions.Configuration;
using Twilio;

namespace LogicService.Services
{
    public class VerificationService
    {

        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;
        private readonly OrganizationService _OrganizationService;
        private readonly UserService _UserService;
        public VerificationService(IConfiguration iconfiguration,
         TokenService tokenService, OrganizationService organizationService,
         UserService UserService)
        {
            _configuration = iconfiguration;
            TwilioClient.Init(_configuration["accountSid"], _configuration["authToken"]);
            _tokenService = tokenService;
            _OrganizationService = organizationService;
            _UserService = UserService;
        }

        public SimpelResponse GetVerification(VerificationRequstDto requst)
        {
            try
            {
                // requst.Phone = System.Text.RegularExpressions.Regex.Replace(requst.Phone, @"\s+", " ");
                // var verification = VerificationResource.Create(
                //     pathServiceSid: _configuration["_pathServiceSid"],
                //     to: requst.Phone,
                //     channel: requst.Channel,
                //     locale: "he"
                // );

                // if (verification.Status != "canceled")
                //     return new(true, verification.DateCreated.ToString());
                return new(true, "");
            }
            catch (Exception)
            {

                return new(false, "somthing went worng");
            }

        }

        public async Task<OrgResponseDto> ChecCode(VerificationRequstDto requst)
        {
            try
            {
                // requst.Phone = System.Text.RegularExpressions.Regex.Replace(requst.Phone, @"\s+", " ");

                // var verificationCheck = VerificationCheckResource.Create(
                // to: requst.Phone,
                // code: requst.Code,
                // pathServiceSid: _configuration["_pathServiceSid"]
                //  );


                if (requst.Code == "1234")
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

        public async Task<OrgResponseDto> ChecCodeUser(VerificationUserDto data)
        {
            var requst = data.requstData;
            var userinfo = data.user;
            try
            {
                // requst.Phone = System.Text.RegularExpressions.Regex.Replace(requst.Phone, @"\s+", " ");

                // var verificationCheck = VerificationCheckResource.Create(
                // to: requst.Phone,
                // code: requst.Code,
                // pathServiceSid: _configuration["_pathServiceSid"]);

                if (requst.Code == "1234")
                {
                    if (userinfo != null)
                    {
                        return await _UserService.CreateUser(userinfo);
                    }

                    var Exist = await _UserService.CheckUserExist(requst.Phone);
                    if (!Exist)
                    {
                        return new OrgResponseDto(false, "הרשם תחילה");

                    }

                    var a = _tokenService.GenerateJwtTokenUser(requst.Phone, "user");
                    return new OrgResponseDto(true, "approved", requst.Phone, a);


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
