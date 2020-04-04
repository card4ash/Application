using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppProj.Domain;
using AppProj.Data.Repositories;
using AppProj.Data.Infrastructure;
using AppProj.Service.Services;

namespace AppProj.Service.ServicesImpl
{

    public class UserProfileService : IUserProfileService
    {        
        readonly IUserProfileRepository userProfileRepository;
        
        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
        }

        public UserProfile GetByPin(string pin)
        {
            return userProfileRepository.Get(expression => expression.Pin == pin);
        }

        public IEnumerable<UserProfile> GetAll(string searchText, int userId)
        {
            if (searchText == "" || searchText == null)
            {
                return userProfileRepository.GetMany(c=>c.Id !=userId);
            }

            return userProfileRepository.GetMany(c => (c.Pin.Contains(searchText) || c.UserName.Contains(searchText)) && c.Id != userId);
        }
        public IEnumerable<UserProfile> GetAllUsers()
        {

            return userProfileRepository.GetAll();

        }
        public UserProfile GetDataById(int id)
        {
            return userProfileRepository.GetById(id);
        }
        public IEnumerable<UserProfile> GetDataByRoleId(int roleId)
        {
            return userProfileRepository.GetMany(c => c.RoleId == roleId);
        }

        public void Add(UserProfile entity)
        {            
            if (GetByPin(entity.Pin) == null)
            {
                userProfileRepository.Add(entity);
            }
        }
        public void Update(UserProfile entity)
        {
            if (userProfileRepository.GetMany(c => c.Pin == entity.Pin && c.Id != entity.Id).Count() <= 0)
            {
                userProfileRepository.Update(entity);                
            }
        }
    }
}
