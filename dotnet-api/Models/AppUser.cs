using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace dotnet_api.Models
{
    // 1. We need to create a user model, so we use the built-in class, that can be customized, after go to the file ApplicationDBContext
    public class AppUser : IdentityUser
    {

    }
}