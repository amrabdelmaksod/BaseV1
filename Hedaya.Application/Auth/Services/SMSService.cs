using Hedaya.Application.Auth.Abstractions;
using Hedaya.Application.Auth.Models;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Hedaya.Application.Auth.Services
{
    public class SMSService : ISMSService
    {
        private readonly TwilioSettings _twilio;
        public SMSService(IOptions<TwilioSettings> twilio)
        {
            _twilio = twilio.Value;
        }
        public MessageResource send(string mobileNumber, string Body)
        {
            TwilioClient.Init(_twilio.AccountSID, _twilio.AuthToken);
            var result = MessageResource.Create(
                body: Body,
                from: new Twilio.Types.PhoneNumber(_twilio.TwilioPhoneNumber),
                to: mobileNumber);
            return result;
        }
    }
}
