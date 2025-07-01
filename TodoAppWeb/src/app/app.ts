import { Component, OnInit } from '@angular/core';
import { TodoItemOutput } from './models/todo-item.model';
import { TodoItemService } from './services/todo-item.service';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App implements OnInit {
  title = 'Todo App';
  items: TodoItemOutput[] = [];
  // todoItemForm: FormBuilder = new FormBuilder();

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

  create() {
    //
  }

  updateForm(item: TodoItemOutput): void {
    //
  }

  deleteItem(id: string): void {
    this.todoItemService.delete(id).subscribe({
      next: () => {
        this.items = this.items.filter(item => item.id !== id);
      },
      error: (error: any) => {
        console.error('Error deleting todo item:', error);
      }
    });
  }
}
