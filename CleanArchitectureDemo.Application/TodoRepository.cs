namespace CleanArchitectureDemo.Application;

using CleanArchitectureDemo.Domain;

public class TodoRepository
    : ITodoRepository
{
    private List<ITodoItem> _repository = new();

    public void CreateToDoItem(string title, bool IsCompleted = false)
    {
        var todo = new TodoItem(title);
        _repository.Add(todo);
    }

    public IEnumerable<ITodoItem> ListTodos()
    {
        return _repository;
    }
}
