﻿using ERP.Entities;
using ERP.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Services
{
    public class UrlContextService : Service<UrlContext>, IUrlContextService
    {
        public readonly IRepositoryAsync<UrlContext> _repository;
        public UrlContextService(IRepositoryAsync<UrlContext> repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
    public interface IUrlContextService : IService<UrlContext>
    {
    }
}
