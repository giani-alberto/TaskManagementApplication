using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace TaskManagementAPI
{
    public class TaskServices
    {
        List<TaskItem> tasks = new List<TaskItem>();
        int nextId = 1;
        string file = "tasks.json";

        public TaskServices()
        {
            if (File.Exists(file))
            {
                string savedText = File.ReadAllText(file);
                tasks = JsonSerializer.Deserialize<List<TaskItem>>(savedText);

                if (tasks.Count > 0)
                {
                    int maxId = 0;
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        if (tasks[i].Id > maxId)
                        {
                            maxId = tasks[i].Id;
                        }
                    }
                    nextId = maxId + 1;
                }
            }
        }

        public void AddTask(string title,string priority, string description)
        {
            TaskItem newTask = new TaskItem()
            {
                Id = nextId,
                Title = title,
                Priority = priority,
                Description = description,
                IsCompleted = false,
            };
            tasks.Add(newTask);
            nextId++;
        }

        public List<TaskItem> GetAllTasks()
        {
            return tasks;
        }

        public bool CompleteTask(int id, string completedChoice)
        {
            for(int i=0; i<tasks.Count; i++)
            {
                if(tasks[i].Id == id)
                {
                    if (completedChoice == "true") tasks[i].IsCompleted = true;
                    else if(completedChoice=="false") tasks[i].IsCompleted = false;
                    return true;
                }

            }
            return false;
        }
        
        public bool DeleteTask(int id)
        {
            for(int i=0;i<tasks.Count;i++)
            {
                if (tasks[i].Id == id)
                {
                    tasks.Remove(tasks[i]);
                    return true;
                }
            }
            return false;
        }

        public void SaveTasks()
        {
            string jsonString = JsonSerializer.Serialize(tasks);
            File.WriteAllText(file, jsonString);
        }
    }
    
}
