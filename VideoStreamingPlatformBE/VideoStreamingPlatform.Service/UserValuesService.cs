using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.UserValues;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.UserValues;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class UserValuesService : IUserValuesService
    {
        private readonly VideoStreamingPlatformContext _db;
        private readonly string _jwtSecretKey;
        private readonly IEmailService _emailService;
        private static readonly Dictionary<int, string> _verificationCodes = new();


        public UserValuesService(VideoStreamingPlatformContext dbContext, IEmailService emailService)
        {
            _db = dbContext;
            _jwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? throw new Exception("JWT Secret key not set.");
            _emailService = emailService;
        }

        public CommonResponse CreateUserValues(CreateUserValuesRequest request)
        {
            var userExists = _db.Users.FirstOrDefault(x => x.UserId == request.UserId);
            if (userExists == null) { throw new NullReferenceException("UserID provided in request does not exist."); }

            var userValueExists = _db.UserValues.FirstOrDefault(x => x.UserId == request.UserId);
            if (userValueExists != null) { throw new NullReferenceException("UserID provided in request already exists."); }

            var emailExists = _db.UserValues.FirstOrDefault(x => x.Email.Equals(request.Email));
            if (emailExists != null) { throw new NullReferenceException("Email provided in request already exists."); }

            if (!IsValidEmail(request.Email))
            {
                throw new NullReferenceException("Email provided in request has bad format.");
            }

            var newObject = new UserValue()
            {
                UserId = request.UserId,
                Email = request.Email,
                Password = "hidden",
                PasswordHash = HashPassword(request.Password)
            };

            var response = _db.UserValues.Add(newObject);
            _db.SaveChanges();
            return new CommonResponse() { Id = response.Entity.UserValuesId, Message = "UserValue successfully added." };
        }

        
        public CommonResponse DeleteUserValues(CommonDeleteRequest request)
        {
            var removeObject = _db.UserValues.FirstOrDefault(x => x.UserId == request.Id);
            if (removeObject != null)
            {
                _db.UserValues.Remove(removeObject);
                _db.SaveChanges();
                return new CommonResponse() { Id = request.Id, Message = "UserValue removed." };
            }
            throw new NullReferenceException("UserID provided in request does not exist.");
        }

        public GetUserValuesResponse GetUserValues(GetUserValuesRequest request)
        {
            var getUser = _db.UserValues.FirstOrDefault(x => x.UserId == request.UserId);
            var response = new GetUserValuesResponse();
            if (getUser != null)
            {
                if (VerifyPassword(request.Password, getUser.PasswordHash))
                {
                    response.UserId = getUser.UserId;
                    response.Email = getUser.Email;
                    response.Password = request.Password;
                }
                else { throw new InvalidOperationException("Provided password is incorrect."); }
            }
            else
            {
                throw new NullReferenceException("UserID provided in request does not exist.");
            }

            return response;
        }
                

        public CommonResponse UpdateUserValues(UpdateUserValuesRequest request)
        {
            var updateUser = _db.UserValues.FirstOrDefault(x => x.UserId == request.UserId);
            if (updateUser != null)
            {
                updateUser.Email = request.Email ?? updateUser.Email;
                updateUser.PasswordHash = !string.IsNullOrEmpty(request.Password) ? HashPassword(request.Password) : updateUser.PasswordHash;

                _db.SaveChanges();
                return new CommonResponse() { Message = "User values updated." };
            }
            else
            {
                throw new NullReferenceException("UserID provided in request does not exist.");
            }
        }

        public LoginResponse LoginUser(LoginRequest request)
        {
            var userValue = _db.UserValues.FirstOrDefault(x => x.Email == request.Email);
            if (userValue == null)
                throw new UnauthorizedAccessException("Email or password is incorrect.");
            if (!VerifyPassword(request.Password, userValue.PasswordHash))
                throw new UnauthorizedAccessException("Email or password is incorrect.");

            var user = _db.Users.FirstOrDefault(x => x.UserId == userValue.UserId);
            if (user == null)
                throw new NullReferenceException("User not found.");

            return new LoginResponse
            {
                UserId = user.UserId,
                UserName=user.UserName,
                TypeId=user.TypeId
            };
        }

        public VerifiedCodeResponse VerifyCode(VerifyCodeRequest request)
        {
            if (_verificationCodes.TryGetValue(request.UserId, out var storedCode))
            {
                if (_verificationCodes[request.UserId] == request.Code)
                {
                    _verificationCodes.Remove(request.UserId);

                    var userValue = _db.UserValues.FirstOrDefault(x => x.UserId == request.UserId);
                    if (userValue == null)
                    {
                        throw new NullReferenceException("User not found.");
                    }

                    var user = _db.Users.FirstOrDefault(x => x.UserId == userValue.UserId);
                    if (user == null)
                    {
                        throw new NullReferenceException("User not found.");
                    }

                    var token = GenerateJwtToken(user);

                    return new VerifiedCodeResponse
                    {
                        Token = token,
                        UserId = user.UserId,
                        UserName = user.UserName,
                        TypeId = user.TypeId
                    };
                }
                else
                {
                    throw new UnauthorizedAccessException("Invalid verification code.");
                }
            }
            else
            {
                throw new UnauthorizedAccessException("Verification code has expired or not found.");
            }
        }

        public CommonResponse SendVerificationCode(SendMailRequest request)
        {
            var userToVerify = _db.UserValues.FirstOrDefault(x => x.Email.Equals(request.Email));
            if (userToVerify == null)
            {
                throw new NullReferenceException("User with the provided email not found.");
            }

            var verificationCode = GenerateVerificationCode();
            Console.WriteLine($"Storing code {verificationCode} for UserId {userToVerify.UserId.Value}");
            _verificationCodes[userToVerify.UserId.Value] = verificationCode;

            _verificationCodes[request.UserId]=verificationCode;

            var subject = "Your Verification Login Code :: VideoStreamingPlatform";
            var body = @"
    <html>
    <body>
        <h2 style='color: #4CAF50;'>Your Verification Code</h2>
        <p style='font-family: Arial, sans-serif; font-size: 16px;'>
            Your verification code is: <span style='font-weight: bold; font-size: 18px; color: #FF5733;'>"
            + verificationCode + @"
            </span>
        </p>
        <p style='font-family: Arial, sans-serif; font-size: 14px; color: #888;'>
            This code will expire in 10 minutes.
        </p>
    </body>
    </html>";

            try
            {
                _emailService.SendEmail(request.Email,subject,body);
                return new CommonResponse() {Success=true, Message = $"Verification code sent to email."};
            }
            catch (Exception ex)
            {

                return new CommonResponse() {Success=false ,Message = $"Error sending email {ex.Message}"};
            }
        }

        public bool IsValidEmail(string email)
        {
            string emailRegexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailRegexPattern);
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string base64Hash = Convert.ToBase64String(hashBytes);

            return base64Hash;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }

            return true;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecretKey);

            if (key.Length != 32) // Ensure key is 256-bit
                throw new InvalidOperationException("JWT Secret key must be 256 bits (32 bytes).");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, MapUserTypeToRole(user.TypeId))
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private string GenerateVerificationCode()
        {
            var rng = new Random();
            return rng.Next(100000, 999999).ToString();
        }
        private string MapUserTypeToRole(int typeId)
        {
            return typeId switch
            {
                1 => "Admin",
                2 => "SuperAdmin",
                3 => "User",
                4 => "SubscribedUser",
                _ => "User"
            };
        }
    }
}
