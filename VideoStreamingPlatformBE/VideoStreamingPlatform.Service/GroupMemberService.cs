using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.GroupMembers;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.GroupMembers;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class GroupMemberService : IGroupMemberService
    {
        //VideoStreamingPlatformContext db = new VideoStreamingPlatformContext();
        private readonly VideoStreamingPlatformContext db;
        public GroupMemberService(VideoStreamingPlatformContext dbContext)
        {
            db = dbContext;
        }

        public CommonResponse CreateGroupMembers(CreateGroupMembersRequest request)
        {
            var newObject = new GroupMember()
            {
                UserId = request.UserId,
                GroupId = request.GroupId
            };

            var response = db.GroupMembers.Add(newObject);
            db.SaveChanges();
            return new CommonResponse() { Id = response.Entity.GroupMemberId };

        }

        public CommonResponse DeleteGroupMembers(CommonDeleteRequest request)
        {
            var removeObject = db.GroupMembers.Where(x => x.GroupMemberId == request.Id).FirstOrDefault();

            //provjera da li objekat sa proslijedjenim ID postoji u bazi.
            if (removeObject != null)
            {
                db.GroupMembers.Remove(removeObject);
                db.SaveChanges();
                return new CommonResponse() { Id = request.Id };
            }

            throw new NullReferenceException("Object with provided ID does not exist.");
        }

        public List<GetGroupMemberResponse> GetGroupMembers(GetGroupMembersRequest request)
        {
            //Include?
            var response = db.GroupMembers
                .Where(x => x.GroupId == request.GroupId)
                .ToList();

            var dataList = new List<GetGroupMemberResponse>();

            foreach (var item in response)
            {
                dataList.Add(new GetGroupMemberResponse()
                {
                    UserId = item.UserId
                });
            }
            return dataList;
        }

        public CommonResponse UpdateGroupMembers(UpdateGroupMembersRequest request)
        {
            var entry = db.GroupMembers.Where(x => x.GroupMemberId == request.GroupMemberId).FirstOrDefault();
            if (entry != null)
            {
                entry.GroupMemberId = request.GroupMemberId;
                entry.GroupId = request.GroupId;
                entry.UserId = request.UserId;


                db.SaveChanges();
                return new CommonResponse() { Id = request.GroupMemberId };
            }
            throw new NullReferenceException("Object with provided ID does not exist.");
        }
    }
}