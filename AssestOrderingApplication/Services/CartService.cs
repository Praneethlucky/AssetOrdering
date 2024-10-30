using AssestOrderingApplication.Models;
using Microsoft.Data.SqlClient;
using System;

namespace AssestOrderingApplication.Services
{
    public class CartService
    {
        private readonly string _connectionString;

        public CartService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Cart> GetCartItems(string EmployeeName)
        {
            var cartItems = new List<Cart>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"select c.AssetId as AId, Quantity, a.Name as AssetName, (Quantity*CAST(REPLACE(a.Category,'$','' ) AS INT)) as TotalCost from Cart c INNER JOIN Assets a ON c.AssetId = a.Id WHERE c.EmployeeId = '{EmployeeName}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var asset = new Cart
                            {
                                AssetId = Int32.Parse(reader["AId"].ToString()),
                                AssetName = reader["AssetName"].ToString(),
                                AssetQuantity = Int32.Parse(reader["Quantity"].ToString()),
                                TotalCost = Double.Parse(reader["TotalCost"].ToString())
                            };
                            cartItems.Add(asset);
                        }
                    }
                }
            }

            return cartItems;
        }
        public bool InsertIntoCart(List<Cart> cart)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO cart (EmployeeId, AssetId, Quantity) VALUES";
                foreach(var item in cart)
                {
                    query += $"('{item.EmployeeName}','{item.AssetId}','{item.AssetQuantity}'),";
                }
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();

                    // Close the connection
                    connection.Close();
                }
            }

            return true;
        }
    }
}
