using System.Collections.Generic;

namespace WebRailwayApp.Models
{
    public class userDisplay
    {
        public User user { get; set; }
        public List<Role> roles { get; set; }
        public List<User> users { get; set; }

    }
}