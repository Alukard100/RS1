namespace VideoStreamingPlatform.Commons.DTOs.Requests.User
{
    public class CreateUserRequest
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public string? Country { get; set; }
        public int ?TypeId { get; set; }

    }
}