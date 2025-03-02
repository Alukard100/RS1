namespace VideoStreamingPlatform.Commons.DTOs.Requests.GroupMembers
{
    public class CreateGroupMembersRequest
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int UserId { get; set; }
        //promijeniti na listu UserId  =>  List<int>

    }
}