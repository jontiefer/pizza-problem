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

    public IEnumerable<string> GetDepartmentWithLargestNumberOfEmployeesWhoLovePineapple() =>
        GetMostPreferredList(CompanyPizzaData.DepartmentCountByTopping
            .Where(x => x.Key == "Pineapple")
            .SelectMany(x => x.Value)
            .OrderByDescending(x => x.Value).ToList());


    public IEnumerable<string> GetDepartmentThatPrefersPepperoniAndOnions() =>
        GetMostPreferredList(CompanyPizzaData.DepartmentCountByToppingCombo
            .Where(x => x.Key.OrderBy(i => i).SequenceEqual(new[] { "Pepperoni", "Onions" }.OrderBy(i => i)))
            .SelectMany(x => x.Value)
            .OrderByDescending(x => x.Value).ToList());

    private IEnumerable<string> GetMostPreferredList(List<KeyValuePair<string, int>> mostPreferred)
    {
        var maxCount = 0;

        var mostPreferredList = mostPreferred.ToList();

        maxCount = mostPreferred.First().Value;

        foreach (var item in mostPreferred)
        {
            if(item.Value == maxCount)
                yield return item.Key;
            else
                yield break;
        }
    }



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

