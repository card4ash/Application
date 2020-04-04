using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppProj.Domain;


namespace AppProj.Service.Services
{
    public interface IUserProfileService
    {
        UserProfile GetByPin(string pin);
        IEnumerable<UserProfile> GetAll(string searchText, int userId);
        IEnumerable<UserProfile> GetAllUsers();
        UserProfile GetDataById(int id);
        IEnumerable<UserProfile> GetDataByRoleId(int roleId);
        void Add(UserProfile entity);
        void Update(UserProfile entity);
    }
}
