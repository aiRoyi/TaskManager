using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.MVC.Web.Models;

namespace TaskManager.MVC.Web.Repositories
{
    public interface ITaskRepository
    {
        TaskModel FindOne(int Id);

        List<TaskModel> FindAll();

        void AddOrUpate(TaskModel model);

        void Delete(TaskModel model);

        List<TaskModel> FindAllByType(string type);
    }

    public class TaskRepository : ITaskRepository
    {
        private readonly TaskContext _dbContext;

        public TaskRepository(TaskContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddOrUpate(TaskModel model)
        {
            try
            {
                if (model.Id != 0)
                    _dbContext.Update(model);
                else
                    _dbContext.Add(model);

                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void Delete(TaskModel model)
        {
            _dbContext.Remove(model);
            _dbContext.SaveChanges();
        }

        public List<TaskModel> FindAll()
        {
            try
            {
                return _dbContext.Tasks.Where(task => task.Id != 0).ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public List<TaskModel> FindAllByType(string type)
        {
            try
            {
                return _dbContext.Tasks.Where(task => task.Type.ToString() == type).ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public TaskModel FindOne(int Id)
        {
            try
            {
                return _dbContext.Tasks.Single(task => task.Id == Id);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
    }
}
