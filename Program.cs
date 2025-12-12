using System;
using System.Threading.Tasks;

namespace CET2007_Assignment
{

    internal class Program
    {


        static void Main(string[] args)
        {

            TaskManager manager = new TaskManager();
            manager.LoadTasksFromFile();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=== Task & Project Tracker ===");
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. View all tasks");
                Console.WriteLine("3. Search task by ID");
                Console.WriteLine("4. Sort tasks by due date");
                Console.WriteLine("5. Sort tasks by priority");
                Console.WriteLine("6. Update task status");
                Console.WriteLine("7. Save tasks to file");
                Console.WriteLine("8. Load tasks from file");
                Console.WriteLine("9. Generate report");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTask(manager);
                        break;
                    case "2":
                        ShowAllTasks(manager);
                        break;
                    case "3":
                        SearchTaskById(manager);
                        break;
                    case "4":
                        manager.SortByDueDate();
                        Console.WriteLine("Tasks sorted by due date.");
                        break;
                    case "5":
                        manager.SortByPriority();
                        Console.WriteLine("Tasks sorted by priority.");
                        break;
                    case "6":
                        UpdateTaskStatus(manager);
                        break;
                    case "7":
                        manager.SaveTasksToFile();
                        break;
                    case "8":
                        manager.LoadTasksFromFile();
                        break;
                    case "9":
                        manager.GenerateReport();
                        break;
                    case "0":
                        manager.SaveTasksToFile();
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }

        }

        static void AddTask(TaskManager manager)
        {
            try
            {
                Console.Write("Enter task ID (number): ");
                int id = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter title: ");
                string title = Console.ReadLine();

                Console.Write("Enter assignee: ");
                string assignee = Console.ReadLine();

                Console.Write("Enter due date (dd/MM/yyyy): ");
                DateTime dueDate = DateTime.ParseExact(
                    Console.ReadLine() ?? "",
                    "dd/MM/yyyy",
                    null
                );

                Console.Write("Enter priority (Low, Medium, High): ");
                string prioText = Console.ReadLine();
                PriorityLevel priority =
                    (PriorityLevel)Enum.Parse(typeof(PriorityLevel), prioText, true);

                TaskItem task = new TaskItem();
                task.Id = id;
                task.Title = title;
                task.Assignee = assignee;
                task.DueDate = dueDate;
                task.Priority = priority;
                task.Status = TaskStatus.ToDo;

                bool added = manager.AddTask(task);

                if (added)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("Task added successfully.");
                    Console.WriteLine(" ");
                }
                else
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("Could not add task. Maybe that ID already exists?");
                    Console.WriteLine(" ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" ");
                Console.WriteLine("Error creating task: " + ex.Message);
                Console.WriteLine(" ");
            }


        }
        static void ShowAllTasks(TaskManager manager)
        {
            var all = manager.GetAllTasks();

            if (all.Count == 0)
            {
                Console.WriteLine(" ");
                Console.WriteLine("No tasks yet.");
                return;
            }
            
            Console.WriteLine(" ");
            Console.WriteLine("\n=== All tasks ===");
            foreach (TaskItem t in all)
            {
                Console.WriteLine($"{t.Id} - {t.Title} - {t.Assignee} - Due: {t.DueDate:d} - {t.Priority} - {t.Status}");
            }
        }

        static void SearchTaskById(TaskManager manager)
        {
            Console.Write("Enter task ID to search: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            TaskItem found = manager.GetTaskById(id);

            if (found == null)
            {
                Console.WriteLine("Task not found.");
            }
            else
            {
                Console.WriteLine("Task found:");
                Console.WriteLine($"{found.Id} - {found.Title} - {found.Assignee} - Due: {found.DueDate:d} - {found.Priority} - {found.Status}");
            }
        }

        static void UpdateTaskStatus(TaskManager manager)
        {
            Console.Write("Enter task ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            Console.Write("Enter new status (ToDo, InProgress, Done): ");
            string statusText = Console.ReadLine();

            try
            {
                TaskStatus newStatus = (TaskStatus)Enum.Parse(typeof(TaskStatus), statusText, true);

                bool updated = manager.UpdateTaskStatus(id, newStatus);

                if (updated)
                {
                    Console.WriteLine("Task status updated successfully.");
                }
                else
                {
                    Console.WriteLine("Task not found.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid status entered.");
            }
        }

    }



}
