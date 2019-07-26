using AutoMapper;
using StoreService.Web.DTO;
using StoreService.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreService.Web.Services
{
    public interface IAuthorService
    {
    }

    public class AuthorService : IAuthorService
    {
        private StoreContext context;
        private IMapper mapper;

        public AuthorService(StoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
    }
}
