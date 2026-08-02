using System.IO;
using System.Text.Json;

using TastManagementApp;

List<TaskItem> tasks = new List<TaskItem>();

int nextId = 1;
string file = "tasks.json";

if(File.Exists(file))
{
    string savedText = File.ReadAllText(file);
    tasks = JsonSerializer.Deserialize<List<TaskItem>>(savedText);

    if (tasks.Count > 0)
    {
        int maxId = 0;
        for (int i = 0; i < tasks.Count; i++)
        {
            if (maxId > tasks[i].Id) { maxId = i; }
        }
        nextId = maxId + 1;
    }
}

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

    switch(choice)
    {
        case "1":
            Console.WriteLine("Introdu numele task-ului: ");
            string? Title = Console.ReadLine();
            Console.WriteLine("Ce prioritate are task-ul? (Scazuta, Medie, Ridicata");
            string? Priority= Console.ReadLine();
            TaskItem newTask = new TaskItem()
            {
                Id = nextId,
                Title = Title ?? string.Empty,
                Priority = Priority ?? "Normala",
                IsCompleted = false,
            };
           tasks.Add(newTask);
            nextId++;
            Console.WriteLine("Task adaugat cu succes");
            break;
        case "2":
            Console.WriteLine("Lista de taskuri");
            if(tasks.Count == 0)
            {
                Console.WriteLine("Nu exista task-uri");
                break;
            }
            else
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"\nTask-ul {tasks[i].Id} \n Nume task: {tasks[i].Title} \n Prioritatea: {tasks[i].Priority}  ");

                    if (tasks[i].IsCompleted == false)
                    {
                        Console.WriteLine("\n Status: necompletat");
                    }
                    else
                    {
                        Console.WriteLine("\n Status: completat");
                    }
                }
            }
                break;
        case "3":
            Console.WriteLine("\n Care este Id-ul task-ului? :");
          int searchId =  Convert.ToInt32(Console.ReadLine());

            if (tasks.Count == 0)
            {
                Console.WriteLine("Nu exista task-uri");
                break;
            }
            else if (searchId > tasks.Count())
            {
                Console.WriteLine($"Nu exista task-ul {searchId}");
            }
            else
            {
                Console.WriteLine($"Task-ul {searchId} este finalizat? (True/false)");
                string? completedChoice = Console.ReadLine();
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (tasks[i].Id == searchId)
                    {

                        if (completedChoice == "true")
                            tasks[i].IsCompleted = true;
                        else if (completedChoice == "false")
                            tasks[i].IsCompleted = false;
                    }
                }
                Console.WriteLine("Statusul task-ului a fost modificat cu succes!");
            }
           
            break;

            case "4":
            if (tasks.Count == 0)
            {
                Console.WriteLine("Nu exista task-uri.");
                break;
            }
            Console.WriteLine("Ce id are task-ul pe care doriti sa il stergeti ? ");
            int deletedTask = Convert.ToInt32(Console.ReadLine());

            bool taskFinded = false;
            for(int i =0; i < tasks.Count;i++)
            {
                
                if (tasks[i].Id == deletedTask)
                {
                    tasks.Remove(tasks[i]);
                    Console.WriteLine($"Task-ul {deletedTask} a fost sters cu succes!");
                    taskFinded = true;
                    break;
                }
            }
            if (taskFinded == false)
            {
                Console.WriteLine($"Nu exista task-ul {deletedTask}");
            }
            break;
        case "0":
            Console.WriteLine("Datele se salveaza..");
            string jsonString =JsonSerializer.Serialize(tasks);
            File.WriteAllText("tasks.json", jsonString);
            Console.WriteLine("Datele au fost salvate. La revedere");
            IsRunning = false;
            break;
        default: 
            Console.WriteLine("Eroare");
           
            break;
    }

}
