using MercuryWorksSample.Data;
using MercuryWorksSample.Models;
using Newtonsoft.Json;

namespace MercuryWorksSample.Services;

public class PizzaDataLoader
{
    public PizzaData LoadData(string fileName)
    {
        var pizzaData = new PizzaData();

        using var stream = File.OpenRead(fileName);
        using var streamReader = new StreamReader(stream);

        using var jsonTextReader = new JsonTextReader(streamReader);

        var jsonSerializer = new JsonSerializer();

        jsonTextReader.Read();

        while (jsonTextReader.Read())
        {
            if (jsonTextReader.TokenType == JsonToken.StartObject)
            {
                Employee employee = jsonSerializer.Deserialize<Employee>(jsonTextReader)!;
                var toppingCombo = employee.Toppings.ToArray();

                // EmployeeCountByDepartment
                if (!pizzaData.EmployeeCountByDepartment.ContainsKey(employee.Department))
                    pizzaData.EmployeeCountByDepartment.Add(employee.Department, 1);
                else
                    pizzaData.EmployeeCountByDepartment[employee.Department]++;

                // DepartmentCountByToppingCombo
                if (!pizzaData.DepartmentCountByToppingCombo.ContainsKey(toppingCombo))
                {
                    pizzaData.DepartmentCountByToppingCombo.Add(toppingCombo,
                        new Dictionary<string, int>() { [employee.Department] = 1 });
                }
                else
                {
                    if(!pizzaData.DepartmentCountByToppingCombo[toppingCombo].ContainsKey(employee.Department))
                        pizzaData.DepartmentCountByToppingCombo[toppingCombo].Add(employee.Department, 1);
                    else
                        pizzaData.DepartmentCountByToppingCombo[toppingCombo][employee.Department]++;
                }

                // DepartmentCountByTopping
                foreach (var topping in employee.Toppings)
                {
                    if (!pizzaData.DepartmentCountByTopping.ContainsKey(topping))
                    {
                        pizzaData.DepartmentCountByTopping.Add(topping,
                            new Dictionary<string, int>() { [employee.Department] = 1 });
                    }
                    else
                    {
                        if (!pizzaData.DepartmentCountByTopping[topping].ContainsKey(employee.Department))
                            pizzaData.DepartmentCountByTopping[topping].Add(employee.Department, 1);
                        else
                            pizzaData.DepartmentCountByTopping[topping][employee.Department]++;
                    }
                        
                }

                // ToppingComboCountByDepartment
                if (!pizzaData.ToppingComboCountByDepartment.ContainsKey(employee.Department))
                {
                    pizzaData.ToppingComboCountByDepartment.Add(employee.Department,
                        new Dictionary<string[], int>(new PizzaDataStringArrayComparer()) { [toppingCombo] = 1 });
                }
                else
                {
                    if (!pizzaData.ToppingComboCountByDepartment[employee.Department].ContainsKey(toppingCombo))
                        pizzaData.ToppingComboCountByDepartment[employee.Department].Add(toppingCombo, 1);
                    else
                        pizzaData.ToppingComboCountByDepartment[employee.Department][toppingCombo]++;
                }
            }
        }

        return pizzaData;
    }
}

