using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net.Http;
using TaskManager.MVC.Web;
using TaskManager.MVC.Web.Models;
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
            var result = client.GetStreamAsync("Account/Login").Result;
            var error = client.GetStreamAsync("Account/Login").Exception;
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
    }
}
