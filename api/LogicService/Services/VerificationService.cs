

using LogicService.Dto;
using MongoDB.Bson;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace LogicService.Services
{
    public class VerificationService
    {

       
        public VerificationService()
        {
             TwilioClient.Init(accountSid, authToken);
        }
        public ErrorResponse GetSmsVerification()
        {
            try
            {
                var verification = VerificationResource.Create(
                    pathServiceSid: "VAe4fa8e151fabab6ad41c8b0b7d2f0a02",
                    to: "+18482971108",
                    channel: "sms",
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
        public void CheckSms(string code)
        {
            var verificationCheck = VerificationCheckResource.Create(
                to: "+18482971108",
                code: code,
                pathServiceSid: "VAe4fa8e151fabab6ad41c8b0b7d2f0a02"

            );
          
            Console.WriteLine(verificationCheck.Status);
        }
    }
}
