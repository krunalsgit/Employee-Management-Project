using Employee351.Common;
using Employee351.Models;
using Employee351.Models.Models;
using Employee351.Services.Login;
using Employee351.Services.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Tokens.Jwt;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;

namespace RepositoryPattern.Controllers
{
    [ApiController]
    [Route("api/Sign")]
    public class SignController:ControllerBase
    {
        private IConfiguration _configuration;
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;
        private readonly AppSetting _appSettings;  //for jwt

        public SignController(ILoginService loginService, IRegisterService registerService,IOptions<AppSetting> applications, IConfiguration configuration) {
            _loginService = loginService;
            _registerService= registerService;
            _appSettings = applications.Value;    //for jwt
            _configuration = configuration;
        } 

        [HttpPost("Login")]
        public async Task<ApiPostResponse<LoginResponseModel>> Login(LoginModel loginData)
        {
            ApiPostResponse<LoginResponseModel> response = new ApiPostResponse<LoginResponseModel>();
            try
            {
                var result = await _loginService.Login(loginData);
                if (result!=0)
                {
                    string _jwtToken = JWT_Token.GenerateJSONWebToken(loginData.Email, _appSettings.JWT_Secret);  //for jwt
                    Random rnd = new Random();
                    var otp = rnd.Next(100000, 999999).ToString();
                    await SendEmail(
                       loginData.Email,
                       "Employee351", $"Please enter the OTP within 2 minutes to complete your login {otp}");

                    response.Data = new LoginResponseModel();
                    response.Data.JWT_Token = _jwtToken;  //for jwt
                    response.Data.Email = loginData.Email;
                    response.Data.Id = result;
                    response.Data.OTP = otp;
                    response.Success = true;
                    response.Message = "Success";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not Found";
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("Register")]
        public async Task<ApiPostResponse<int>> Register(RegisterModel registerData)
        {
            try
            {
                ApiPostResponse<int> response = new ApiPostResponse<int>();

                var result =await _registerService.Register(registerData);
                if (result ==1 )
                {
                    string _jwtToken = JWT_Token.GenerateJSONWebToken(registerData.Email, _appSettings.JWT_Secret);  //for jwt
                    var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(_jwtToken));
                    var callbackUrl = Url.ActionLink("VerifyUser","Sign", values: new { code, registerData.Email }, protocol: Request.Scheme);

                    await SendEmail(
                        registerData.Email,
                        "Employee351", $"Click verify to verify your email <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Verify Email!</a>");

                    response.Data = result;
                    response.Success = true;
                    response.Message = "Success";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Failure";
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }  
        }

        [HttpPost("VerifyUser")]
        public async Task<int> VerifyUser(string code,string email)
        {
            ApiPostResponse<int> response = new ApiPostResponse<int>();
            byte[] decodedBytes = WebEncoders.Base64UrlDecode(code);
            string decodedToken = Encoding.UTF8.GetString(decodedBytes);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = handler.ReadJwtToken(decodedToken);
            Console.WriteLine(decodedToken);
            string mail = jwtToken.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            // 
            if (mail == email)
            {
                var result = await _registerService.VerifyEmail(email);
                return result;
            }
            return 0;
        }


        [HttpPost("SendEmail")]
        public async Task SendEmail(string email, string subject, string body)
        {
            var emailSettings = _configuration.GetSection("MailSettings");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Shaligram Infotech", emailSettings["UserName"]));
            message.To.Add(new MailboxAddress(email, email));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["Port"]), SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(emailSettings["UserName"], emailSettings["Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
