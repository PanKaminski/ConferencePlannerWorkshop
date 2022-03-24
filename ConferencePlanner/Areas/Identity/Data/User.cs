using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ConferencePlanner.Areas.Identity.Data
{
    public class User : IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}
