import { Component, OnInit } from '@angular/core';
import { TodoItemInput, TodoItemOutput } from './models/todo-item.model';
import { TodoItemService } from './services/todo-item.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App implements OnInit {
  title = 'Todo App';
  isLoading = true;
  items: TodoItemOutput[] = [];
  todoItemForm: FormGroup = new FormGroup({
    item: new FormControl('', [Validators.required, Validators.minLength(3)]),
    isCompleted: new FormControl(false)
  });
  private selectedItemId: string | null = null;

  constructor(private readonly todoItemService: TodoItemService) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.todoItemService.getAll().subscribe({
      next: (data: TodoItemOutput[]) => {
        this.items = data;
      },
      error: (error: any) => {
        console.error('Error fetching todo items:', error);
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }

  submitForm() {
    if (this.todoItemForm.invalid) {
      return;
    }

    if (this.selectedItemId) {
      this.updateItem();
    } else {
      this.createItem();
    }
  }

  createItem() {
    if (this.todoItemForm.invalid) {
      return;
    }

    const newItem: TodoItemInput = {
      item: this.todoItemForm.value.item,
      isCompleted: this.todoItemForm.value.isCompleted
    };

    this.isLoading = true;

    this.todoItemService.create(newItem).subscribe({
      next: (createdItem: TodoItemOutput) => {
        this.items.push(createdItem);
        this.todoItemForm.reset();
      },
      error: (error: any) => {
        console.error('Error creating todo item:', error);
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }

  updateForm(item: TodoItemOutput): void {
    this.todoItemForm.reset();

    this.selectedItemId = item.id;
    this.todoItemForm.setValue({
      item: item.item,
      isCompleted: item.isCompleted
    });
  }

  updateItem(): void {
    if (this.todoItemForm.invalid || !this.selectedItemId) {
      return;
    }

    const updatedItem: TodoItemInput = {
      item: this.todoItemForm.value.item,
      isCompleted: this.todoItemForm.value.isCompleted
    };

    this.isLoading = true;

    this.todoItemService.update(this.selectedItemId, updatedItem).subscribe({
      next: (updatedTodo: TodoItemOutput) => {
        const index = this.items.findIndex(item => item.id === this.selectedItemId);
        if (index !== -1) {
          this.items[index] = updatedTodo;
        }
        this.todoItemForm.reset();
        this.selectedItemId = null;
      },
      error: (error: any) => {
        console.error('Error updating todo item:', error);
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }

  deleteItem(id: string): void {    
    this.isLoading = true;
    this.todoItemService.delete(id).subscribe({
      next: () => {
        this.items = this.items.filter(item => item.id !== id);
      },
      error: (error: any) => {
        console.error('Error deleting todo item:', error);
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }
}
