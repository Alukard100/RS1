using System.Reflection.Metadata.Ecma335;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public string? Token { get; set; }
    public int? UserId { get; set; }
    public string? UserName { get; set; }
    public int? TypeId{ get; set; }
}

public class VerifyCodeRequest
{
    public int UserId { get; set; }
    public string Code{ get; set; }
}

public class SendMailRequest
{
    public int UserId { get; set; }
    public string Email { get; set; }
}

