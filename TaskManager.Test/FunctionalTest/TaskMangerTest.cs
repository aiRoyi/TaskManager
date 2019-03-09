using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using TaskManager.MVC.Web;
using TaskManager.MVC.Web.Models;
using TaskManager.MVC.Web.ViewModels;
using TaskManager.Test.Utils;
using Xunit;

namespace TaskManager.Test.FunctionalTest
{
    public class TaskMangerTest 
    {   
        [Fact]
        public void LoginTest()
        {
            var client = new TestServer(WebHost
                .CreateDefaultBuilder()
                .UseContentRoot(ProjectUtil.GetProjectPath("TaskManager.sln", "", typeof(Startup).Assembly))
                .UseStartup<Startup>())
                .CreateClient();
            var userModel = new UserModel { Id = 1, UserName = "chenmeiyi", Password = "cmy123456" };
            var jsonString = JsonConvert.SerializeObject(userModel);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var result = client.PostAsJsonAsync("Account/Login", content).Result;
            var error = client.PostAsJsonAsync("Account/Login", content).Exception;
            Assert.NotNull(result);
            Assert.Null(error);
        }

        [Fact]
        public void RegisterTest()
        {
            var client = new TestServer(WebHost
                .CreateDefaultBuilder()
                .UseContentRoot(ProjectUtil.GetProjectPath("TaskManager.sln", "", typeof(Startup).Assembly))
                .UseStartup<Startup>())
                .CreateClient();

            var result = client.GetStreamAsync("Account/Register").Result;
            var error = client.GetStreamAsync("Account/Register").Exception;
            Assert.NotNull(result);
            Assert.Null(error);
        }

        [Fact]
        public void CreateTaskTest()
        {
            var client = new TestServer(WebHost
                .CreateDefaultBuilder()
                .UseContentRoot(ProjectUtil.GetProjectPath("TaskManager.sln", "", typeof(Startup).Assembly))
                .UseStartup<Startup>())
                .CreateClient();
            var model = new UpdateTaskViewModel { Description = "test001", Status = TaskStatus.NotDone, Tag = "test", Type = "test" };
            var result = client.PostAsJsonAsync("Task/Create", new { taskModel = model, userId = 1, userName = "chenmeiyi" }).Result;
            Assert.NotNull(result);
        }
    }
}
