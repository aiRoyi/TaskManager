using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.MVC.Web.Controllers;
using TaskManager.MVC.Web.Models;
using TaskManager.MVC.Web.Repositories;
using TaskManager.MVC.Web.ViewModels;
using Xunit;

namespace TaskManager.Test.Controllers
{
    public class TaskControllerTest
    {
        private ITaskRepository _taskRepository;

        public TaskControllerTest()
        {
            Mock<ITaskRepository> mockRep = new Mock<ITaskRepository>();
            mockRep.Setup(m => m.FindAll()).Returns(new List<TaskModel> {
            new TaskModel { Id = 1, Owner = 1, Status = TaskStatus.NotDone, Description = "test1", Tag = "test", Type = "test", CreateTime = DateTime.Now },
            new TaskModel { Id = 2, Owner = 1, Status = TaskStatus.NotDone, Description = "test2", Tag = "test", Type = "test", CreateTime = DateTime.Now },
            new TaskModel { Id = 3, Owner = 1, Status = TaskStatus.NotDone, Description = "test3", Tag = "other", Type = "other", CreateTime = DateTime.Now },
            });

            mockRep.Setup(m => m.FindAllByType(It.Is<string>(type => type == "test"))).Returns(new List<TaskModel> {
            new TaskModel { Id = 1, Owner = 1, Status = TaskStatus.NotDone, Description = "test1", Tag = "test", Type = "test1", CreateTime = DateTime.Now },
            new TaskModel { Id = 2, Owner = 1, Status = TaskStatus.NotDone, Description = "test2", Tag = "test", Type = "test2", CreateTime = DateTime.Now },
            });

            mockRep.Setup(m => m.FindAllByType(It.Is<string>(type => type == "other"))).Returns(new List<TaskModel> {
            new TaskModel { Id = 3, Owner = 1, Status = TaskStatus.NotDone, Description = "test3", Tag = "other", Type = "other", CreateTime = DateTime.Now }
            });

            mockRep.Setup(m => m.FindOne(It.Is<int>(id => id == 1))).Returns(new TaskModel {  Id = 1, Owner = 1, Status = TaskStatus.NotDone, Description = "test1", Tag = "test", Type = "test", CreateTime = DateTime.Now });

            mockRep.Setup(m => m.FindOne(It.Is<int>(id => id == 2))).Returns(new TaskModel { Id = 2, Owner = 1, Status = TaskStatus.NotDone, Description = "test2", Tag = "test", Type = "test", CreateTime = DateTime.Now });

            mockRep.Setup(m => m.FindOne(It.Is<int>(id => id == 3))).Returns(new TaskModel { Id = 3, Owner = 1, Status = TaskStatus.NotDone, Description = "test3", Tag = "other", Type = "other", CreateTime = DateTime.Now });

            mockRep.Setup(m => m.FindOne(It.Is<int>(id => id != 1 && id != 2 && id != 3))).Returns<TaskModel>(null);

            _taskRepository = mockRep.Object;
        }

        [Fact]
        public void TaskIndexShouldReturnAllTasks()
        {
            // Arrange
            var controller = new TaskController(_taskRepository);

            // Act
            var actionResult = controller.Index(1, "chenmeiyi", null) as ViewResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.Equal(_taskRepository.FindAll(), actionResult.Model);
        }

        [Fact]
        public void TaskIndexWithSearchTypeShouldReturnSpecificTasks()
        {
            // Arrange
            var controller = new TaskController(_taskRepository);

            // Act
            var actionResult = controller.Index(1, "chenmeiyi", "test") as ViewResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.Equal(_taskRepository.FindAllByType("test"), actionResult.Model);
        }

        [Fact]
        public void TaskEditShouldReturnTaskViewModel()
        {
            // Arrange
            var controller = new TaskController(_taskRepository);

            // Act
            var actionResult = controller.Edit(1, "chenmeiyi", 1) as ViewResult;

            // Assert
            Assert.NotNull(actionResult);

            Assert.IsType<UpdateTaskViewModel>(actionResult.Model);
        }

        [Fact]
        public void TaskEditWithoutIdShouldReturnNotFound()
        {
            // Arrange
            var controller = new TaskController(_taskRepository);

            // Act
            var actionResult = controller.Edit(1, "chenmeiyi", null);

            // Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
