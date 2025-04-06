using VideoStreamingPlatform.Commons.DTOs.Requests.UserValues;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Responses.UserValues;
using VideoStreamingPlatform.Commons.DTOs.Responses;

public interface IUserValuesService
{
    CommonResponse CreateUserValues(CreateUserValuesRequest request);
    CommonResponse DeleteUserValues(CommonDeleteRequest request);
    GetUserValuesResponse GetUserValues(GetUserValuesRequest request);
    CommonResponse UpdateUserValues(UpdateUserValuesRequest request);

    LoginResponse LoginUser(LoginRequest request);

    CommonResponse SendVerificationCode(SendMailRequest request);
    VerifiedCodeResponse VerifyCode(VerifyCodeRequest request);
}
