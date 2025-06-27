namespace TodoApp.DataAccess.Entities
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Item { get; set; } = null!;
        public bool IsCompleted { get; set; }
    }
}
