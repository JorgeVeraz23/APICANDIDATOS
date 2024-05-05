using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.SecurityInterfaces
{
    public interface IUnitOfWorkRepository
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
