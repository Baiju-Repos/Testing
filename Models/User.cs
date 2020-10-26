using System;

namespace Okta_Web.Models
{
    public class User
    {
        public string id { get; set; }
        public string status { get; set; }
        public DateTime created { get; set; }
        public DateTime? activated { get; set; }
        public DateTime? statusChanged { get; set; }
        public DateTime? lastLogin { get; set; }
        public DateTime? lastUpdated { get; set; }
        public DateTime? passwordChanged { get; set; }
        public Types type { get; set; }
        public UserProfile profile { get; set; }
        public Credentials credentials { get; set; }
        public Links _links { get; set; }
    }
}
