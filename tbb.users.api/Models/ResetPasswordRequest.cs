namespace tbb.users.api.Models
{
    public class ResetPasswordRequest
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
