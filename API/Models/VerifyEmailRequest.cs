namespace SRPM.API.Models
{
    public class VerifyEmailRequest
    {
        public string Email { get; set; } = string.Empty;
        public string VerificationCode { get; set; } = string.Empty;
    }
}
