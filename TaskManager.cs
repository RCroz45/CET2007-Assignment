using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CET2007_Assignment
{
    public class TaskManager
    {
        // this will hold the tasks in memory
        private List<TaskItem> tasks = new List<TaskItem>();

        // this will return all the tasks to display
        public List<TaskItem> GetAllTasks()
        {
            return tasks;  
        }

        public bool AddTask(TaskItem task)
        {
            if (task == null)
            {
                return false;
            }

            foreach (var t in tasks) 
            {
                if (t.Id == task.Id)
                {
                    return false;
                }
            }



            tasks.Add(task);
            return true;



        }

        public TaskItem GetTaskById(int id)
        {
            foreach (var task in tasks)
            {
                if (task.Id == id)
                {
                    return task;
                }
            }

            return null;
        }

        public void SortByDueDate()
        {
            for (int i = 0; i < tasks.Count - 1; i++)
            {
                for (int j = 0; j < tasks.Count - 1 - i; j++)
                {
                    if (tasks[j].DueDate > tasks[j +1].DueDate)
                    {
                        TaskItem temp = tasks[j];
                        tasks[j] = tasks[j + 1];
                        tasks[j + 1] = temp;
                    }
                }
            }
        }

        public void SortByPriority()
        {
            for (int i = 0; i < tasks.Count - 1; i++)
            {
                for (int j = 0; j < tasks.Count - 1 - i; j++)
                {
                    if (tasks[j].Priority < tasks[j + 1].Priority)
                    {
                        TaskItem temp = tasks[j];
                        tasks[j] = tasks[j + 1];
                        tasks[j + 1] = temp;

                    }
                }
            }
        
        }
    
    
    }





}
