using Models;
using Services;

var jsonPlaceholder = new JsonPlaceholderClient();

var users = await jsonPlaceholder.GetUsersAsync();

if (users.Count == 0)
{
    Console.WriteLine("No users found!");
    return;
}

var todos = await jsonPlaceholder.GetTodosAsync();

if (todos.Count == 0)
{
    Console.WriteLine("No todos found!");
    return;
}


var userTaskSummaryList = new List<UserTaskSummary>();
Console.WriteLine("Task Completion Report");
Console.WriteLine(" ");
foreach (var user in users)
{
    //Map the user to their todos
    var userTodos = todos.Where(x => x.UserId == user.Id).ToList();

    //Create user task Summary
    var userTaskSummary = new UserTaskSummary
    {
        Name = user.Name,
        Email = user.Email,
        CompanyName = user.Company.Name,
        TotalTasks = userTodos.Count,
        CompletedTasks = userTodos.Count(x => x.Completed),
        IncompleteTasks = userTodos.Count(x => !x.Completed),
        CompletionPercentage = CalculatePercentage(userTodos)
    };

    userTaskSummary.Status = GetStatus(userTaskSummary.CompletionPercentage);
    userTaskSummaryList.Add(userTaskSummary);
}

ConsolePrinter.PrintSummary(userTaskSummaryList);

string GetStatus(decimal completionPercentage)
{
    if (completionPercentage >= 80)
    {
        return "Excellent";
    }
    else if (completionPercentage >= 60)
    {
        return "Good";
    }
    else
    {
        return "Needs Focus";
    }
}

decimal CalculatePercentage(List<Todo> userTodos)
{
    var completed = userTodos.Count(x => x.Completed);
    int total = userTodos.Count;
    if (total > 0)
    {
        decimal value = (decimal)completed / total * 100;
        return value;
    }

    return 0;
}