import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskService } from './task';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule], 
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  

  tasks: any[] = []; 

constructor(private taskService: TaskService, private cdr: ChangeDetectorRef) {}

  ngOnInit() {
    this.loadTasks();
  }


  loadTasks() {
    this.taskService.getTasks().subscribe({
      next: (datesFromApi: any) => {
        this.tasks = datesFromApi; 
        console.log('The tasks have been successfully retrieved:', this.tasks);
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error('Error:', error);
      }
    });
  }

  addTask(newTitle:string, priority: string, description: string)
  {
   if(newTitle==='')
   {
    return;
   }

   this.taskService.AddTask(newTitle,priority,description).subscribe({
    next:(answer)=>
    {
      console.log('Succes: ',answer);
      this.loadTasks();
    },
    error:(error)=>
    {
      console.error('Error',error);
    }
   });

  }
}