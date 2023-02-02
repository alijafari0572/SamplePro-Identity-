using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Site.Persistance.Contexts
{
    public class DataBaseContext:IdentityDbContext
    {
        public DataBaseContext(DbContextOptions databaseOperations)
        : base(databaseOperations)
             {
        }
    }
}
