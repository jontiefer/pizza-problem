namespace MercuryWorksSample.Data;

public class PizzaData
{
    public Dictionary<string, int> EmployeeCountByDepartment { get; } = new();

    public Dictionary<string[], Dictionary<string, int>> DepartmentCountByToppingCombo { get; } = new(new PizzaDataStringArrayComparer());

    public Dictionary<string, Dictionary<string, int>> DepartmentCountByTopping { get; } = new();

    public Dictionary<string, Dictionary<string[], int>> ToppingComboCountByDepartment { get; } = new();

    public PizzaData()
    {
    }
}

public class PizzaDataStringArrayComparer : IEqualityComparer<string[]>
{
    public bool Equals(string[]? x, string[]? y)
    {
        if (ReferenceEquals(x, y)) return true;

        if (x == null || y == null)
            return false;

        return x.OrderBy(e => e).SequenceEqual(y.OrderBy(e => e));
    }

    public int GetHashCode(string[] obj)
    {
        int hash = 17;

        foreach (var str in obj.OrderBy(e => e))
        {
            hash = hash * 23 + (str?.GetHashCode() ?? 0);
        }

        return hash;
    }
}