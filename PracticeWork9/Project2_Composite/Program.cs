var emp1 = new Employee("user1", "Position1", 5000);
var emp2 = new Employee("user2", "Position2", 6000);
var emp3 = new Employee("user3", "Position3", 7000);
var emp4 = new Employee("user4", "Position4", 8000);
var emp5 = new Employee("user5", "Position5", 9000);

var contractor = new Contractor("contractor1", "IT support", 4000);

var itDep = new Department("IT Department", "Handles technology");
itDep.Add(emp1);
itDep.Add(emp2);
itDep.Add(emp3);
itDep.Add(contractor);

var hrDep = new Department("HR Department", "Handles hiring");
hrDep.Add(emp4);
hrDep.Add(emp5);

var company = new Department("Tech Company", "Parent company");
company.Add(itDep);
company.Add(hrDep);

Console.WriteLine("Company structure: ");
company.Display(1);

var budget = company.CalculateBudget();
Console.WriteLine($"\nTotal company budget: {budget}");

Console.WriteLine($"\nTotal employees: {company.GetEmployeeCount()}");

emp1.ChangeSalary(10000);
Console.WriteLine($"\nПосле повышения зарплаты {emp1.Name}:");
Console.WriteLine($"Новый бюджет: {company.CalculateBudget()}");

Console.WriteLine("\nПоиск сотрудника 'user3':");
var found = company.FindEmployeeByName("user3");
if (found != null)
    found.Display(1);
else
    Console.WriteLine("Сотрудник не найден.");

Console.WriteLine("\nВсе сотрудники IT отдела:");
itDep.DisplayAllEmployees();


public abstract class OrganizationComponent(string name, string description)
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;

    public abstract void Display(int indentLevel);
    public abstract decimal CalculateBudget();
    public abstract int GetEmployeeCount();

    public virtual void Add(OrganizationComponent component) => throw new NotImplementedException();

    public virtual void Remove(OrganizationComponent component) => throw new NotImplementedException();

    public virtual OrganizationComponent? GetChild(int index) => null;

    public virtual Employee? FindEmployeeByName(string name) => null;

    public virtual void DisplayAllEmployees() { }
}

public class Employee(string name, string position, decimal salary)
    : OrganizationComponent(name, position)
{
    protected string Position { get; set; } = position;
    protected decimal Salary { get; private set; } = salary;

    public void ChangeSalary(decimal newSalary)
    {
        Salary = newSalary;
        Console.WriteLine($"{Name} теперь получает {Salary}");
    }

    public override void Display(int indentLevel)
    {
        Console.WriteLine(new string('-', indentLevel) +
                          $"Employee: {Name}, Position: {Position}, Salary: {Salary}");
    }

    public override decimal CalculateBudget() => Salary;

    public override int GetEmployeeCount() => 1;

    public override Employee? FindEmployeeByName(string name) => Name.Equals(name, StringComparison.OrdinalIgnoreCase) ? this : null;
}

public class Contractor(string name, string position, decimal fixedPayment) : Employee(name, position, fixedPayment)
{
    public override decimal CalculateBudget() => 0;

    public override void Display(int indentLevel) =>
        Console.WriteLine(new string('-', indentLevel) +
                          $"[Contractor] {Name}, Position: {Position}, Fixed Payment: {Salary}");
}

public class Department(string name, string description)
    : OrganizationComponent(name, description)
{
    private readonly List<OrganizationComponent> _components = [];

    public override void Add(OrganizationComponent component) =>
        _components.Add(component);

    public override void Remove(OrganizationComponent component) =>
        _components.Remove(component);

    public override OrganizationComponent GetChild(int index) => _components[index];

    public override void Display(int indentLevel)
    {
        Console.WriteLine(new string('-', indentLevel) + $"Department: {Name}");
        foreach (var component in _components)
            component.Display(indentLevel + 2);
    }

    public override decimal CalculateBudget() => _components.Sum(c => c.CalculateBudget());

    public override int GetEmployeeCount() => _components.Sum(c => c.GetEmployeeCount());

    public override Employee? FindEmployeeByName(string name) => 
        _components.Select(c => c.FindEmployeeByName(name)).OfType<Employee>().FirstOrDefault();

    public override void DisplayAllEmployees()
    {
        foreach (var organization in _components)
        {
            switch (organization)
            {
                case Employee or Contractor:
                    organization.Display(2);
                    break;
                case Department dep:
                    dep.DisplayAllEmployees();
                    break;
            }
        }
    }
}
