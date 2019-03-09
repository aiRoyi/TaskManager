using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.MVC.Web.Models;
using TaskManager.MVC.Web.Repositories;
using TaskManager.MVC.Web.ViewModels;

namespace TaskManager.MVC.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository repository)
        {
            _taskRepository = repository;
        }

        public IActionResult Index(int userId, string userName, string searchString)
        {
            if (userName != null)
            {
                ViewData["UserName"] = userName;
                ViewData["UserId"] = userId;
            }

            if (searchString != null)
                return View(_taskRepository.FindAllByType(searchString));

            return View(_taskRepository.FindAll());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskModel = _taskRepository.FindOne((int)id);
            if (taskModel == null)
            {
                return NotFound();
            }

            return View(taskModel);
        }

        public IActionResult Create(int userId, string userName)
        {
            ViewData["UserName"] = userName;
            ViewData["UserId"] = userId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UpdateTaskViewModel taskModel, int userId, string userName)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.AddOrUpate(new TaskModel()
                {
                    CreateTime = DateTime.Now,
                    Description = taskModel.Description,
                    Owner = userId,
                    Status = taskModel.Status,
                    Tag = taskModel.Tag,
                    Type = taskModel.Type
                });
                return RedirectToAction(nameof(Index), new { userId, userName });
            }
            return View(taskModel);
        }

        public IActionResult Edit(int userId, string userName, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["UserName"] = userName;
            ViewData["UserId"] = userId;
            var task = _taskRepository.FindOne((int)id);
            UpdateTaskViewModel taskModel = new UpdateTaskViewModel()
            {
                Id = task.Id,
                Description = task.Description,
                Status = task.Status,
                Tag = task.Tag,
                Type = task.Type
            };
            if (taskModel == null)
            {
                return NotFound();
            }
            return View(taskModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int userId, string userName, int id, [Bind("Id,Description,Owner,Status,Tag,Type,CreateTime")] UpdateTaskViewModel taskModel)
        {
            if (id != taskModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var task = _taskRepository.FindOne(taskModel.Id);
                task.Description = taskModel.Description;
                task.Status = taskModel.Status;
                task.Tag = taskModel.Tag;
                task.Type = taskModel.Type;
                _taskRepository.AddOrUpate(task);

                return RedirectToAction(nameof(Index), new { userId, userName });
            }
            return View(taskModel);
        }

        public IActionResult Delete(int userId, string userName, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["UserName"] = userName;
            ViewData["UserId"] = userId;
            var taskModel = _taskRepository.FindOne((int)id);
            if (taskModel == null)
            {
                return NotFound();
            }

            return View(taskModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int userId, string userName, int id)
        {
            var taskModel = _taskRepository.FindOne((int)id);
            _taskRepository.Delete(taskModel);
            return RedirectToAction(nameof(Index), new { userId, userName });
        }
    }
}
