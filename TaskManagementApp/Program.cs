using System.ComponentModel;
using System.IO;
using System.Text.Json;
using TaskManagementApp;

TaskServices taskService = new TaskServices();
bool IsRunning = true;

while (IsRunning)
{
    Console.WriteLine("\n--Task Manager--");
    Console.WriteLine("1. Add a new task.");
    Console.WriteLine("2. View all tasks");
    Console.WriteLine("3. Mark a task as completed");
    Console.WriteLine("4. Delete a task");

    Console.WriteLine("0. Quit.");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine("Enter the task name: ");
            string? Title = Console.ReadLine();
            Console.WriteLine("Enter the task priority: (low, medium, high");
            string? Priority = Console.ReadLine();
            Console.WriteLine("Task description: ");
            string? Description = Console.ReadLine();

            taskService.AddTask(Title ?? string.Empty, Priority ?? "Medium", Description ?? string.Empty);
            Console.WriteLine("Task successfully added.");
            break;
        case "2":
            Console.WriteLine("--- Task List ---");
            var listaTaskuri = taskService.GetAllTasks();

            if (listaTaskuri.Count == 0)
                Console.WriteLine("There are no tasks! ");
            else
            {
                for (int i = 0; i < listaTaskuri.Count; i++)
                {
                    Console.WriteLine($"Task {listaTaskuri[i].Id} \n Task Name: {listaTaskuri[i].Title} \n Priority: {listaTaskuri[i].Priority} \n Description: {listaTaskuri[i].Description}");
                    if (listaTaskuri[i].IsCompleted == false)
                        Console.WriteLine("\n Status: unfinished");
                    else
                        Console.WriteLine("\n Status: finished");
                }
            }
            break;

        case "3":
            Console.WriteLine("\n What is the task ID? :");
            int searchId = Convert.ToInt32(Console.ReadLine());
            if (taskService.GetAllTasks().Count == 0)
            {
                Console.WriteLine("There are no tasks!");
                break;
            }
            Console.WriteLine($"Is the task {searchId} finished? (True/False)");
            string? completedChoice = Console.ReadLine();
            bool taskFound = taskService.CompleteTask(searchId, completedChoice ?? "false");

            if (taskFound)
            {
                Console.WriteLine("The task status has been successfully updated!");
            }
            else
            {
                Console.WriteLine($"There is no task {searchId}");
            }
            break;

        case "4":
            if (taskService.GetAllTasks().Count == 0)
            {
                Console.WriteLine("There are no tasks!");
                break;
            }
            Console.WriteLine("What is the ID of the task you want to delete?");
            int deletedTask = Convert.ToInt32(Console.ReadLine());
            bool taskFinded = taskService.DeleteTask(deletedTask);
            if (taskFinded)
                Console.WriteLine($"Task {deletedTask} was successfully deleted!");
            else
                Console.WriteLine($"There is no task {deletedTask}");
            break;
        case "0":
            Console.WriteLine("The data is being saved.....");
            taskService.SaveTasks();
            Console.WriteLine("The data has been saved. Goodbye!");
            IsRunning = false;
            break;
        default:
            Console.WriteLine("Error");

         break;
    }

}
    
