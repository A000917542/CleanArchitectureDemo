namespace CleanArchitectureDemo.Domain;

public interface ITodoItem
{
    public string Title { get; init; }
    public bool IsCompleted { get; set; }
}
