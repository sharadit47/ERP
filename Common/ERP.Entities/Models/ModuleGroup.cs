﻿using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Entities.Models
{
    public class ModuleGroup : Entity
    {
        public ModuleGroup()
        {
            Modules = new HashSet<Module>();
        }
        public int ModuleGroupID { get; set; }
        public string Name { get; set; }
        public int Orderby { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        public virtual UIStyle UIStyle { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
    }

}
