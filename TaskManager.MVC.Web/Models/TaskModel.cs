using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.MVC.Web.Models
{
    public enum TaskStatus
    {
        NotDone,
        InProgress,
        Completed
    }

    public class TaskModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int Owner { get; set; }

        public TaskStatus Status { get; set; }  

        public string Tag { get; set; }

        public string Type { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
