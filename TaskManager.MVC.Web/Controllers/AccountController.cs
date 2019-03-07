using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.MVC.Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.MVC.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly TaskContext _context;

        public AccountController(TaskContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
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
                var users = from user in _context.Users select user;
                var current = users.FirstOrDefault(user => user.UserName == model.UserName && user.Password == model.Password);
                if (current != null)
                    return RedirectToAction("Index", "Task", new { userId = current.Id, userName = current.UserName });
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
        public async Task<IActionResult> Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var users = from user in _context.Users select user;
                var current = users.FirstOrDefault(user => user.UserName == model.UserName);
                if (current != null)
                {
                    return Register("UserName exist!");
                }
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Task", new { userId = current.Id, userName = current.UserName });
            }

            return View(model);
        }
    }
}
