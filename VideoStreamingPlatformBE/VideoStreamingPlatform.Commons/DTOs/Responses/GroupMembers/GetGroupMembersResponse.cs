namespace VideoStreamingPlatform.Commons.DTOs.Responses.GroupMembers
{
    public class GetGroupMemberResponse
    {
        public int? GroupId { get; set; }
        public string? GroupName { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
    }
}