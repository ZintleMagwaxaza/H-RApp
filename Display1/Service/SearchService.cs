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

        public event Action<Person>? OnUserSelected;

        public SearchService(AdventureWorks2019Context dbContext)
        {
            _db = dbContext;
            SearchResults = new List<Person>();
            Shifts = new List<Shift>();
        }

        public void PerformSearch()
        {
            if (!string.IsNullOrEmpty(SearchInput))
            {
                SearchResults = _db.Person
                    .AsEnumerable()
                    .Where(p => p.PersonType == "EM" &&
                                (p.FirstName.Contains(SearchInput, StringComparison.OrdinalIgnoreCase) ||
                                p.LastName.Contains(SearchInput, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }
            else
            {
                SearchResults.Clear();
            }
        }

        public void SelectUser(Person person)
        {
            if (person.PersonType == "EM")
            {
                SelectedPerson = person;
                SelectedPersonHistory = GetEmployeeDepartmentHistory(person.BusinessEntityId);
                Shifts = GetShiftsForBusinessEntity(person.BusinessEntityId);
                OnUserSelected?.Invoke(person);
            }
        }

        public EmployeeDepartmentHistory? GetEmployeeDepartmentHistory(int businessEntityId)
        {
            return _db.EmployeeDepartmentHistory
                .FirstOrDefault(edh => edh.BusinessEntityId == businessEntityId);
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
    }
}
