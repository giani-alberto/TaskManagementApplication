

using TastManagementApp;

List<TaskItem> tasks = new List<TaskItem>();
bool IsRunning = true;
while(IsRunning)
{
    Console.WriteLine("\n--Task Manager--");
    Console.WriteLine("1. Adauga task nou.");
    Console.WriteLine("2. Vezi lista de task-uri.");
    Console.WriteLine("3. Marcheaza un task ca fiind finalizat");
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
                Id = tasks.Count + 1,
                Title = Title ?? string.Empty,
                Priority = Priority ?? "Normala",
                IsCompleted = false,
            };
           tasks.Add(newTask);
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
                    Console.WriteLine($"\nTask-ul {tasks[i].Id} \n Nume task: {tasks[i].Title} \n Prioritatea: {tasks[i].Priority} \n Status: ");

                    if (tasks[i].IsCompleted == false)
                    {
                        Console.WriteLine("necompletat");
                    }
                    else
                    {
                        Console.WriteLine("completat");
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
            }
            Console.WriteLine("Statusul task-ului a fost modificat cu succes!");
            break;
        case "0":
            Console.WriteLine("La revedere");
            IsRunning = false;
            break;
        default: 
            Console.WriteLine("Eroare");
           
            break;
    }

}
