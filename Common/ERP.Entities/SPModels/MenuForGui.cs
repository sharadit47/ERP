﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Entities.SPModels
{
  public  class MenuForGui
    {
        public int Count { get; set; }
        public string ModuleName { get; set; }
        public int ModuleID { get; set; }
        public int ModuleOrder { get; set; }
        public IList<MenuDetails> Menu { get; set; }
    }

  public class ModuleForGui
  {
      public int Count { get; set; }
      public string ModuleGroupName { get; set; }
      public int ModuleGroupID { get; set; }
      public int ModuleGroupOrder { get; set; }
      public IList<ModuleDetails> Modules { get; set; }
  }

  public class MenuDetails : Menu
  {
      public string ModuleName { get; set; }
  }
  public class ModuleDetails : Module
  {
      public string ModuleGroupName { get; set; }
  }
  public class UserMenu
  {
      public int ModuleId { get; set; }
      public string MenuCode { get; set; }
      public string MenuName { get; set; }
  }

  public class UserMenuMap
  {
      public int MenuID { get; set; }
      public int AccessTypeID { get; set; }     
  }
  public class UserMenuDetails
  {
      public int Id { get; set; }
      public int ModuleId { get; set; }
      public string ModuleName { get; set; }
      public string MenuCode { get; set; }
      public string MenuName { get; set; }
      public string backgroundColor { get; set; }
      public int? IconID { get; set; }
      public string FontColor { get; set; }
      public string ControllerName { get; set; }
      public string ActionName { get; set; }
      public string UserEmail { get; set; }

      public int Orderby { get; set; }
      public int AccessTypeID { get; set; }
      public int UserID { get; set; }	
  }

  public class UserModuleDetails
  {
      public int ModuleId { get; set; }
      public string ModuleName { get; set; }     
      public int ModuleGroupId { get; set; }      
      public string ModuleGroupName { get; set; }
      public string UserEmail { get; set; }
      public int UserID { get; set; }
      public int ModuleOrder { get; set; }
      public int ModuleGroupOrder { get; set; }
      public string backgroundColor { get; set; }
      public string FontIconClass { get; set; }
      public string FontColor { get; set; }

      public string ControllerName { get; set; }
      public string ActionName { get; set; }
      public string AreaName { get; set; }
  }

  public class UserSetting
  {
      public string URL { get; set; }
      public string SkinTheme { get; set; }
  }
}
