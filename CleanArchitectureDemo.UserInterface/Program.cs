using CleanArchitectureDemo.Application;
using CleanArchitectureDemo.Domain;
using CleanArchitectureDemo.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

if (args[0] == "File")
{
	builder.Services.AddSingleton<ITodoRepository, FileBasedToDoRepository>();
}
else if(args[0] == "Memory")
{
	builder.Services.AddSingleton<ITodoRepository, TodoRepository>();
}
else
{
	builder.Services.AddSingleton<ITodoRepository, TodoRepository>();
}


using IHost host = builder.Build();

ITodoRepository todoRepository = host.Services.GetRequiredService<ITodoRepository>();
var isRunning = true;

while (isRunning)
{
	Console.WriteLine();
	Console.WriteLine("Todo Menu");
	Console.WriteLine("1. Add todo item");
	Console.WriteLine("2. List todo items");
	Console.WriteLine("3. Mark todo item as complete");
	Console.WriteLine("0. Exit");
	Console.Write("Choose an option: ");

	var choice = Console.ReadLine();

	switch (choice)
	{
		case "1":
			Console.Write("Enter a todo item: ");
			var title = Console.ReadLine();

			if (!string.IsNullOrWhiteSpace(title))
			{
				todoRepository.CreateToDoItem(title.Trim());
				Console.WriteLine("Todo item added.");
			}
			else
			{
				Console.WriteLine("Todo item cannot be empty.");
			}

			break;

		case "2":
			var todos = todoRepository.ListTodos().ToList();

			if (todos.Count == 0)
			{
				Console.WriteLine("No todo items found.");
				break;
			}

			Console.WriteLine("Todo items:");
			for (var index = 0; index < todos.Count; index++)
			{
				var todo = todos[index];
				var status = todo.IsCompleted ? "completed" : "pending";
				Console.WriteLine($"{index + 1}. {todo.Title} ({status})");
			}

			break;

		case "3":
			var todoItems = todoRepository.ListTodos().ToList();

			if (todoItems.Count == 0)
			{
				Console.WriteLine("No todo items found.");
				break;
			}

			for (var index = 0; index < todoItems.Count; index++)
			{
				var todo = todoItems[index];
				var status = todo.IsCompleted ? "completed" : "pending";
				Console.WriteLine($"{index + 1}. {todo.Title} ({status})");
			}

			Console.Write("Enter the number of the todo item to complete: ");
			var itemNumberInput = Console.ReadLine();

			if (int.TryParse(itemNumberInput, out var itemNumber) &&
				itemNumber >= 1 &&
				itemNumber <= todoItems.Count)
			{
				todoRepository.MarkTodoItemAsCompleted(itemNumber - 1);
				Console.WriteLine("Todo item marked as complete.");
			}
			else
			{
				Console.WriteLine("Invalid todo item number.");
			}

			break;

		case "0":
			isRunning = false;
			break;

		default:
			Console.WriteLine("Invalid option.");
			break;
	}
}

