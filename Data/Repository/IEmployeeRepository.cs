using CrudEmployee.Entity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CrudEmployee.Data.Repository
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetAll();
        public Task<Employee> Get(int id);
        public Task<Employee> Create(Employee employee);
        public Task Update(int id, Employee employee);
        public Task Delete(int id);


    }
}
