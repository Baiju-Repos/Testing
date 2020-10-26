namespace Okta_Web.Models
{
    public class Credentials
    {
        public RecoveryQuestion recovery_question { get; set; }
        public Provider provider { get; set; }
        public Password password { get; set; }
    }
}
