namespace Models;

public class UserTaskSummary
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string CompanyName { get; set; } = "";
    public int TotalTasks { get; set; }
    public int CompletedTasks { get; set; }
    public int IncompleteTasks { get; set; }
    public decimal CompletionPercentage { get; set; }
    public string Status { get; set; } = "";
}