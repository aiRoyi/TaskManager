using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.MVC.Web.Models;
using TaskManager.MVC.Web.Repositories;
using TaskManager.MVC.Web.Session;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.MVC.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        private readonly IAdminSession _adminSession;

        public AccountController(IUserRepository repository, IAdminSession adminSession)
        {
            _userRepository = repository;
            _adminSession = adminSession;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                if(_userRepository.IsValid(model.UserName, model.Password))
                {
                    var currentUser = _userRepository.FindByUserName(model.UserName);
                    _adminSession.SetCurrentUser(currentUser);
                    return RedirectToAction("Index", "Task", new { userId = currentUser.Id, userName = currentUser.UserName });
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string errorMsg = null)
        {
            ViewData["ErrorMessage"] = errorMsg;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.FindByUserName(model.UserName);
                if (user != null)
                {
                    return Register("UserName exist!");
                }
                _userRepository.AddOrUpate(model);
                _adminSession.SetCurrentUser(user);
                return RedirectToAction("Index", "Task", new { userId = user.Id, userName = user.UserName });
            }

            return View(model);
        }
    }
}
