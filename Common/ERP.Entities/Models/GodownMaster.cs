﻿using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Entities.Models
{
    public class Godown : Entity
    {
        public int ID { get; set; }
        [Required]
        public string GodownName { get; set; }
        public bool Status { get; set; }
    }
}
