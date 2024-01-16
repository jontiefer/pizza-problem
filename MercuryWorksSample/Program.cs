// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using MercuryWorksSample.Services;

var pizzaAnalytics = new PizzaAnalytics("./Data/Data.json");

Console.WriteLine("Which department has the largest number of employees who like Pineapple on their pizzas?");
Console.WriteLine(pizzaAnalytics.GetDepartmentWithLargestNumberOfEmployeesWhoLovePineapple());
Console.WriteLine();

Console.WriteLine("Which department prefers Peperoni and Onions?");
Console.WriteLine(pizzaAnalytics.GetDepartmentThatPrefersPepperoniAndOnions());
Console.WriteLine();

Console.WriteLine("How many people prefer Anchovies?");
Console.WriteLine(pizzaAnalytics.GetEmployeeCountThatPreferAnchovies());
Console.WriteLine();

Console.WriteLine("How many pizzas would you need to order to feed the Engineering department, assuming a pizza feeds 4 people? Ignore personal preferences.");
Console.WriteLine(pizzaAnalytics.GetPizzaCountToFeedEngineeringDepartment());
Console.WriteLine();

Console.WriteLine("Which pizza topping combination is the most popular in each department and how many employees prefer it?");

pizzaAnalytics.GetMostPopularPizzaToppingComboByDepartment().Select(x => new
    {
        x.Department,
        ToppingsCombo = string.Join(", ", x.ToppingsCombo),
        x.EmployeeCount
    }).ToList().ForEach(i => Console.WriteLine(i.ToString()));

