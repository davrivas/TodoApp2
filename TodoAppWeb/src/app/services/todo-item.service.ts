import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TodoItemInput, TodoItemOutput } from '../models/todo-item.model';
import { Observable } from 'rxjs';

@Injectable({providedIn: 'root'})
export class TodoItemService {
    private readonly baseUrl = 'https://localhost:7069/api/TodoItem';

    constructor(private readonly httpClient: HttpClient) { }    

    getAll(): Observable<TodoItemOutput[]> {
        return this.httpClient.get<TodoItemOutput[]>(this.baseUrl);
    }

    getById(id: string): Observable<TodoItemOutput> {
        return this.httpClient.get<TodoItemOutput>(`${this.baseUrl}/${id}`);
    }

    create(item: TodoItemInput): Observable<TodoItemOutput> {
        return this.httpClient.post<TodoItemOutput>(this.baseUrl, item);
    }

    update(id: string, item: TodoItemInput): Observable<TodoItemOutput> {
        return this.httpClient.put<TodoItemOutput>(`${this.baseUrl}/${id}`, item);
    }

    delete(id: string): Observable<void> {
        return this.httpClient.delete<void>(`${this.baseUrl}/${id}`);
    }
}