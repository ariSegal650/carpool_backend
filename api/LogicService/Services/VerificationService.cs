

using LogicService.Dto;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace LogicService.Services
{
    public class VerificationService
    {

        private readonly IConfiguration _configuration;
        public VerificationService(IConfiguration iconfiguration)
        {
            _configuration = iconfiguration;
            TwilioClient.Init(_configuration["accountSid"], _configuration["authToken"]);
        }

        public SimpelResponse GetVerification(VerificationRequstDto requst)
        {
            try
            {
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
        public bool ChecCode(VerificationRequstDto requst)
        {
            var verificationCheck = VerificationCheckResource.Create(
                to: requst.Phone,
                code: requst.Code,
                pathServiceSid: _configuration["_pathServiceSid"]
            );

          return verificationCheck.Status == "approved" ? true : false;
        }
    }
}
