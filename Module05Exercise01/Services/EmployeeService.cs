using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module05Exercise01.Model;
using MySql.Data.MySqlClient;
using System.Data;

namespace Module05Exercise01.Services
{
    internal class EmployeeService
    {
        private readonly string _connectionString;

        public EmployeeService()
        {
            var dbService = new DatabaseConnectionService();
            _connectionString = dbService.GetConnectionString();
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var employeeService = new List<Employee>();
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var cmd = new MySqlCommand("SELECT * FROM tblEmployee", conn);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        employeeService.Add(new Employee
                        {
                            EmployeeID = reader.GetInt32("EmployeeID"),
                            Name = reader.GetString("Name"),
                            Address = reader.GetString("Address"),
                            Email = reader.GetString("Email"),
                            ContactNo = reader.GetString("ContactNo"),
                        });
                    }
                }
            }

            return employeeService;
        }

        public async Task<bool> AddEmployeeAsync(Employee newEmployee)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    var cmd = new MySqlCommand("INSERT INTO tblEmployee (Name, Address, Email, ContactNo) VALUES (@Name, @Address, @Email, @contactNo)", conn);
                    cmd.Parameters.AddWithValue("@Name", newEmployee.Name);
                    cmd.Parameters.AddWithValue("@Address", newEmployee.Address);
                    cmd.Parameters.AddWithValue("@Email", newEmployee.Email);
                    cmd.Parameters.AddWithValue("@ContactNo", newEmployee.ContactNo);

                    var result = await cmd.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error adding employee record: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    var cmd = new MySqlCommand("DELETE FROM tblEmployee WHERE EmployeeID=@EmployeeID", conn);
                    cmd.Parameters.AddWithValue("@EmployeeID", id);

                    var result = await cmd.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee record: {ex.Message}");
                return false;
            }
        }
        
        public async Task<bool> UpdateEmployeeAsync(Employee updatedEmployee)
        {
            try
            {
                using (var conn = new MySqlConnection ( _connectionString))
                {
                    await conn.OpenAsync();
                    var cmd = new MySqlCommand("UPDATE tblEmployee SET Name = @Name, Address = @Address, Email = @Email, ContactNo = @ContactNo WHERE EmployeeID = @EmployeeID");
                    cmd.Parameters.AddWithValue("@Name", updatedEmployee.Name);
                    cmd.Parameters.AddWithValue("@Address", updatedEmployee.Address);
                    cmd.Parameters.AddWithValue("@Email", updatedEmployee.Email);
                    cmd.Parameters.AddWithValue("@ContactNo", updatedEmployee.ContactNo);

                    var result = await cmd.ExecuteNonQueryAsync();
                    return result > 0;


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating personal record: {ex.Message}");
                return false;
            }
        }



    }
}
