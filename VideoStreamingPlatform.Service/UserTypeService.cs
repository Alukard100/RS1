using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.UserType;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.UserType;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{

    public class UserTypeService : IUserTypeService
    {
        VideoStreamingPlatformContext db = new VideoStreamingPlatformContext();

        public CommonResponse CreateUserType(CreateUserTypeRequest request)
        {
            var UserTypeExist = db.UserTypes.Where(x => x.Type == request.Type).FirstOrDefault();
            if (UserTypeExist != null)
            {
                throw new InvalidOperationException("Tip korisnika koji ste unijeli vec postoji.");
            }
            var newUserType = new UserType()
            {
                Type = request.Type
            };
            var response = db.UserTypes.Add(newUserType);
            db.SaveChanges();
            return new CommonResponse { Id = response.Entity.TypeId };
        }

        public CommonResponse DeleteUserType(CommonDeleteRequest request)
        {
            var removeObject = db.UserTypes.Where(type=>type.TypeId==request.Id).FirstOrDefault();
            if (removeObject!=null)
            {
                db.UserTypes.Remove(removeObject);
                db.SaveChanges();
                return new CommonResponse() { Id = request.Id};
            }
            throw new InvalidOperationException("Tip usera koji ste odabrali ne postoji");
        }

        public IEnumerable<GetUserTypeResponse> GetUserType()
        {
            var dataList = db.UserTypes.ToList();

            var userTypeResponse= dataList.Select(x => new GetUserTypeResponse() { TypeId=x.TypeId,Type=x.Type});

            return userTypeResponse;
        }

    }
}


