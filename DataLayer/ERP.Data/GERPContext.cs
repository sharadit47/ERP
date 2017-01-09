using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ERP.Data.Models.Mapping;
using ERP.Entities;
using Repository.Pattern.Ef6;
using ERP.Entities.Models;
using ERP.Data.Mapping;
namespace ERP.Data.Models
{
    public partial class GERPContext : DataContext
    {
        static GERPContext()
        {
            Database.SetInitializer<GERPContext>(null);
        }

        public GERPContext()
            : base("Name=ERP_main")
        {
        }

        public DbSet<AccessType> AccessTypes { get; set; }
       
        public DbSet<CompanyWiseModule> CompanyWiseModules { get; set; }
     
        //public DbSet<Designation> Designations { get; set; }
        public DbSet<FYear> FYears { get; set; }
        public DbSet<Gender> Genders { get; set; }
        //public DbSet<Icon> Icons { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Module> Modules { get; set; }
       
        public DbSet<Title> Titles { get; set; }
        public DbSet<UserRight> UserRights { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }

        public DbSet<ModuleGroup> ModuleGroups { get; set; }
       
        public DbSet<UIStyle> UIStyles { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<ERPObjectType> ERPObjectTypes { get; set; }
        public DbSet<UrlContext> UrlContexts { get; set; }
        public DbSet<SystemSetting> SystemSettingMaster { get; set; }
        public DbSet<ModuleGroupAccess> ModuleGroupsAccess { get; set; }
        public DbSet<UserModules> UsersModules { get; set; }
        public DbSet<RolesProvider> RolesProviderMaster { get; set; }

        public DbSet<Country> Countries { get; set; }
         


        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<SalesExecutive> SalesExecutives { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Zone> Zones { get; set; }
        //public DbSet<UserType> UserTypes { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccessTypeMap());         
            modelBuilder.Configurations.Add(new CompanyWiseModuleMap());    
            modelBuilder.Configurations.Add(new FYearMap());
            modelBuilder.Configurations.Add(new GenderMap());
            //modelBuilder.Configurations.Add(new IconMap());
            modelBuilder.Configurations.Add(new MaritalStatusMap());
            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new ModuleMap());            
            modelBuilder.Configurations.Add(new TitleMap());
            modelBuilder.Configurations.Add(new UserRightMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
            modelBuilder.Configurations.Add(new SystemSettingsMap());

            modelBuilder.Configurations.Add(new BuyerMasterMap());
            modelBuilder.Configurations.Add(new CityMasterMap());
            modelBuilder.Configurations.Add(new CountryMasterMap());
            modelBuilder.Configurations.Add(new DepartmentMasterMap());
            modelBuilder.Configurations.Add(new DesignationMasterMap());
            modelBuilder.Configurations.Add(new FactoryMasterMap());
            modelBuilder.Configurations.Add(new SalesExecutiveMasterMap());
            modelBuilder.Configurations.Add(new StateMasterMap());
            modelBuilder.Configurations.Add(new tblProductMap());
            modelBuilder.Configurations.Add(new ZoneMasterMap());
        }
    }
}
