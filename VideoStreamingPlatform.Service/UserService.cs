using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.User;
using VideoStreamingPlatform.Commons.DTOs.Responses.User;
using VideoStreamingPlatform.Database.Models;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using System.Linq.Expressions;


namespace VideoStreamingPlatform.Service
{
    public class UserService : IUserService
    {

        VideoStreamingPlatformContext db = new VideoStreamingPlatformContext();

        public List<GetUserResponse> GetUser(GetUserRequest request)
        {
            bool isRequestEmpty = string.IsNullOrEmpty(request.name) &&
                                  string.IsNullOrEmpty(request.surname) &&
                                  string.IsNullOrEmpty(request.userName) &&
                                  (request.userID == null || request.userID == 0);

            var filteredUsers = isRequestEmpty
                ? db.Users.ToList()
                : db.Users.Where(user =>
                    (request.userID == null || user.UserId == request.userID) &&
                    (string.IsNullOrEmpty(request.name) || user.Name.ToLower().StartsWith(request.name.ToLower())) &&
                    (string.IsNullOrEmpty(request.surname) || user.Surname.ToLower().StartsWith(request.surname.ToLower())) &&
                    (string.IsNullOrEmpty(request.userName) || user.UserName.ToLower().StartsWith(request.userName.ToLower()))
                ).ToList();

            var dataList = filteredUsers.Select(user => new GetUserResponse
            {
                userID = user.UserId,
                name = user.Name,
                surname = user.Surname,
                userName = user.UserName
            }).ToList();

            return dataList;
        }


        //List<GetUserResponse> IUserService.GetUser(GetUserRequest request)
        //{
        //    throw new NotImplementedException();
        //}

        public CommonResponse CreateUser(CreateUserRequest request)
        {
            //Provjera da li postoji user sa istim usernameom, a za email cemo provjeravati u servisu UserValueService

            var UserExist = db.Users.Where(user => user.UserName == request.UserName).FirstOrDefault();
            if (UserExist != null)
            {
                throw new InvalidOperationException("Korisnicko ime koje ste unijeli vec postoji.");
            }
            var newUser = new User()
            {
                Name = request.Name,
                Surname = request.Surname,
                UserName = request.UserName,
                BirthDate = request.BirthDate,
                ProfilePicture = request.ProfilePicture,
                Country = request.Country,
                SubscriberCount = 0,
                TypeId = request.TypeId
            };
            var response = db.Users.Add(newUser);
            db.SaveChanges();
            return new CommonResponse() { Id = response.Entity.UserId };
        }


        public CommonResponse DeleteUser(CommonDeleteRequest request)
        {
            var UserObject = db.Users.Where(user => user.UserId == request.Id).FirstOrDefault();




            if (UserObject == null)
            {
                throw new NullReferenceException("Korisnicki nalog ne postoji.");
            }
            //pronalazim wallet sa datim userom prije brisanja, te ga brisem
            var removeUserWallet = db.Wallets.Where(wallet => wallet.UserId == request.Id).FirstOrDefault();
            db.Wallets.Remove(removeUserWallet);
            db.SaveChanges();

            db.Users.Remove(UserObject); db.SaveChanges();
            return new CommonResponse() { Id = request.Id };
        }

        public CommonResponse UpdateUser(UpdateUserRequest request)
        {
            var UserObject = db.Users.Where(user => user.UserId == request.UserId).FirstOrDefault();
            if (UserObject != null)
            {
                UserObject.Name = request.Name ?? UserObject.Name;
                UserObject.Surname = request.Surname ?? UserObject.Surname;
                var KorisnickoImeProvjera = db.Users.Where(user => user.UserName == request.UserName).FirstOrDefault();
                if (KorisnickoImeProvjera != null)
                {
                    throw new InvalidOperationException("Korisnicko ime koje ste unijeli vec postoji.");
                }
                UserObject.UserName = request.UserName ?? UserObject.UserName;
                UserObject.ProfilePicture = request.ProfilePicture ?? UserObject.ProfilePicture;
                UserObject.Country = request.Country ?? UserObject.Country;
                UserObject.TypeId = request.TypeId != 0 ? request.TypeId : UserObject.TypeId;
                db.SaveChanges();
                return new CommonResponse() { Id = request.UserId };
            }
            throw new NullReferenceException("Korisnicki nalog ne postoji.");
        }


    }
}
