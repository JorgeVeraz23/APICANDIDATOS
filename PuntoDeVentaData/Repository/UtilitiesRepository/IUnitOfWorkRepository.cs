using Data.Interfaces.SecurityInterfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.UtilitiesRepository
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IConfiguration _configuration;

        public UnitOfWorkRepository(ApplicationDbContext context, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
