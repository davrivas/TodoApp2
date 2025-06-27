using AutoMapper;
using TodoApp.DataAccess.Entities;
using TodoApp.DataAccess.Repositories;
using TodoApp.Services.Models;

namespace TodoApp.Services
{
    public interface ITodoItemService
    {
        Task<List<TodoItemOutputModel>> GetAllAsync();
        Task<TodoItemOutputModel?> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(TodoItemInputModel todoItem);
        Task<TodoItemOutputModel> UpdateAsync(Guid id, TodoItemInputModel todoItem);
        Task DeleteAsync(Guid id);
    }

    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly IMapper _mapper;

        public TodoItemService(ITodoItemRepository todoItemRepository,
            IMapper mapper)
        {
            _todoItemRepository = todoItemRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(TodoItemInputModel todoItem)
        {
            ArgumentNullException.ThrowIfNull(todoItem);
            var todoItemEntity = _mapper.Map<TodoItem>(todoItem);
            return await _todoItemRepository.AddAsync(todoItemEntity);
        }

        public async Task DeleteAsync(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id);
            await _todoItemRepository.DeleteAsync(id);
        }

        public async Task<List<TodoItemOutputModel>> GetAllAsync()
        {
            var todoItems = await _todoItemRepository.GetAllAsync();
            return _mapper.Map<List<TodoItemOutputModel>>(todoItems);
        }

        public async Task<TodoItemOutputModel?> GetByIdAsync(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id);
            var todoItem = await _todoItemRepository.GetByIdAsync(id);
            if (todoItem == null) return null;
            return _mapper.Map<TodoItemOutputModel>(todoItem);
        }

        public async Task<TodoItemOutputModel> UpdateAsync(Guid id, TodoItemInputModel todoItem)
        {
            ArgumentNullException.ThrowIfNull(id);
            ArgumentNullException.ThrowIfNull(todoItem);
            var todoItemEntity = _mapper.Map<TodoItem>(todoItem);
            await _todoItemRepository.UpdateAsync(id, todoItemEntity);
            return new TodoItemOutputModel
            {
                Id = id,
                Item = todoItem.Item,
                IsCompleted = todoItem.IsCompleted
            };
        }
    }
}
