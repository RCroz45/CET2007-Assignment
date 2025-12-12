using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CET2007_Assignment
{
    public class TaskManager
    {
        private readonly string dataFilePath = "tasks.txt";
        // this will hold the tasks in memory
        private List<TaskItem> tasks = new List<TaskItem>();


        // this will return all the tasks to display
        public List<TaskItem> GetAllTasks()
        {
            return tasks;  
        }

        // this is the add task method
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
            // this will log the added task
            LogAction($"Created task (ID: {task.Id}, Title: {task.Title})");
            return true;
            



        }

        // this is the method for searching tasks by ID
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

        // this is the method for sorting by due date
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

        // this is the method for sorting by priority
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

        public bool UpdateTaskStatus(int id, TaskStatus newStatus)
        {
            TaskItem task = GetTaskById(id);

            if (task == null)
            {
                return false;
            }

            task.Status = newStatus;
            LogAction($"Updated task {id} status: {newStatus}");
            return true;
        
        }

        public void SaveTasksToFile()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(dataFilePath))
                {
                    foreach (TaskItem t in tasks)
                    {
                        string line = string.Join("|", t.Id, t.Title.Replace("|", "/"), t.Assignee.Replace("|", "/"), t.DueDate.ToString("o"), (int)t.Priority, (int)t.Status);

                        sw.WriteLine(line);
                    }

                }

                Console.WriteLine("Tasks saved to file");
                LogAction("Saved all tasks to file.");
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error saving tasks: " + ex.Message);
            }
        
            
        }

        public void LoadTasksFromFile()
        {
            try
            {
                if (!File.Exists(dataFilePath))
                {
                    
                    return;
                }

                string[] lines = File.ReadAllLines(dataFilePath);
                tasks.Clear();

                int loadedCount = 0;

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split('|');
                    if (parts.Length != 6)
                        continue; 

                    int id = int.Parse(parts[0]);
                    string title = parts[1];
                    string assignee = parts[2];
                    DateTime dueDate = DateTime.Parse(parts[3]);
                    PriorityLevel priority = (PriorityLevel)int.Parse(parts[4]);
                    TaskStatus status = (TaskStatus)int.Parse(parts[5]);

                    TaskItem task = new TaskItem();
                    task.Id = id;
                    task.Title = title;
                    task.Assignee = assignee;
                    task.DueDate = dueDate;
                    task.Priority = priority;
                    task.Status = status;

                    tasks.Add(task);
                    loadedCount++; 
                }

                Console.WriteLine("Tasks loaded from file.");
                LogAction($"Loaded {loadedCount} tasks from file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading tasks: " + ex.Message);
            }
        }

        private void LogAction(string message)
        {
            string logPath = "log.txt";
            string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";

            File.AppendAllText(logPath, logEntry + Environment.NewLine);
        }




    }


}
