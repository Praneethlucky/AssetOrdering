using AssestOrderingApplication.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AssestOrderingApplication.Services
{
    public class AssetService
    {
        private readonly string _connectionString;

        public AssetService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Asset> GetAllAssets()
        {
            List<Asset> assets = new List<Asset>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Name, Type, Category, Location, InsertedBy, InsertedTime, UpdatedTime FROM Assets";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Asset asset = new Asset
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Type = reader["Type"].ToString(),
                                Category = reader["Category"].ToString(),
                                Location = reader["Location"].ToString(),
                                InsertedBy = reader["InsertedBy"].ToString(),
                                InsertedTime = (DateTime)reader["InsertedTime"],
                                UpdatedTime = (DateTime)reader["UpdatedTime"]
                            };
                            assets.Add(asset);
                        }
                    }
                }
            }

            return assets;
        }

        public bool AddAsset(Asset asset)
        {
            if (asset == null)
            {
                return false;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Assets (Name, Type, Category, Location, InsertedBy, InsertedTime, UpdatedTime) " +
                           "VALUES (@Name, @Type, @Category, @Location, @InsertedBy, @InsertedTime, @UpdatedTime)";

                    // Create a SqlCommand with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameters from the Asset object
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = asset.Name;
                        cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = asset.Type;
                        cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = asset.Category;
                        cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = asset.Location;
                        cmd.Parameters.Add("@InsertedBy", SqlDbType.VarChar).Value = asset.InsertedBy;
                        cmd.Parameters.Add("@InsertedTime", SqlDbType.DateTime).Value = asset.InsertedTime;
                        cmd.Parameters.Add("@UpdatedTime", SqlDbType.DateTime).Value = asset.UpdatedTime;

                        // Open the connection

                        // Execute the query
                        _ = cmd.ExecuteNonQuery();

                        // Close the connection
                        connection.Close();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAsset(string assetId)
        {
            if (assetId == null)
            {
                return false;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = $"DELETE FROM Assets WHERE Id='{assetId}'";

                    // Create a SqlCommand with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Execute the query
                        _ = cmd.ExecuteNonQuery();

                        // Close the connection
                        connection.Close();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertIntoCart(List<Cart> cart)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO cart (EmployeeId, AssetId, Quantity) VALUES";
                foreach (Cart item in cart)
                {
                    query += $"('{item.EmployeeName}','{item.AssetId}','{item.AssetQuantity}'),";
                }
                query = query.TrimEnd(',');
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    _ = command.ExecuteNonQuery();

                    // Close the connection
                    connection.Close();
                }
            }

            return true;
        }
        public bool InsertIntoCart(int Id, string status)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"UPDATE ORDERS SET OrderStatus = '{status}' WHERE OrderId = '{Id}'";
                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    _ = command.ExecuteNonQuery();

                    // Close the connection
                    connection.Close();
                }
            }

            return true;
        }
        public bool DeleteFromCart(string EmployeeName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"DELETE FROM Cart where EmployeeId = '{EmployeeName}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    _ = command.ExecuteNonQuery();

                    // Close the connection
                    connection.Close();
                }
            }

            return true;
        }
        public List<Cart> GetCartItems(string EmployeeName)
        {
            List<Cart> cartItems = new List<Cart>();

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
                            Cart asset = new Cart
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

        public bool insertIntoOrder(Order order)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
            INSERT INTO [dbo].[Orders] (
                [AssetId],
                [EmployeeId],
                [OrderDate],
                [ManagerEmployeeId],
                [ProductFamily],
                [DeliverTo],
                [Country],
                [PhoneNumber],
                [Comments],
                [OfficeLocation],
                [HomeLocation],
                OrderStatus
            ) VALUES (
                @AssetId,
                @EmployeeId,
                GETDATE(),
                @ManagerEmployeeId,
                @ProductFamily,
                @DeliverTo,
                @Country,
                @PhoneNumber,
                @Comments,
                @OfficeLocation,
                @HomeAddress,
                'Waiting For Approval'
            )", connection))
                {
                    _ = command.Parameters.AddWithValue("@AssetId", order.AssetNames); // Assuming this is an int
                    _ = command.Parameters.AddWithValue("@EmployeeId", order.EmployeeId); // Assuming this is an int
                    _ = command.Parameters.AddWithValue("@ManagerEmployeeId", order.ManagerEmployeeId);
                    _ = command.Parameters.AddWithValue("@ProductFamily", order.ProductFamily);
                    _ = command.Parameters.AddWithValue("@DeliverTo", order.DeliverTo);
                    _ = command.Parameters.AddWithValue("@Country", order.Country);
                    _ = command.Parameters.AddWithValue("@PhoneNumber", order.PhoneNumber);
                    _ = command.Parameters.AddWithValue("@Comments", string.IsNullOrEmpty(order.Comments)?"No Comments": order.Comments);
                    _ = command.Parameters.AddWithValue("@OfficeLocation", order.OfficeAddress); // Assuming this is the same as OfficeLocation
                    _ = command.Parameters.AddWithValue("@HomeAddress", order.HomeAddress); // Assuming this is the same as HomeLocation

                    _ = command.ExecuteNonQuery();
                }
            }

            return true;
        }
    }


}
