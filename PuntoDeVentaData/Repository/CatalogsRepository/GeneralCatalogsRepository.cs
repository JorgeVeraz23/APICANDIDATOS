using Data.Dto.CatalogsDto;
using Data.Interfaces.CatalogsInterfaces;
using Data.Interfaces.SecurityInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PuntoDeVentaData.Dto.UtilitiesDTO;
using PuntoDeVentaData.Entities.Enumerators;
using PuntoDeVentaData.Entities.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.CatalogsRepository
{
    public  class GeneralCatalogsRepository : GeneralCatalogsInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private string username;
        private string _ip;
        private IUnitOfWorkRepository _unit;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private MessageInfoDTO? infoDTO;

        public GeneralCatalogsRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider, IConfiguration configuration, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _context = context;
            _userManager = userManager;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _unit = unitOfWorkRepository;
        }


        public List<TypeDocumentDTO> GetAllTypeDocuments()
        {
            List<TypeDocumentDTO> typeDocumentList = new List<TypeDocumentDTO>();

            foreach (EnumTypeDocument enumValue in Enum.GetValues(typeof(EnumTypeDocument)))
            {
                var enumMember = enumValue.GetType().GetMember(enumValue.ToString());
                var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(enumMember[0], typeof(DescriptionAttribute));

                var typeDocument = new TypeDocumentDTO
                {
                    IdTypeDocument = (int)enumValue,
                    Text = descriptionAttribute.Description
                };

                typeDocumentList.Add(typeDocument);
            }


            return typeDocumentList;
        }
    }
}
