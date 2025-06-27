namespace TodoApp.Services.Models
{
    public class TodoItemInputModel
    {
        public string Item { get; set; } = null!;
        public bool IsCompleted { get; set; }
    }
}
