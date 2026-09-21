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

    public void MarkTodoItemAsCompleted(int index)
    {
        if (index < 0 || index >= _repository.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
        }

        var todoItem = _repository[index];
        todoItem.MarkAsCompleted();
    }
}
