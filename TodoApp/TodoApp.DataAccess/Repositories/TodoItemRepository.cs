using Microsoft.EntityFrameworkCore;
using TodoApp.DataAccess.Contexts;
using TodoApp.DataAccess.Entities;

namespace TodoApp.DataAccess.Repositories
{
    public interface ITodoItemRepository
    {
        Task<List<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(TodoItem todoItem);
        Task UpdateAsync(Guid id, TodoItem todoItem);
        Task DeleteAsync(Guid id);
    }

    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TodoItemRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddAsync(TodoItem todoItem)
        {
            ArgumentNullException.ThrowIfNull(todoItem);
            await _dbContext.TodoItems.AddAsync(todoItem);
            await _dbContext.SaveChangesAsync();
            return todoItem.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var todoItem = await _dbContext.TodoItems.FindAsync(id)
                ?? throw new KeyNotFoundException($"TodoItem with ID {id} not found.");
            _dbContext.TodoItems.Remove(todoItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return await _dbContext.TodoItems.ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(Guid id)
        {
            var todoItem = await _dbContext.TodoItems.FindAsync(id);
            return todoItem ?? throw new KeyNotFoundException($"TodoItem with ID {id} not found.");
        }

        public async Task UpdateAsync(Guid id, TodoItem todoItem)
        {
            ArgumentNullException.ThrowIfNull(todoItem);
            var existingTodoItem = await _dbContext.TodoItems.FindAsync(id)
                ?? throw new KeyNotFoundException($"TodoItem with ID {id} not found.");
            existingTodoItem.Item = todoItem.Item;
            existingTodoItem.IsCompleted = todoItem.IsCompleted;
            _dbContext.TodoItems.Update(existingTodoItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}
