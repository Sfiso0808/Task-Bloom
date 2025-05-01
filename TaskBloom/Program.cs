using System;
using System.Collections.Generic;
using System.IO;
using TaskBloom.Models;
using TaskBloom.Services;

namespace TaskBloom
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskService taskService = new TaskService();

            Console.WriteLine("=== TaskBloom Console App ===");

            while (true)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Add new Task");
                Console.WriteLine("2. List all Tasks");
                Console.WriteLine("3. Mark Task as Complete");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Export All Tasks to CSV");
                Console.WriteLine("6. View Dashboard Summary");
                Console.WriteLine("0. Exit");
                Console.Write("Enter option: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddNewTask(taskService);
                        break;
                    case "2":
                        ListAllTasks(taskService);
                        break;
                    case "3":
                        MarkTaskComplete(taskService);
                        break;
                    case "4":
                        DeleteTask(taskService);
                        break;
                    case "5":
                        ExportTasksToCsv(taskService);
                        break;

                    case "6":
                        ShowDashboard(taskService);
                        break;


                    case "0":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        static void AddNewTask(TaskService taskService)
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            DateTime dueDate;
            while (true)
            {
                Console.Write("Enter Due Date (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out dueDate))
                {
                    break;
                }
                Console.WriteLine("Invalid date format. Please try again.");
            }

            int priority;
            while (true)
            {
                Console.Write("Enter Priority (1-5): ");
                if (int.TryParse(Console.ReadLine(), out priority) && priority >= 1 && priority <= 5)
                {
                    break;
                }
                Console.WriteLine("Priority must be a number between 1 and 5. 5 being Urgent");
            }

            TaskBloom.Models.Task task = new TaskBloom.Models.Task
            {
                Title = title,
                Description = description,
                DueDate = dueDate,
                Priority = priority,
                IsCompleted = false
            };

            taskService.AddTask(task);
            Console.WriteLine("Task added successfully!");
        }

        static void ListAllTasks(TaskService taskService)
        {
            List<TaskBloom.Models.Task> tasks = taskService.GetAllTasks();

            Console.WriteLine("\nFilter options:");
            Console.WriteLine("1. Show All Tasks");
            Console.WriteLine("2. Show Only Pending Tasks");
            Console.WriteLine("3. Show Only Completed Tasks");
            Console.Write("Choose filter (1-3): ");

            string filterChoice = Console.ReadLine();

            // Sort tasks by Due Date
            tasks.Sort((a, b) => a.DueDate.CompareTo(b.DueDate));

            Console.WriteLine("\n=== Filtered Tasks ===");

            foreach (var task in tasks)
            {
                bool display = filterChoice switch
                {
                    "1" => true,
                    "2" => !task.IsCompleted,
                    "3" => task.IsCompleted,
                    _ => true
                };

                if (display)
                {
                    string statusIcon = task.IsCompleted ? "[YES]" : "[NO]";

                    if (task.IsCompleted)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.WriteLine($"{statusIcon} ID: {task.Id} | Title: {task.Title} | Due: {task.DueDate:yyyy-MM-dd} | Priority: {task.Priority}");

                    Console.ResetColor();
                }
            }
        }

        static void MarkTaskComplete(TaskService taskService)
        {
            Console.Write("Enter Task ID to mark complete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var tasks = taskService.GetAllTasks();
                var task = tasks.Find(t => t.Id == id);

                if (task == null)
                {
                    Console.WriteLine("Task with that ID does not exist. Returning to main menu");
                    return;
                }

                if (task.IsCompleted)
                {
                    Console.WriteLine("Task has already been completed!");
                    return;
                }

                taskService.MarkComplete(id);
                Console.WriteLine("Task marked as completed!");

                // Log completed task to file
                LogCompletedTask(task);
            }
            else
            {
                Console.WriteLine("Invalid ID entered.");
            }
        }

        static void DeleteTask(TaskService taskService)
        {
            Console.Write("Enter Task ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var tasks = taskService.GetAllTasks();
                var task = tasks.Find(t => t.Id == id);

                if (task == null)
                {
                    Console.WriteLine("Task with that ID does not exist. Returning to main menu ");
                    return;
                }

                taskService.DeleteTask(id);
                Console.WriteLine("Task deleted successfully!");
            }
            else
            {
                Console.WriteLine("Invalid ID entered.");
            }
        }

        //dashboard
        static void ShowDashboard(TaskService taskService)
        {
            List<TaskBloom.Models.Task> tasks = taskService.GetAllTasks();

            int totalTasks = tasks.Count;
            int completedTasks = tasks.FindAll(t => t.IsCompleted).Count;
            int pendingTasks = totalTasks - completedTasks;

            Console.WriteLine("\n TaskBloom Dashboard ");
            Console.WriteLine($"Total Tasks: {totalTasks}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Completed Tasks: {completedTasks}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Pending Tasks: {pendingTasks}");
            Console.ResetColor();
        }




        static void ExportTasksToCsv(TaskService taskService)
{
    List<TaskBloom.Models.Task> tasks = taskService.GetAllTasks();

            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string filePath = Path.Combine(downloadsPath, "tasks_export.csv");

            using (var writer = new StreamWriter(filePath))
    {
        writer.WriteLine("Id,Title,Description,DueDate,Priority,IsCompleted");

        foreach (var task in tasks)
        {
            string line = $"{task.Id},\"{task.Title}\",\"{task.Description}\",{task.DueDate:yyyy-MM-dd},{task.Priority},{task.IsCompleted}";
            writer.WriteLine(line);
        }
    }

    Console.WriteLine($"Tasks exported successfully to {filePath}!");
}


        static void LogCompletedTask(TaskBloom.Models.Task task)
        {
            string logPath = "completed_log.txt";

            using (var writer = new StreamWriter(logPath, append: true))
            {
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | ID: {task.Id} | Title: {task.Title}");
            }
        }
    }
}
