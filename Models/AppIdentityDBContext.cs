using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProj.Models
{   //setting up a class called AppIdentityDBContext that inherits from a class called IdentityDbContext of type IdentityUser
    public class AppIdentityDBContext : IdentityDbContext<IdentityUser>
    {
        //constructor to set up options of this context file
        public AppIdentityDBContext(DbContextOptions options) : base(options)
        {

        }
    }
}
