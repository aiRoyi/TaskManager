using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.MVC.Web.Models;

namespace TaskManager.MVC.Web.Repositories
{
    public interface IUserRepository
    {
        UserModel FindByUserName(string name);

        void AddOrUpate(UserModel model);

        bool IsValid(string userName, string password);
    }

    public class UserRepository : IUserRepository
    {
        private readonly TaskContext _dbContext;

        public UserRepository(TaskContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddOrUpate(UserModel model)
        {
            try
            {
                if (model.Id != 0)
                    _dbContext.Update(model);
                else
                    _dbContext.Add(model);

                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public UserModel FindByUserName(string name)
        {
            try
            {
                return _dbContext.Users.Single(user => user.UserName == name);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public bool IsValid(string userName, string password)
        {
            try
            {
                var checkUser = _dbContext.Users.Single(user => user.UserName == userName && user.Password == password);
                if (checkUser != null)
                    return true;
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
    }
}
