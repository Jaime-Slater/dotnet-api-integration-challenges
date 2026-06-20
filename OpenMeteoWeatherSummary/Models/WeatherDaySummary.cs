namespace Models;

public class WeatherDaySummary
{
    public DateOnly Date { get; set; }
    public decimal MinTemperature { get; set; }
    public decimal MaxTemperature { get; set; }
    public decimal AverageTemperature { get; set; }
    public decimal Rainfall { get; set; }
    public string Risk { get; set; } = "";

}