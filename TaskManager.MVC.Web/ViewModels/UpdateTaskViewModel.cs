using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.MVC.Web.ViewModels
{
    public class UpdateTaskViewModel
    {
        public string Description { get; set; }

        public TaskStatus Status { get; set; }

        public string Tag { get; set; }

        public string Type { get; set; }
    }
}
