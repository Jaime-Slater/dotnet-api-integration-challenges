using Models;

public static class ConsolePrinter
{
    private static void PrintUserDetails(UserTaskSummary user)
    {
        Console.WriteLine("----------");
        Console.WriteLine($"Name: {user.Name}");
        Console.WriteLine($"Email: {user.Email}");
        Console.WriteLine($"Company name: {user.CompanyName}");
        Console.WriteLine($"Total tasks: {user.TotalTasks}");
        Console.WriteLine($"Completed tasks: {user.CompletedTasks}");
        Console.WriteLine($"Incomplete tasks: {user.IncompleteTasks}");
        Console.WriteLine($"Completion percentage: {user.CompletionPercentage:F1}%");
        Console.WriteLine($"Status: {user.Status}");
    }

    public static void PrintSummary(List<UserTaskSummary> summaryTaskSummaryList)
    {

        if (summaryTaskSummaryList.Count == 0)
        {
            Console.WriteLine("No task summary data available.");
            return;
        }

        foreach (var userTaskSummary in summaryTaskSummaryList.OrderByDescending(x => x.CompletionPercentage))
        {
            PrintUserDetails(userTaskSummary);
        }

        int totalTasks = summaryTaskSummaryList.Sum(x => x.TotalTasks);
        int completedTasks = summaryTaskSummaryList.Sum(x => x.CompletedTasks);
        int incompleteTasks = summaryTaskSummaryList.Sum(x => x.IncompleteTasks);
        decimal averageCompletion = summaryTaskSummaryList.Average(x => x.CompletionPercentage);
        var bestUser = summaryTaskSummaryList.OrderByDescending(x => x.CompletionPercentage).First();
        var worstUser = summaryTaskSummaryList.OrderBy(x => x.CompletionPercentage).First();

        Console.WriteLine("----------");
        Console.WriteLine("Overall Summary");
        Console.WriteLine("");
        Console.WriteLine($"Total Users: {summaryTaskSummaryList.Count}");
        Console.WriteLine($"Total Tasks: {totalTasks}");
        Console.WriteLine($"Total Completed: {completedTasks}");
        Console.WriteLine($"Total Incomplete: {incompleteTasks}");
        Console.WriteLine($"Average Completion: {averageCompletion:F1}%");
        Console.WriteLine($"Best User: {bestUser.Name} - {bestUser.CompletionPercentage:F1}%");
        Console.WriteLine($"Worst User: {worstUser.Name} - {worstUser.CompletionPercentage:F1}%");

    }
}