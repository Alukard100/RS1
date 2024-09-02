namespace VideoStreamingPlatform.Commons.DTOs.Requests.GroupMembers
{
    public class UpdateGroupMembersRequest
    {
        public int GroupMemberId { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}