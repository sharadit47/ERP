﻿

using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using Repository.Pattern.Infrastructure;
using ERP.Data.Models;
using ERP.Services.Utilities;
using ERP.Entities;
using ERP.Repository;
using ERP.Entities.SPModels;
namespace ERP.Services
{
    public class MembershipService : IMembershipService
    {
        #region Variables
        private readonly IRepositoryAsync<User> _userRepository;
        private readonly IRepositoryAsync<Role> _roleRepository;
        private readonly IRepositoryAsync<UserRole> _userRoleRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IERPStoredProcedure _storedProcedueRepository;      
        #endregion
        public MembershipService(IRepositoryAsync<User> userRepository, IRepositoryAsync<Role> roleRepository,
     IRepositoryAsync<UserRole> userRoleRepository, IEncryptionService encryptionService, 
            IUnitOfWorkAsync unitOfWork,IERPStoredProcedure storedProcedueRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _encryptionService = encryptionService;
            _unitOfWorkAsync = unitOfWork;
            _userRoleRepository = userRoleRepository;
            _storedProcedueRepository = storedProcedueRepository;
            
        }

        #region IMembershipService Implementation

        public MembershipContext ValidateUser(string username, string password)
        {
            var membershipCtx = new MembershipContext();

            var user = _userRepository.GetUserByUsername(username);
            if (user != null && isUserValid(user, password))
            {
                var userRoles = GetUserRoles(user.Username);
                membershipCtx.User = user;

                var identity = new GenericIdentity(user.Username);
                membershipCtx.Principal = new GenericPrincipal(
                    identity,
                    userRoles.Select(x => x.Name).ToArray());
            }

            return membershipCtx;
        }
        public User CreateUser(User user, string password, int[] roles) //string username, string email, string password, int[] roles)
        {
            var existingUser = _userRepository.GetUserByUsername(user.Email);
            if (existingUser != null)
            {
                throw new Exception("Username is already in use");
            }

            var passwordSalt = _encryptionService.CreateSalt();
            user.Salt = passwordSalt;
            user.IsLocked = false;
            user.HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt);
            user.ObjectState = ObjectState.Added;
            _userRepository.Insert(user);
            _unitOfWorkAsync.SaveChanges();
            if (roles != null && roles.Length > 0)
            {
                foreach (var role in roles)
                {
                    addUserToRole(user, role);
                }
            }
            return user;
        }

        public User CreateUserByManager(User user, string password, string CreatedByUserEmail)
        {
            var existingUser = _userRepository.GetUserByUsername(user.Email);
            var manager = _userRepository.GetUserByUsername(CreatedByUserEmail);
            if (existingUser != null)
            {
                throw new Exception("Username is already in use");
            }
            var passwordSalt = _encryptionService.CreateSalt();
            user.Salt = passwordSalt;
            user.IsLocked = false;
            user.HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt);
            user.ObjectState = ObjectState.Added;
            user.Manager = manager;
            user.CreatedByUserID = manager.UserID;
            user.ManagerID = manager.UserID;
            _userRepository.Insert(user);
            _unitOfWorkAsync.SaveChanges();
            return user;
        }

        public User CreateUser(User user, string password, string CreatedByUserEmail, UserType userType)
        {
            var existingUser = _userRepository.GetUserByUsername(user.Email);
            var manager = _userRepository.GetUserByUsername(CreatedByUserEmail);
            if (existingUser != null)
            {
                throw new Exception("Username is already in use");
            }
            if (user.Username == null)
                user.Username = user.Email;
            var passwordSalt = _encryptionService.CreateSalt();
            user.Salt = passwordSalt;
            user.IsLocked = false;
            user.HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt);
            user.ObjectState = ObjectState.Added;
            //user.Manager = manager;
            user.CreatedByUserID = manager.UserID;
            user.ManagerID = manager.UserID;
            user.UserType = userType;
            user.UserTypeID = userType.ID;
            _userRepository.Insert(user);
            _unitOfWorkAsync.SaveChanges();
            return user;
        }

        public User CreateUser(User user, string password, string CreatedByUserEmail, UserType userType, int userReferenceID)
        {
            var existingUser = _userRepository.GetUserByUsername(user.Email);
            var manager = _userRepository.GetUserByUsername(CreatedByUserEmail);
            if (existingUser != null)
            {
                throw new Exception("Username is already in use");
            }
            var passwordSalt = _encryptionService.CreateSalt();
            user.Salt = passwordSalt;
            user.IsLocked = false;
            user.HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt);
            user.ObjectState = ObjectState.Added;
            user.Manager = manager;
            user.CreatedByUserID = manager.UserID;
            user.ManagerID = manager.UserID;
            user.UserType = userType;
            user.UserTypeID = userType.ID;
            user.UserReferenceID = userReferenceID;
            _userRepository.Insert(user);
            _unitOfWorkAsync.SaveChanges();
            return user;
        }
       
        public User GetUser(int userId)
        {
            return _userRepository.GetSingle(userId);
        }

        public List<Role> GetUserRoles(string username)
        {
            List<Role> _result = new List<Role>();

            var existingUser = _userRepository.GetUserByUsername(username);

            if (existingUser != null)
            {
                //foreach (var userRole in existingUser.UserRoles)
                //{
                //    _result.Add(userRole.Role);
                //}
            }

            return _result.Distinct().ToList();
        }
        #endregion

        #region Helper methods
        private void addUserToRole(User user, int roleId)
        {
            var role = _roleRepository.GetSingle(roleId);
            if (role == null)
                return;
            //throw new ApplicationException("Role doesn't exist.");

            var userRole = new UserRole()
            {
                RoleId = role.ID,
                UserId = user.UserID,
            };
            _userRoleRepository.Insert(userRole);
        }

        private bool isPasswordValid(User user, string password)
        {
            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        private bool isUserValid(User user, string password)
        {
            if (isPasswordValid(user, password))
            {
                return !user.IsLocked;
            }

            return false;
        }
        #endregion
        public void ManageUserStatus(string email, bool status)
        {
            var existingUser = _userRepository.GetUserByUsername(email);
            if (existingUser != null)
            {
                existingUser.IsLocked = (!status);
                existingUser.ObjectState = ObjectState.Modified;
                _unitOfWorkAsync.SaveChangesAsync();
            }
        }

        List<UserRight> IMembershipService.GetUserRoles(string username)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<User> GetUsersByManageEmail(string emailID)
        {
            var manager = _userRepository.GetUserByUsername(emailID);
            if (manager != null)
                return _userRepository.Queryable().Where(x => x.ManagerID == manager.UserID);
            else
            {
                return null;
            }
        }

        public User GetUsersByEmail(string emailID)
        {
            return _userRepository.GetUserByUsername(emailID);
        }

        public UserClaims GetUserClaims(string emailID)
        {
            return _storedProcedueRepository.GetUserClaims(emailID).FirstOrDefault();
        }
    }
}
