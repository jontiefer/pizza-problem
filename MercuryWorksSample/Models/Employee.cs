namespace MercuryWorksSample.Models;

public class Employee
{
    public string Name { get; set; } = null!;
    public string Department { get; set; } = null!;
    public List<string> Toppings { get; set; } = new();
}
