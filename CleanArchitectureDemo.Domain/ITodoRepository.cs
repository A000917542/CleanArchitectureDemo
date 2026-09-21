namespace CleanArchitectureDemo.Domain;

public interface ITodoRepository
{
    public void CreateToDoItem(string title, bool IsCompleted = false);

    // TODO: Create a Repository Read Method

    // TODO: Create a Repository Update Method

    // TODO: Create a Repository Delete Method

    public IEnumerable<ITodoItem> ListTodos();

    public void MarkTodoItemAsCompleted(int index);
}
