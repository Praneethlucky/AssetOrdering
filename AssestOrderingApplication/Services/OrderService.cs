using AssestOrderingApplication.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace AssestOrderingApplication.Services
{
    public class OrderService
    {
        private readonly string _connectionString;

        public OrderService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public List<Order> GetOrderById(string EmployeeName)
        {
            List<Order> orderList = new List<Order>(); // Initialize the order object to null

            // Replace with your actual connection string
            string connectionString = _connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Open the connection

                // SQL query to retrieve the order by OrderId
                string query = "SELECT * FROM [Orders] WHERE EmployeeId = @EmployeeName ORDER BY OrderId Desc";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeName", EmployeeName); // Add the parameter for OrderId

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has any results
                        while (reader.Read())
                        {
                            // Map the data to the Order object
                            Order order = new Order
                            {
                                OrderId = (int)reader["OrderId"],
                                AssetNames = reader["AssetId"].ToString(),
                                EmployeeId = reader["EmployeeId"].ToString(),
                                OrderDate = (DateTime)reader["OrderDate"],
                                ManagerEmployeeId = reader["ManagerEmployeeId"].ToString(),
                                ProductFamily = reader["ProductFamily"].ToString(),
                                DeliverTo = reader["DeliverTo"].ToString(),
                                Country = reader["Country"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Comments = reader["Comments"].ToString(),
                                OfficeAddress = reader["OfficeLocation"].ToString(),
                                HomeAddress = reader["HomeLocation"].ToString(),
                                OrderStatus = reader["OrderStatus"].ToString()
                            };
                            orderList.Add(order);
                        }
                    }
                }
            }

            return orderList; // Return the retrieved order
        }

        internal List<Order> GetOrderByManager(string EmployeeName)
        {
            List<Order> orderList = new List<Order>(); // Initialize the order object to null

            // Replace with your actual connection string
            string connectionString = _connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Open the connection

                // SQL query to retrieve the order by OrderId
                string query = "SELECT * FROM [Orders] WHERE ManagerEmployeeId = @EmployeeName ORDER BY OrderId Desc";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeName", EmployeeName); // Add the parameter for OrderId

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has any results
                        while (reader.Read())
                        {
                            // Map the data to the Order object
                            Order order = new Order
                            {
                                OrderId = (int)reader["OrderId"],
                                AssetNames = reader["AssetId"].ToString(),
                                EmployeeId = reader["EmployeeId"].ToString(),
                                OrderDate = (DateTime)reader["OrderDate"],
                                ManagerEmployeeId = reader["ManagerEmployeeId"].ToString(),
                                ProductFamily = reader["ProductFamily"].ToString(),
                                DeliverTo = reader["DeliverTo"].ToString(),
                                Country = reader["Country"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Comments = reader["Comments"].ToString(),
                                OfficeAddress = reader["OfficeLocation"].ToString(),
                                HomeAddress = reader["HomeLocation"].ToString(),
                                OrderStatus = reader["OrderStatus"].ToString()
                            };
                            orderList.Add(order);
                        }
                    }
                }
            }

            return orderList; // Return the retrieved order
        }
    }
}
