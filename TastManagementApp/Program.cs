using System.ComponentModel;
using System.IO;
using System.Text.Json;
using TastManagementApp;

TaskService taskService = new TaskService();
bool IsRunning = true;

while (IsRunning)
{
    Console.WriteLine("\n--Task Manager--");
    Console.WriteLine("1. Adauga task nou.");
    Console.WriteLine("2. Vezi lista de task-uri.");
    Console.WriteLine("3. Marcheaza un task ca fiind finalizat");
    Console.WriteLine("4. Sterge un task");

    Console.WriteLine("0. Iesi din aplicatie.");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine("Introdu numele task-ului: ");
            string? Title = Console.ReadLine();
            Console.WriteLine("Ce prioritate are task-ul? (Scazuta, Medie, Ridicata");
            string? Priority = Console.ReadLine();
            Console.WriteLine("Descrierea task-ului: ");
            string? Description = Console.ReadLine();

            taskService.AddTask(Title ?? string.Empty, Priority ?? "Medie", Description ?? string.Empty);
            Console.WriteLine("Task adaugat cu succes");
            break;
        case "2":
            Console.WriteLine("--- Lista de taskuri ---");
            var listaTaskuri = taskService.GetAllTasks();

            if (listaTaskuri.Count == 0)
                Console.WriteLine("Nu exista task-uri! ");
            else
            {
                for (int i = 0; i < listaTaskuri.Count; i++)
                {
                    Console.WriteLine($"Task-ul {listaTaskuri[i].Id} \n Nume task: {listaTaskuri[i].Title} \n Prioritate: {listaTaskuri[i].Priority} \n Descriere: {listaTaskuri[i].Description}");
                    if (listaTaskuri[i].IsCompleted == false)
                        Console.WriteLine("\n Status: necompletat");
                    else
                        Console.WriteLine("\n Status: completat");
                }
            }
            break;

        case "3":
            Console.WriteLine("\n Care este Id-ul task-ului? :");
            int searchId = Convert.ToInt32(Console.ReadLine());
            if (taskService.GetAllTasks().Count == 0)
            {
                Console.WriteLine("Nu exista task-uri!");
                break;
            }
            Console.WriteLine($"Task-ul {searchId} este finalizat? (True/False)");
            string? completedChoice = Console.ReadLine();
            bool taskFound = taskService.CompleteTask(searchId, completedChoice ?? "false");

            if (taskFound)
            {
                Console.WriteLine("Statusul task-ului a fost modificat cu succes!");
            }
            else
            {
                Console.WriteLine($"Nu exista task-ul {searchId}");
            }
            break;

        case "4":
            if (taskService.GetAllTasks().Count == 0)
            {
                Console.WriteLine("Nu exista task-uri");
                break;
            }
            Console.WriteLine("Ce ID are task-ul pe care doriti sa il stergeti?");
            int deletedTask = Convert.ToInt32(Console.ReadLine());
            bool taskFinded = taskService.DeleteTask(deletedTask);
            if (taskFinded)
                Console.WriteLine($"Task-ul {deletedTask} a fost sters cu succes !");
            else
                Console.WriteLine($"Nu exista task-ul {deletedTask}");
            break;
        case "0":
            Console.WriteLine("Datele se salveaza..");
            taskService.SaveTasks();
            Console.WriteLine("Datele au fost salvate. La revedere !");
            IsRunning = false;
            break;
        default:
            Console.WriteLine("Eroare");

         break;
    }

}
    
