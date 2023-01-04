using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace OPSB_DBMS.Model.DataBase.Commands
{
    /// <summary>
    /// Предоставляет методы для вставки значений в БД
    /// </summary>
    internal static class Insert
    {
        /// <summary>
        /// Выбирает соответствующий метод вставки записей, исходя из типа объектов, которые нужно вставить
        /// </summary>
        /// <param name="handledDatas">Объекты, которые нужно вставить</param>
        /// <returns>Возвращает количество вставленных объектов</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public static int InsertHandledData(in IEnumerable<ObservableType> handledDatas)
        {
            switch (handledDatas.First().nameOfData)
            {
                case NameOfData.Product:
                    return InsertProducts(handledDatas.Cast<Product>());
                case NameOfData.Customer:
                    return InsertClients(handledDatas.Cast<Customer>());
                case NameOfData.Contract:
                    return InsertContracts(handledDatas.Cast<Contract>());
                default:
                    throw new System.NotImplementedException();
            }
        }

        /// <summary>
        /// Вставляет в БД список оборудования
        /// </summary>
        /// <param name="products">Список оборудования</param>
        /// <returns>Возвращает количество добавленных в БД объектов</returns>
        public static int InsertProducts(in IEnumerable<Product> products)
        {
            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = ""
                };

                command.Parameters.Add("@Name", SqlDbType.NVarChar, 30);
                command.Parameters.Add("@Description", SqlDbType.NVarChar);
                command.Parameters.Add("@Category", SqlDbType.NVarChar, 10);
                command.Parameters.Add("@Brand", SqlDbType.NVarChar, 30);
                command.Parameters.Add("@Manufacturer", SqlDbType.NVarChar, 30);
                command.Parameters.Add("@Quantity", SqlDbType.Int);
                command.Parameters.Add("@Price", SqlDbType.Money);

                try
                {
                    connection.Open();

                    foreach (Product product in products)
                    {
                        command.Parameters["@Name"].Value = product.Name;
                        command.Parameters["@Description"].Value = product.Description;
                        command.Parameters["@Category"].Value = product.Category;
                        command.Parameters["@Brand"].Value = product.Brand;
                        command.Parameters["@Manufacturer"].Value = product.Manufacturer;
                        command.Parameters["@Quantity"].Value = product.Quantity;
                        command.Parameters["@Price"].Value = product.Price;

                        command.CommandText = "INSERT INTO [Product] VALUES (@Name, @Description, @Category, @Brand, @Manufacturer, @Quantity, @Price)";

                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            return products.Count();
        }

        /// <summary>
        /// Вставляет в БД список клиентов
        /// </summary>
        /// <param name="clients">Список клиентов</param>
        /// <returns>Возвращает количество добавленных в БД объектов</returns>
        internal static int InsertClients(IEnumerable<Customer> clients)
        {
            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand("", connection);

                command.Parameters.Add("@FullName", SqlDbType.NVarChar, 256);
                command.Parameters.Add("@Phone", SqlDbType.NVarChar, 11);
                command.Parameters.Add("@Email", SqlDbType.NVarChar, 256);
                command.Parameters.Add("@ReqServices", SqlDbType.NVarChar);

                try
                {
                    connection.Open();

                    foreach (Customer client in clients)
                    {
                        command.Parameters["@FullName"].Value = client.FullName;
                        command.Parameters["@Phone"].Value = client.Phone;
                        command.Parameters["@Email"].Value = client.Email;
                        command.Parameters["@ReqServices"].Value = client.Required_services;

                        command.CommandText = "INSERT INTO [Customer] VALUES (@FullName, @Phone, @Email, @ReqServices)";

                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            return clients.Count();
        }

        /// <summary>
        /// Вставляет в БД список договоров
        /// </summary>
        /// <param name="contracts">Список договоров</param>
        /// <returns>Возвращает количество добавленных в БД объектов</returns>
        internal static int InsertContracts(IEnumerable<Contract> contracts)
        {
            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand("", connection);

                command.Parameters.Add("@Agreement", SqlDbType.NVarChar);
                command.Parameters.Add("@CustomerID", SqlDbType.Int);
                command.Parameters.Add("@ProductID", SqlDbType.Int);

                try
                {
                    connection.Open();

                    foreach (Contract contract in contracts)
                    {
                        command.Parameters["@Agreement"].Value = contract.Agreement;
                        command.Parameters["@CustomerID"].Value = contract.CustomerID;
                        command.Parameters["@ProductID"].Value = contract.ProductID;

                        command.CommandText = "INSERT INTO [Contract] VALUES (@Agreement, @CustomerID, @ProductID)";

                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            return contracts.Count();
        }
    }
}