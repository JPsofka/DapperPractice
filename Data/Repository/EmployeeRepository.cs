using CrudEmployee.Entity;
using Dapper;
using System.Data;

namespace CrudEmployee.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;
        public EmployeeRepository(DapperContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var query = "SELECT * FROM Employee";

            using (var connection = _context.GetConnection()) 
            {
                var employees = await connection.QueryAsync<Employee>(query);
                return employees;
            }
        }

        public async Task<Employee> Get(int id)
        {
            var query = "SELECT * FROM Employee WHERE Id = @Id";
            using (var connection = _context.GetConnection())
            {
                var employee = await connection.QuerySingleOrDefaultAsync<Employee>(query, new { id });
                return employee;
            }
        }

        public async Task<Employee> Create(Employee employee)
        {
            var query = "INSERT INTO Employee (Name, EmployeeCode, Email, Age, HireDate, TerminationDate) " +
                "VALUES (@Name, @EmployeeCode, @Email, @Age, @HireDate, @TerminationDate)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", employee.Name, DbType.String);
            parameters.Add("EmployeeCode", employee.EmployeeCode, DbType.String);
            parameters.Add("Email", employee.Email, DbType.String);
            parameters.Add("Age", employee.Age, DbType.String);
            employee.HireDate = DateTime.Now;
            parameters.Add("HireDate", employee.HireDate, DbType.DateTime);
            parameters.Add("TerminationDate", employee.TerminationDate, DbType.DateTime);


            using (var connection = _context.GetConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdEmployee = new Employee
                {
                    Id = id,
                    Name = employee.Name,
                    EmployeeCode = employee.EmployeeCode,
                    Email = employee.Email,
                    Age = employee.Age,
                    HireDate = employee.HireDate,
                    TerminationDate = employee.TerminationDate
                };
                return createdEmployee;
            }
        }

        public async Task Update(int id, Employee employee)
        {
            var query = "UPDATE Employee SET Name = @Name, EmployeeCode = @EmployeeCode, Email= @Email, Age = @Age," +
                " HireDate = @HireDate, TerminationDate = @TerminationDate WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", employee.Name, DbType.Int32);
            parameters.Add("Name", employee.Name, DbType.String);
            parameters.Add("EmployeeCode", employee.EmployeeCode, DbType.String);
            parameters.Add("Email", employee.Email, DbType.String);
            parameters.Add("Age", employee.Age, DbType.String);
            parameters.Add("HireDate", employee.HireDate, DbType.DateTime);
            parameters.Add("TerminationDate", employee.TerminationDate, DbType.DateTime);
            using (var connection = _context.GetConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task Delete(int id)
        {
            var query = "DELETE FROM Employee WHERE Id = @Id";
            using (var connection = _context.GetConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}
