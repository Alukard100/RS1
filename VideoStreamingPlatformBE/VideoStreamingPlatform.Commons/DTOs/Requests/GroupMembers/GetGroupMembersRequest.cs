namespace VideoStreamingPlatform.Commons.DTOs.Requests.GroupMembers
{
    public class GetGroupMembersRequest
    {
        public int GroupId { get; set; }
        public int GroupMemberId { get; set; }
        public int UserId { get; set; }
    }
}
