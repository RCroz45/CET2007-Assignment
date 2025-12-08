using System;
using System.Threading.Tasks;

namespace CET2007_Assignment
{

    internal class Program
    {


        static void Main(string[] args)
        {

            TaskManager manager = new TaskManager();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=== Task & Project Tracker ===");
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. View all tasks");
                Console.WriteLine("3. Search task by ID");
                Console.WriteLine("4. Sort tasks by due date");
                Console.WriteLine("5. Sort tasks by priority");
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
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }


            TaskItem task = new TaskItem();
            {
                task.Id = 1;
                task.Title = "test task";
                task.Assignee = "Alice";
                task.DueDate = DateTime.Today.AddDays(3);
                task.Priority = PriorityLevel.Low;
                task.Status = TaskStatus.ToDo;



            };

            TaskItem task2 = new TaskItem();
            {
                task2.Id = 2;
                task2.Title = "test task 2";
                task2.Assignee = "Bob";
                task2.DueDate = DateTime.Today.AddDays(1);
                task2.Priority = PriorityLevel.Medium;
                task2.Status = TaskStatus.ToDo;
                manager.AddTask(task2);
            }

            TaskItem task3 = new TaskItem();
            {
                task3.Id = 3;
                task3.Title = "test task 3";
                task3.Assignee = "Charlie";
                task3.DueDate = DateTime.Today.AddDays(7);
                task3.Priority = PriorityLevel.High;
                task3.Status = TaskStatus.ToDo;
                manager.AddTask(task3);
            }

            bool added = manager.AddTask(task);

            if (added)
            {
                Console.WriteLine("task added succesfully");

            }
            else
            {
                Console.WriteLine("failed");
            }


            Console.WriteLine("\nAll tasks: ");
            foreach (var t in manager.GetAllTasks())
            {
                Console.WriteLine($"{t.Id} - {t.Title} - {t.Assignee}");
            }

            Console.ReadLine();

            Console.WriteLine("\nSearch test:");
            TaskItem found = manager.GetTaskById(1);

            if (found != null)
            {
                Console.WriteLine($"Found Task: {found.Title}");
            }
            else
            {
                Console.WriteLine("task not found");
            }
            Console.ReadLine();

            manager.SortByDueDate();

            Console.WriteLine("\nAll tasks (Sorted by due date): ");
            foreach (var t in manager.GetAllTasks())
            {
                Console.WriteLine($"{t.Id} - {t.Title} - {t.Assignee} - due: {t.DueDate:d} - Priority: {t.Priority}");
            }
            Console.ReadLine();

            manager.SortByPriority();

            Console.WriteLine("\nAll tasks (Sorted by priority): ");
            foreach (var t in manager.GetAllTasks())
            {
                Console.WriteLine($"{t.Id} - {t.Title} - {t.Assignee} - due: {t.DueDate:d} - Priority: {t.Priority}");
            }
            Console.ReadLine();
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
                    Console.WriteLine("Task added successfully.");
                }
                else
                {
                    Console.WriteLine("Could not add task. Maybe that ID already exists?");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating task: " + ex.Message);
            }


        }
        static void ShowAllTasks(TaskManager manager)
        {
            var all = manager.GetAllTasks();

            if (all.Count == 0)
            {
                Console.WriteLine("No tasks yet.");
                return;
            }

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

    }



}
