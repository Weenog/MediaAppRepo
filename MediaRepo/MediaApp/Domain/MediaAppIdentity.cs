using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaApp.Domain
{
    public class MediaAppIdentity : IdentityUser

    {

        public string Gender { get; set; }

    }

}