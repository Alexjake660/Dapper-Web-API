using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DapperWebAPI.Models
{
    public class EmployeeRepository
    {
        private string conStr;
        public EmployeeRepository(IConfiguration _configuration)
        {
            conStr = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(conStr);
            }
        }

        //INSERT
        public void Add(Employee employee)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"INSERT INTO Employees (FirstName, LastName, Email, City, Salary) 
                                VALUES (@FirstName, @LastName, @Email, @City, @Salary)";
                dbConnection.Open();
                dbConnection.Execute(sql, employee);
                dbConnection.Close();
            }
        }

        //GET ALL
        public IEnumerable<Employee> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"SELECT * FROM Employees";
                dbConnection.Open();
                return dbConnection.Query<Employee>(sql);
            }
        }

        //GET BY ID
        public Employee GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"SELECT * FROM Employees WHERE Id = @id";
                dbConnection.Open();
                return dbConnection.Query<Employee>(sql, new { Id = id }).FirstOrDefault();
            }
        }

        //UPDATE
        public void Update(Employee employee)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, 
                                Email = @Email, City = @City, Salary = @Salary WHERE Id = @id";
                dbConnection.Open();
                dbConnection.Query(sql, employee);
            }
        }

        //DELETE
        public void Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"DELETE FROM Employees WHERE Id = @id";
                dbConnection.Open();
                dbConnection.Query(sql, new { Id = id });
            }
        }
    }
}