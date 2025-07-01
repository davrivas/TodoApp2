import { Component, OnInit } from '@angular/core';
import { TodoItemOutput } from './models/todo-item.model';
import { TodoItemService } from './services/todo-item.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App implements OnInit {
  title = 'Todo App';
  items: TodoItemOutput[] = [];

  constructor(private readonly todoItemService: TodoItemService) {}

  ngOnInit(): void {
    this.todoItemService.getAll().subscribe({
      next: (data: TodoItemOutput[]) => {
        this.items = data;
      },
      error: (error: any) => {
        console.error('Error fetching todo items:', error);
      }
    });
  }
}
