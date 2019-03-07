using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.MVC.Web.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TaskContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<TaskContext>>()))
            {
                if (!context.Users.Any())
                {
                    context.Users.AddRange(new UserModel
                    {
                        UserName = "chenmeiyi",
                        Password = "cmy123456"
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
