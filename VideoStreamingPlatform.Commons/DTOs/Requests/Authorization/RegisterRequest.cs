public class RegisterRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; } // Optional: Assign default role (e.g., 3 for regular user)
}
