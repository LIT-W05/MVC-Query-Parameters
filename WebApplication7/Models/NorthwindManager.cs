using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication7.Models
{
    public class NorthwindManager
    {
        private string _connectionString;

        public NorthwindManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Order> GetOrders()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Orders";
            connection.Open();
            List<Order> orders = new List<Order>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Order order = new Order
                {
                    Id = (int)reader["OrderId"],
                    Date = (DateTime)reader["OrderDate"],
                    ShipAddress = (string)reader["ShipAddress"],
                    ShipName = (string)reader["ShipName"]
                };
                //object region = reader["ShipRegion"];
                //if (region != DBNull.Value)
                //{
                //    order.ShipRegion = (string)region;
                //}

                //object shippedDate = reader["ShippedDate"];
                //if (shippedDate != DBNull.Value)
                //{
                //    order.ShippedDate = (DateTime) shippedDate;
                //}

                order.ShipRegion = reader.GetOrNull<string>("ShipRegion");
                order.ShippedDate = reader.GetOrNull<DateTime>("ShippedDate");

                orders.Add(order);
            }

            connection.Close();
            connection.Dispose();
            return orders;
        }

        public IEnumerable<OrderDetail> GetOrderDetailsFor1997()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"select od.* from [Order Details] od
JOIN Orders o 
ON o.OrderID = od.OrderID
WHERE o.OrderDate BETWEEN '01/01/1997' AND '12/31/1997'";
            connection.Open();
            List<OrderDetail> orders = new List<OrderDetail>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                OrderDetail detail = new OrderDetail
                {
                    OrderId = (int)reader["OrderId"],
                    Quantity = (short)reader["Quantity"],
                    UnitPrice = (decimal)reader["UnitPrice"]
                };
                orders.Add(detail);
            }

            connection.Close();
            connection.Dispose();
            return orders;
        }

        public IEnumerable<OrderDetail> GetDetailsForOrder(int orderId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"select * FROM [Order Details] WHERE 
                                OrderId = @orderId";
            cmd.Parameters.AddWithValue("@orderId", orderId);
            connection.Open();
            List<OrderDetail> orders = new List<OrderDetail>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                OrderDetail detail = new OrderDetail
                {
                    OrderId = (int)reader["OrderId"],
                    Quantity = (short)reader["Quantity"],
                    UnitPrice = (decimal)reader["UnitPrice"]
                };
                orders.Add(detail);
            }

            connection.Close();
            connection.Dispose();
            return orders;
        }

        public IEnumerable<Category> GetCategories()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Categories";
            connection.Open();
            List<Category> categories = new List<Category>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(new Category
                {
                    Id = (int)reader["CategoryId"],
                    Description = (string)reader["Description"],
                    Title = (string)reader["CategoryName"]
                });
            }

            connection.Close();
            connection.Dispose();
            return categories;
        }

        public IEnumerable<Product> GetProducts(int categoryId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Products WHERE 
                                CategoryId = @categoryId";
            cmd.Parameters.AddWithValue("@categoryId", categoryId);
            connection.Open();
            List<Product> products = new List<Product>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = (int)reader["ProductId"],
                    UnitPrice = (decimal)reader["UnitPrice"],
                    Name = (string)reader["ProductName"],
                    QuantityPerUnit = (string)reader["QuantityPerUnit"]
                });
            }

            connection.Close();
            connection.Dispose();
            return products;
        }

        public string GetCategoryName(int categoryId)
        {
            //IEnumerable<Category> categories = GetCategories();
            //Category cat = categories.FirstOrDefault(c => c.Id == categoryId);
            //if (cat != null)
            //{
            //    return cat.Title;
            //}

            //return null;

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT TOP 1 CategoryName FROM Categories WHERE " +
                              "CategoryId = @catId";
            cmd.Parameters.AddWithValue("@catId", categoryId);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return (string) reader["CategoryName"];
            }

            return null;
        }

        public IEnumerable<Product> SearchProducts(string searchText)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Products WHERE ProductName LIKE @search";
            cmd.Parameters.AddWithValue("@search", $"%{searchText}%");
            connection.Open();
            List<Product> products = new List<Product>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = (int)reader["ProductId"],
                    UnitPrice = (decimal)reader["UnitPrice"],
                    Name = (string)reader["ProductName"],
                    QuantityPerUnit = (string)reader["QuantityPerUnit"]
                });
            }

            connection.Close();
            connection.Dispose();
            return products;
        }
    }

    public static class ReaderExtensions
    {
        public static T GetOrNull<T>(this SqlDataReader reader, string columnName)
        {
            object obj = reader[columnName];
            if (obj != DBNull.Value)
            {
                return (T)obj;
            }

            return default(T);
        }
    }
}