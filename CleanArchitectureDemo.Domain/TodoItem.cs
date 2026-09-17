namespace CleanArchitectureDemo.Domain;

public class TodoItem
    : ITodoItem
{
    public TodoItem(string title)
    {
        this.Title = title;
        this.IsCompleted = false;
    }

    public string Title { get; init; }
    public bool IsCompleted { get; set; } = false;
}