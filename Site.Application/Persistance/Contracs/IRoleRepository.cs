using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Persistance.Contracs
{
    public class IRoleRepository : IGenericRepository<IdentityRole>
    {
        public Task<IdentityRole> Add(IdentityRole entity)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> Delete(IdentityRole entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<IdentityRole>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityRole> Update(IdentityRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
