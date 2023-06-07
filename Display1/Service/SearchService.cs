using Display1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Display1.Service
{
    public class SearchService
    {
        private AdventureWorks2019Context _db;

        public string? SearchInput { get; set; }
        public Person? SelectedPerson { get; set; }
        public List<Person> SearchResults { get; set; }
        public EmployeeDepartmentHistory? SelectedPersonHistory { get; set; }
        public EmployeePayHistory? SelectedEmployeePayHistory { get; set; }
        public Display1.Models.Address? SelectedAddress { get; set; }
        public List<Shift> Shifts { get; set; }

        public event Action<Person> OnUserSelected;
        public List<JobCandidate> JobCandidates { get; set; }

        public string ShiftCode { get; set; } // Added ShiftCode property


        public SearchService(AdventureWorks2019Context dbContext)
        {
            _db = dbContext;
            SearchResults = new List<Person>();
            Shifts = new List<Shift>();
        }

        public void Initialize()
        {
            PerformSearch(); // Perform any initialization logic here
        }

        public void PerformSearch()
        {
            if (string.IsNullOrEmpty(SearchInput))
            {
                // Display all employees in alphabetical order by first name
                SearchResults = _db.Employee
                    .Include(e => e.Person)
                    .Select(e => e.Person)
                    .OrderBy(e => e.FirstName)
                    .ToList();
            }
            else
            {
                string[] names = SearchInput.Split(' ');

                string firstName = names[0];
                string lastName = names.Length > 1 ? names[1] : string.Empty;

                if (string.IsNullOrEmpty(lastName))
                {
                    SearchResults = _db.Employee
                        .Include(e => e.BusinessEntity)
                        .AsEnumerable()
                        .Where(e => e.Person.FirstName.IndexOf(firstName, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                    e.Person.LastName.IndexOf(firstName, StringComparison.OrdinalIgnoreCase) >= 0)
                        .Select(e => e.Person)
                        .OrderBy(e => e.FirstName)
                        .ToList();
                }
                else
                {
                    SearchResults = _db.Employee
                        .Include(e => e.Person)
                        .AsEnumerable()
                        .Where(e => e.Person.FirstName.IndexOf(firstName, StringComparison.OrdinalIgnoreCase) >= 0 &&
                                    e.Person.LastName.IndexOf(lastName, StringComparison.OrdinalIgnoreCase) >= 0)
                        .Select(e => e.Person)
                        .OrderBy(e => e.FirstName)
                        .ToList();
                }
            }

            if (SearchResults.Count == 0)
            {
                // If no search results, display all employees
                SearchResults = _db.Employee
                    .Include(e => e.Person)
                    .Select(e => e.Person)
                    .OrderBy(e => e.FirstName)
                    .ToList();
            }

            SelectedPerson = null;
        }


        public void SelectUser(Person person)
        {
            var employee = _db.Employee.FirstOrDefault(e => e.BusinessEntityId == person.BusinessEntityId);
            if (employee != null)
            {
                SelectedPerson = employee.Person;
                SelectedPersonHistory = GetEmployeeDepartmentHistory(employee.BusinessEntityId);

                // Update the shifts only if the selected person has changed
                if (SelectedPerson != person)
                {
                    Shifts = GetShiftsForBusinessEntity(person.BusinessEntityId);
                    SelectedPerson = person;
                }

                // Trigger the OnUserSelected event
                OnUserSelected?.Invoke(person);
            }
        }


        public EmployeeDepartmentHistory? GetEmployeeDepartmentHistory(int businessEntityId)
        {
            EmployeeDepartmentHistory employeeDepartmentHistory = _db.EmployeeDepartmentHistory
                .FirstOrDefault(edh => edh.BusinessEntityId == businessEntityId);

            if (employeeDepartmentHistory != null)
            {
                Department department = _db.Department
                    .FirstOrDefault(d => d.DepartmentId == employeeDepartmentHistory.DepartmentId);

                if (department != null)
                {
                    employeeDepartmentHistory.Department = department;
                }
            }

            return employeeDepartmentHistory;
        }




        public EmployeePayHistory? GetEmployeePayHistory(int businessEntityId)
        {
            return _db.EmployeePayHistory.FirstOrDefault(p => p.BusinessEntityId == businessEntityId);
        }

        public Display1.Models.Address? GetAddressForBusinessEntity(int businessEntityId)
        {
            return _db.BusinessEntityAddress
                .Include(bea => bea.Address)
                .FirstOrDefault(bea => bea.BusinessEntityId == businessEntityId)?.Address;
        }

        public List<Shift> GetShiftsForBusinessEntity(int businessEntityId)
        {
            return _db.EmployeeDepartmentHistory
                .Where(edh => edh.BusinessEntityId == businessEntityId)
                .Select(edh => edh.Shift)
                .ToList();
        }

        public async Task UpdateAddress(Address address)
        {
            if (address != null)
            {
                _db.Entry(address).State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
        }
    }


}