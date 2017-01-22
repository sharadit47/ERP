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
    public class ColorService : Service<Color>, IColorService
    {
        public readonly IRepositoryAsync<Color> _repository;
        public ColorService(IRepositoryAsync<Color> repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
    public interface IColorService : IService<Color>
    {
    }
}
