using Display1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class EmployeeService
{
private readonly AdventureWorks2019Context dbContext;

public EmployeeService(AdventureWorks2019Context dbContext)
{
    this.dbContext = dbContext;
}
    public async Task<int> FindBusinessEntityIdByEmailAsync(string email)
    {
        var businessEntityId = await dbContext.EmailAddress
            .Where(e => e.EmailAddress1 == email)
            .Select(e => e.BusinessEntityId)
            .FirstOrDefaultAsync();

        return businessEntityId;
    }

    //to check for person instead of emoplyee only 
    public Person GetPersonByBusinessEntityId(int businessEntityId)
    {
        Person person = dbContext.Person
            .FirstOrDefault(p => p.BusinessEntityId == businessEntityId);

        return person;
    }

    //to check for employee
    public Employee GetEmployeeByBusinessEntityId(int businessEntityId)
{
    Employee employee = dbContext.Employee
        .Include(e => e.BusinessEntity)
        .FirstOrDefault(e => e.BusinessEntityId == businessEntityId);

    if (employee != null)
    {
        Person person = dbContext.Person
            .FirstOrDefault(p => p.BusinessEntityId == employee.BusinessEntityId);

        if (person != null)
        {
            employee.BusinessEntity = person;
        }
        else
        {
            employee.BusinessEntity = null;
        }
    }

    return employee;
}

    public async Task<EmployeeDepartmentHistory> FindEmployeeDepartment(int businessEntityId)
    {
        return await dbContext.EmployeeDepartmentHistory
            .Include(ed => ed.Department)
            .FirstOrDefaultAsync(ed => ed.BusinessEntityId == businessEntityId);
    }

    public async Task<EmployeeDepartmentHistory> GetEmployeeDepartmentByBusinessEntityId(int businessEntityId)
{
    EmployeeDepartmentHistory employeeDepartment = await dbContext.EmployeeDepartmentHistory
        .Include(ed => ed.Department)
        .FirstOrDefaultAsync(ed => ed.BusinessEntityId == businessEntityId);

    if (employeeDepartment != null)
    {
        Department department = await dbContext.Department
            .FirstOrDefaultAsync(d => d.DepartmentId == employeeDepartment.DepartmentId);

        if (department != null)
        {
            employeeDepartment.Department.Name = department.Name;
            employeeDepartment.Department.GroupName = department.GroupName;
        }
    }

    return employeeDepartment;
}

public async Task<Shift> GetShiftByBusinessEntityId(int businessEntityId)
{
    EmployeeDepartmentHistory employeeDepartmentHistory = await dbContext.EmployeeDepartmentHistory
        .Include(edh => edh.Department)
        .FirstOrDefaultAsync(edh => edh.BusinessEntityId == businessEntityId);

    if (employeeDepartmentHistory != null)
    {
        Shift shift = await dbContext.Shift.FindAsync(employeeDepartmentHistory.ShiftId);
        return shift;
    }

    return null;
}

public async Task<EmployeePayHistory> GetEmployeePayHistoryByBusinessEntityId(int businessEntityId)
{
    return await dbContext.EmployeePayHistory
        .FirstOrDefaultAsync(e => e.BusinessEntityId == businessEntityId);
}

    public Address? GetAddressForBusinessEntity(int businessEntityId)
    {
        var businessEntityAddress = dbContext.BusinessEntityAddress
            .FirstOrDefault(bea => bea.BusinessEntityId == businessEntityId);

        if (businessEntityAddress != null)
        {
            return dbContext.Address.FirstOrDefault(a => a.AddressId == businessEntityAddress.AddressId);
        }

        return null;
    }


}
