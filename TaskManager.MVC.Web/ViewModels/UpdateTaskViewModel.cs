using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.MVC.Web.Models;

namespace TaskManager.MVC.Web.ViewModels
{
    public class UpdateTaskViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public TaskStatus Status { get; set; }

        public string Tag { get; set; }

        public string Type { get; set; }
    }
}
