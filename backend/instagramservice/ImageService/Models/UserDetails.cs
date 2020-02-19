using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.picsfeed.ImageService.Models
{
    public class UserDetails
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
    }
}
