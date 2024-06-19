using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Task
{
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public string Project { get; set; }
}

class Program
{
    static List<Task> tasks = new List<Task>();  // List to store tasks

    static void Main(string[] args)
    {
        LoadTasks();  // Load tasks from file when the program starts

        bool running = true;
        while (running)
        {
            // Display menu options
            Console.WriteLine("\nTODO List Menu:");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Remove Task");
            Console.WriteLine("3. Sort by Date");
            Console.WriteLine("4. Sort by Project");
            Console.WriteLine("5. Show Tasks");
            Console.WriteLine("6. Save and Exit");

            Console.Write("\nEnter choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddTask();
                    break;
                case 2:
                    RemoveTask();
                    break;
                case 3:
                    tasks = tasks.OrderBy(t => t.DueDate).ToList();  // Sort tasks by due date
                    break;
                case 4:
                    tasks = tasks.OrderBy(t => t.Project).ToList();   // Sort tasks by project name
                    break;
                case 5:
                    ShowTasks();
                    break;
                case 6:
                    SaveTasks();  // Save tasks to file and exit
                    running = false;
                    break;
            }
        }
    }

    // Method to add a new task
    static void AddTask()
    {
        Task task = new Task();
        Console.Write("Enter task description: ");
        task.Description = Console.ReadLine();
        Console.Write("Enter due date (MM/DD/YYYY): ");
        task.DueDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter project: ");
        task.Project = Console.ReadLine();

        tasks.Add(task);
    }

    // Method to remove a task
    static void RemoveTask()
    {
        Console.WriteLine("Select task to remove:");
        for (int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {tasks[i].Description}");
        }

        int index = int.Parse(Console.ReadLine()) - 1;
        tasks.RemoveAt(index);
    }

    // Method to display all tasks
    static void ShowTasks()
    {
        Console.WriteLine("\nTasks:");
        foreach (var task in tasks)
        {
            Console.WriteLine($"Description: {task.Description}\tDue Date: {task.DueDate.ToShortDateString()}\tProject: {task.Project}");
        }
    }

    // Method to save tasks to file
    static void SaveTasks()
    {
        using (StreamWriter writer = new StreamWriter("tasks.txt"))
        {
            foreach (var task in tasks)
            {
                writer.WriteLine($"{task.Description},{task.DueDate},{task.Project}");
            }
        }
    }

    // Method to load tasks from file
    static void LoadTasks()
    {
        if (File.Exists("tasks.txt"))
        {
            using (StreamReader reader = new StreamReader("tasks.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    Task task = new Task
                    {
                        Description = data[0],
                        DueDate = DateTime.Parse(data[1]),
                        Project = data[2]
                    };
                    tasks.Add(task);
                }
            }
        }
    }
}