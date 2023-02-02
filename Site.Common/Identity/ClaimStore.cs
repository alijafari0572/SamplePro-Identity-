using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Site.Common.Identity
{
    public static class ClaimStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim(ClaimTypeStore.Owner,true.ToString()),
            new Claim(ClaimTypeStore.Admin,true.ToString()),
            new Claim(ClaimTypeStore.User,true.ToString())
        };
    }
    
}