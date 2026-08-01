using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastManagementApp
{
public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Priority {  get; set; }= string.Empty;

        public bool IsCompleted { get; set; } = false;
    }
}
