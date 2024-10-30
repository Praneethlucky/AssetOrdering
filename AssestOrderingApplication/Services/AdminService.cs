using AssestOrderingApplication.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace AssestOrderingApplication.Services
{
    public class AdminService
    {
        private readonly string _connectionString;

        public AdminService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public bool AddAdmin(string Name, string AddedBy)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = $"INSERT INTO Admins (AdminName, AddedByEmployeeId) VALUES ('{Name}','{AddedBy}')";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();

                        // Close the connection
                        connection.Close();
                    }
                }
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }
        public bool RemoveAdmin(string Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = $"DELETE FROM Admins WHERE Id = '{Id}')";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();

                        // Close the connection
                        connection.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool IsAdmin(string EmployyeName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = $"SELECT AdminId FROM Admins WHERE AdminName='{EmployyeName}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
