using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RugerRumble.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime AddDate { get; set; }
    }
}
