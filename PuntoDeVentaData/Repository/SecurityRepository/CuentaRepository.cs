using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkQuality.Data.Repository.SecurityRepository
{
    public class CuentaRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public CuentaRepository(ApplicationDbContext context, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }
    }
}
