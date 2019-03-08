
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.MVC.Web.Models;

namespace TaskManager.MVC.Web.Session
{
    public interface IAdminSession
    {
        IHttpContextAccessor GetHttpContextAccessor();

        void SetCurrentUser(UserModel user);

        UserModel GetCurrentUser();
    }

    public class AdminSession : IAdminSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public AdminSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IHttpContextAccessor GetHttpContextAccessor()
        {
            return _httpContextAccessor;
        }

        public void SetCurrentUser(UserModel user)
        {
            _session.Set<UserModel>("User", user);
        }

        public UserModel GetCurrentUser()
        {
            return _session.Get<UserModel>("User");
        }
    }
}
