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
        public List<JobCandidate> JobCandidates { get; set; }
        


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
                string[] names = SearchInput.Split(' ');

                string firstName = names[0];
                string lastName = names.Length > 1 ? names[1] : string.Empty;

                if (string.IsNullOrEmpty(lastName))
                {
                    SearchResults = _db.Employee
                        .Include(e => e.BusinessEntity)
                        .AsEnumerable()
                        .Where(e => e.BusinessEntity.FirstName.IndexOf(firstName, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                    e.BusinessEntity.LastName.IndexOf(firstName, StringComparison.OrdinalIgnoreCase) >= 0)
                        .Select(e => e.BusinessEntity)
                        .ToList();
                }
                else
                {
                    SearchResults = _db.Employee
                        .Include(e => e.BusinessEntity)
                        .AsEnumerable()
                        .Where(e => e.BusinessEntity.FirstName.IndexOf(firstName, StringComparison.OrdinalIgnoreCase) >= 0 &&
                                    e.BusinessEntity.LastName.IndexOf(lastName, StringComparison.OrdinalIgnoreCase) >= 0)
                        .Select(e => e.BusinessEntity)
                        .ToList();
                }
            }
            else
            {
                SearchResults.Clear();
            }

            if (SearchResults.Count == 0)
            {
                SelectedPerson = null;
            }
        }






        public void SelectUser(Person person)
        {
            var employee = _db.Employee.FirstOrDefault(e => e.BusinessEntityId == person.BusinessEntityId);
            if (employee != null)
            {
                SelectedPerson = employee.BusinessEntity;
                SelectedPersonHistory = GetEmployeeDepartmentHistory(employee.BusinessEntityId);
                Shifts = GetShiftsForBusinessEntity(employee.BusinessEntityId);
                OnUserSelected?.Invoke(employee.BusinessEntity);
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
