import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { HttpParams } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class TaskService{
    private apiUrl = 'https://localhost:7062/api/tasks';
    constructor(private http: HttpClient){}

    getTasks()
    {
        return this.http.get(this.apiUrl);  
    }

    AddTask(title:string, priority:string, description:string)
    {
        const params = new HttpParams()
        .set('title',title)
        .set('priority',priority)
        .set('description',description);

      return this.http.post('https://localhost:7062/api/tasks', null, { 
    params: params, 
    responseType: 'text' as 'json' 
  });
    }
}