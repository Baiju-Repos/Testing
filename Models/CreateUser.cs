namespace Okta_Web.Models
{
    public class CreateUser
    {
        public UserProfile profile { get; set; }
        public Credentials credentials { get; set; }
    }
}
