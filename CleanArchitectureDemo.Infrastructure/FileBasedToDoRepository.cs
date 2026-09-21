namespace CleanArchitectureDemo.Infrastructure;

using CleanArchitectureDemo.Domain;
using System.Text.Json;

public class FileBasedToDoRepository
    : ITodoRepository
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public FileBasedToDoRepository(string? filePath = null)
    {
        _filePath = filePath ?? Path.Combine(Environment.CurrentDirectory, "todos.txt");
    }

    public void CreateToDoItem(string title, bool IsCompleted = false)
    {
        var todos = LoadTodos();
        todos.Add(new TodoItem(title) { IsCompleted = IsCompleted });
        SaveTodos(todos);
    }

    public IEnumerable<ITodoItem> ListTodos()
    {
        return LoadTodos();
    }

    public void MarkTodoItemAsCompleted(int index)
    {
        var todos = LoadTodos();

        if (index < 0 || index >= todos.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
        }

        todos[index].MarkAsCompleted();
        SaveTodos(todos);
    }

    private List<TodoItem> LoadTodos()
    {
        if (!File.Exists(_filePath))
        {
            return new List<TodoItem>();
        }

        var contents = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<TodoItem>>(contents, _jsonOptions) ?? new List<TodoItem>();
    }

    private void SaveTodos(IEnumerable<TodoItem> todos)
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var contents = JsonSerializer.Serialize(todos, _jsonOptions);
        File.WriteAllText(_filePath, contents);
    }
}
