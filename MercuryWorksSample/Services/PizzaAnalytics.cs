using MercuryWorksSample.Data;

namespace MercuryWorksSample.Services;

public class PizzaAnalytics
{
    public PizzaData CompanyPizzaData { get; }
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fileName">File name containing company pizza data.</param>
    public PizzaAnalytics(string fileName)
    {
        var pizzaDataLoader = new PizzaDataLoader();

        CompanyPizzaData = pizzaDataLoader.LoadData(fileName);
    }

    public string GetDepartmentWithLargestNumberOfEmployeesWhoLovePineapple() =>
        CompanyPizzaData.DepartmentCountByTopping
            .Where(x => x.Key == "Pineapple")
            .Max(x => x.Value)!
            .First().Key;

    public string GetDepartmentThatPrefersPepperoniAndOnions() =>
         CompanyPizzaData.DepartmentCountByToppingCombo
            .Where(x => x.Key.OrderBy(i => i).SequenceEqual(new [] { "Pepperoni", "Onions" }.OrderBy(i => i)))
            .Max(x => x.Value)!
            .First().Key;

    public int GetEmployeeCountThatPreferAnchovies() =>
        CompanyPizzaData.DepartmentCountByTopping
            .Where(x => x.Key == "Anchovies")
            .SelectMany(x => x.Value)
            .Sum(x => x.Value);

    public int GetPizzaCountToFeedEngineeringDepartment() =>
        CompanyPizzaData.EmployeeCountByDepartment["Engineering"] % 4 == 0
            ? Convert.ToInt32(CompanyPizzaData.EmployeeCountByDepartment["Engineering"] / 4d)
            : Convert.ToInt32(Math.Floor(CompanyPizzaData.EmployeeCountByDepartment["Engineering"] / 4d) + 1);

    public IEnumerable<(string Department, string[] ToppingsCombo, int EmployeeCount)> GetMostPopularPizzaToppingComboByDepartment()
    {
        foreach (var departmentToppingComboCount in CompanyPizzaData.ToppingComboCountByDepartment)
        {
            var maxItem = departmentToppingComboCount.Value.MaxBy(x => x.Value);

            yield return
            (
                Department: departmentToppingComboCount.Key,
                ToppingsCombo: maxItem.Key,
                EmployeeCount: maxItem.Value
            );
        }
    }
}

