using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPSB_DBMS.Model.DataBase.Commands
{
    /// <summary>
    /// Предоставляет методы для выбора значений из Базы Данных
    /// </summary>
    internal static class Select
    {
        /// <summary>
        /// Получает значения списка оборудования из БД
        /// </summary>
        /// <returns>Возвращает <see cref="List{T}"/>, где T - <see cref="Product"/></returns>
        public static List<Product> GetProducts()
        {
            var products = new List<Product>(); 

            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "SELECT * FROM [Product]"
                };

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ID = (int)reader["ID"],
                            Name = (string)reader["Name"],
                            Description = (string)reader["Description"],
                            Category = (string)reader["Category"],
                            Brand = (string)reader["Brand"],
                            Manufacturer = (string)reader["Manufacturer"],
                            Quantity = (int)reader["Quantity"],
                            Price = (decimal)reader["Price"]
                        });
                    }
                }
                finally
                {
                    reader.Close();
                }

                connection.Close();
            }

            return products;
        }

        /// <summary>
        /// Получает оборудование <see cref="Product"/> по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор оборудования</param>
        /// <returns>Возвращает найденное оборудование <see cref="Product"/></returns>
        public static Product GetProduct(in int id)
        {
            Product product = null;

            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT * FROM [Product] WHERE [ID] = {id}"
                };

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        product = new Product
                        {
                            ID = (int)reader["ID"],
                            Name = (string)reader["Name"],
                            Description = (string)reader["Description"],
                            Category = (string)reader["Category"],
                            Brand = (string)reader["Brand"],
                            Manufacturer = (string)reader["Manufacturer"],
                            Quantity = (int)reader["Quantity"],
                            Price = (decimal)reader["Price"]
                        };
                    }
                }
                finally
                {
                    reader.Close();
                }

                connection.Close();
            }

            return product;
        }

        /// <summary>
        /// Получает значения списка клиентов из БД
        /// </summary>
        /// <returns>Возвращает <see cref="List{T}"/>, где T - <see cref="Customer"/></returns>
        public static List<Customer> GetClients()
        {
            var clients = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "SELECT * FROM [Customer]"
                };

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        clients.Add(new Customer
                        {
                            ID = (int)reader["ID"],
                            FullName= (string)reader["FullName"],
                            Phone = (string)reader["Phone"],
                            Email = (string)reader["Email"],
                            Required_services = (string)reader["Required services"]
                        });
                    }
                }
                finally
                {
                    reader.Close();
                }

                connection.Close();
            }

            return clients;
        }

        /// <summary>
        /// Получает клиента <see cref="Customer"/> по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        /// <returns>Возвращает найденного клиента <see cref="Customer"/></returns>
        public static Customer GetClient(in int id)
        {
            Customer client = null;

            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $"SELECT * FROM [Customer] WHERE [ID] = {id}"
                };

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        client = new Customer
                        {
                            ID = (int)reader["ID"],
                            FullName = (string)reader["FullName"],
                            Phone = (string)reader["Phone"],
                            Email = (string)reader["Email"],
                            Required_services = (string)reader["Required services"]
                        };
                    }
                }
                finally
                {
                    reader.Close();
                }

                connection.Close();
            }

            return client;
        }

        /// <summary>
        /// Получает значения списка договоров из БД
        /// </summary>
        /// <returns>Возвращает <see cref="List{T}"/>, где T - <see cref="Contract"/></returns>
        public static List<Contract> GetContracts()
        {
            var contracts = new List<Contract>();

            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = "SELECT * FROM [Contract]"
                };

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        contracts.Add(new Contract((int)reader["CustomerID"], (int)reader["ProductID"])
                        {
                            ID = (int)reader["ID"],
                            Agreement = (string)reader["Agreement"],
                            CustomerID = (int)reader["CustomerID"],
                            ProductID = (int)reader["ProductID"]
                        });
                    }
                }
                finally
                {
                    reader.Close();
                }

                connection.Close();
            }

            return contracts;
        }
    }
}