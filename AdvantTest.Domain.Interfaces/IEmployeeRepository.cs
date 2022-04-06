using AdvantTest.Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdvantTest.Domain.Interfaces
{
    /// <summary>
    /// Interface describing methods for manipulating employee data
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Receives all employee
        /// </summary>
        /// <returns>Employee list</returns>
        Task<IEnumerable<Employee>> GetAll();

        /// <summary>
        /// Creates a new employee
        /// </summary>
        /// <param name="employee">Entity of the created employee</param>
        /// <returns>Created employee</returns>
        Employee Create(Employee employee);

        /// <summary>
        /// Updates the data of an existing employee
        /// </summary>
        /// <param name="employee">Updated data</param>
        /// <returns>Updated employee</returns>
        Employee Update(Employee employee);

        /// <summary>
        /// Deletes employee by id
        /// </summary>
        /// <param name="id">Employee id</param>
        void Delete(Employee id);

        /// <summary>
        /// Gets employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Employee object</returns>
        Task<Employee> GetById(int id);

        /// <summary>
        /// Saves all changed in the database
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
