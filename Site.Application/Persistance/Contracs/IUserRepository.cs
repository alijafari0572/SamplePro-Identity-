using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Persistance.Contracs
{
    public class IUserRepository : IGenericRepository<IdentityUser>
    {
        public Task<IdentityUser> Add(IdentityUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> Delete(IdentityUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<IdentityUser>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> Update(IdentityUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
