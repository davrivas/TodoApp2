namespace TodoApp.Services.Models
{
    public class TodoItemOutputModel
    {
        public Guid Id { get; set; }
        public string Item { get; set; } = null!;
        public bool IsCompleted { get; set; }
    }
}
