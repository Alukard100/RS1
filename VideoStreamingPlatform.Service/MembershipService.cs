using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Membership;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Membership;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class MembershipService : IMembershipService
    {
        VideoStreamingPlatformContext _db= new VideoStreamingPlatformContext();

        public List<GetMembershipResponse> GetMembership(GetMembershipRequest request)
        {
            var memberships= _db.Memberships.ToList();

            var active = _db.Memberships.Where(x => x.IsActive == request.IsActive).ToList();

            if (request.UserId!=null)
            {
                memberships= memberships.Where(x=>x.UserId==request.UserId).ToList();
            }
            if (request.IsActive!=null)
            {
                memberships = memberships.Where(x => x.IsActive == request.IsActive).ToList();
            }

            var dataList = new List<GetMembershipResponse>();

            foreach (var membership in memberships)
            {
                dataList.Add(new GetMembershipResponse()
                {
                    UserId = membership.UserId,
                    CreatedAt = membership.CreatedAt,
                    EndDate= membership.EndDate,
                    IsActive= membership.IsActive,
                    StartDate= membership.StartDate
                });
            }
            return dataList;
        }
        public CommonResponse UpdateMembership(CreateMembershipRequest request)
        {
            var membership = _db.Memberships.Where(x=>x.UserId == request.UserId).FirstOrDefault();
            if (membership == null) { throw new InvalidOperationException("User with provided ID does not have membership."); }
            var wallet = _db.Wallets.Where(x => x.UserId == request.UserId).FirstOrDefault();
            if (wallet==null || wallet.Balance < 41) { throw new InvalidOperationException("User with provided ID does not have enough balance to extend membership."); }

            // Produzivanje membershipa ce kostati manje nego prvo placanje

            wallet.Balance -= 40;
            TimeSpan daysLeft = membership.EndDate.Value - DateTime.Now;
                        
            membership.StartDate = DateTime.Now;
            membership.EndDate = DateTime.Now.AddMonths(1).AddDays(daysLeft.Days);


            _db.SaveChanges();

            ScheduleMembershipDeactivation(membership);

            return new CommonResponse() { Message = $"Membership for user id {request.UserId} has been extended for 1 month." };

        }

        public CommonResponse DeleteMembership(CommonDeleteRequest request)
        {
            var membership=_db.Memberships.Where(x=>x.MembershipId==request.Id).FirstOrDefault();
            if (membership == null) { throw new InvalidOperationException("Membership with provided ID does not exist."); }
            var response= _db.Memberships.Remove(membership);
            _db.SaveChanges();
            return new CommonResponse() { Id = response.Entity.MembershipId, Message = "Membership with provided ID has been deleted." };
        }

        public CommonResponse CreateMembership(CreateMembershipRequest request)
        {
            var user = _db.Users.Where(x=>x.UserId== request.UserId).FirstOrDefault();
            if (user == null) { throw new InvalidOperationException("User with provided ID does not exist."); }
            //Provjeriti jel user vec ima membership
            var userMembership = _db.Memberships.Where(x=>x.UserId == request.UserId).FirstOrDefault();
            if (userMembership != null && userMembership.IsActive==true) { throw new InvalidOperationException("User with provided ID already has membership."); }
            //Provjeriti jel ima dovoljno balance-a za uplatu membershipa
            var wallet=_db.Wallets.Where(x=>x.UserId== request.UserId).FirstOrDefault();
            //Recimo da membership kosta 50 coinsa
            if (wallet.Balance < 51) { throw new InvalidOperationException("User with provided ID does not have enough balance to pay membership."); }

            wallet.Balance -= 50;

            var membership = new Membership() { 
            UserId = request.UserId,
            IsActive=true,
            StartDate= DateTime.Now,
            CreatedAt= DateTime.Now,
            EndDate=DateTime.Now.AddMonths(1)
            };

            var response= _db.Memberships.Add(membership);
            _db.SaveChanges();

            ScheduleMembershipDeactivation(membership);

            return new CommonResponse() { Id = response.Entity.MembershipId, Message = "Membership successfully activated." };
            
        }
        private void ScheduleMembershipDeactivation(Membership membership)
        {
            var timeUntilEnd = membership.EndDate - membership.StartDate;

            long totalMilliseconds = (long)timeUntilEnd.Value.TotalMilliseconds;

            totalMilliseconds = Math.Min(totalMilliseconds, int.MaxValue);

            Task.Delay((int)totalMilliseconds).ContinueWith(_ => 
            { 
                var membershipDb=_db.Memberships.Where(x=>x.MembershipId==membership.MembershipId).FirstOrDefault();

                if (membershipDb != null) { 
                membershipDb.IsActive=false;
                    _db.SaveChanges();              
                }
            });
        }


    }
}
