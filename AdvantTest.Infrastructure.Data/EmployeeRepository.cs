using AdvantTest.Domain.Core;
using AdvantTest.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdvantTest.Infrastructure.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly RepositoryContext _repositoryContext;
        public EmployeeRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public Employee Create(Employee employee)
        {
            return _repositoryContext.Add(employee).Entity;
        }

        public void Delete(Employee employee)
        {
            _repositoryContext.Employees.Remove(employee);
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _repositoryContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            return await _repositoryContext.Employees.FirstOrDefaultAsync(o => o.Id.Equals(id));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _repositoryContext.SaveChangesAsync();
        }

        public Employee Update(Employee employee)
        {
            return _repositoryContext.Employees.Update(employee).Entity;
        }
    }
}
