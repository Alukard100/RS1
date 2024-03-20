using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.UserValues;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class UserValuesService : IUserValuesService
    {
        VideoStreamingPlatformContext _db = new VideoStreamingPlatformContext();

        public CommonResponse CreateUserValues(CreateUserValuesRequest request) {
            var userExists = _db.Users.Where(x => x.UserId == request.UserId).FirstOrDefault();
            if (userExists == null) { throw new NullReferenceException("UserID provided in request does not exist."); }

            var userValueExists= _db.UserValues.Where(x=>x.UserId == request.UserId).FirstOrDefault();
            if (userValueExists != null) { throw new NullReferenceException("UserID provided in request already exists."); }

            var emailExists= _db.UserValues.Where(x=>x.Email.Equals(request.Email)).FirstOrDefault();
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
            return new CommonResponse() { Id = response.Entity.UserValuesId ,Message = "UserValue successfully added."};
    }

        public CommonResponse DeleteUserValues(CommonDeleteRequest request)
        {
            var removeObject=_db.UserValues.Where(x=>x.UserId==request.Id).FirstOrDefault();
            if (removeObject != null)
            {
                _db.UserValues.Remove(removeObject);
                _db.SaveChanges();
                return new CommonResponse() { Id = request.Id, Message = "UserValue removed." };
            }
            throw new NullReferenceException("UserID provided in request does not exist.");


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
    }
}
