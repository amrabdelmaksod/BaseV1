using Twilio.Rest.Api.V2010.Account;

namespace Hedaya.Application.Auth.Abstractions
{
    public interface ISMSService
    {
        MessageResource send(string mobileNumber, string Body);

    }
}
