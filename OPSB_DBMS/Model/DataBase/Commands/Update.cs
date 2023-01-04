using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace OPSB_DBMS.Model.DataBase.Commands
{
    /// <summary>
    /// Предоставляет методы для обновления записей в БД
    /// </summary>
    internal static class Update
    {
        /// <summary>
        /// Выбирает соответствующий метод обновления записей, исходя из типа объектов, которые нужно обновить
        /// </summary>
        /// <param name="handledDatas">Тип объектов, которые нужно обновить</param>
        /// <returns>Возвращает количество обновлённых объектов</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public static int UpdateHandledData(in IEnumerable<ObservableType> handledDatas)
        {
            switch (handledDatas.First().nameOfData)
            {
                case NameOfData.Product:
                    return UpdateProducts(handledDatas.Cast<Product>());
                case NameOfData.Customer:
                    return UpdateClients(handledDatas.Cast<Customer>());
                case NameOfData.Contract:
                    return UpdateContracts(handledDatas.Cast<Contract>());
                default:
                    throw new System.NotImplementedException();
            }
        }

        /// <summary>
        /// Обновляет значения оборудования <see cref="Product"/>
        /// </summary>
        /// <param name="products">Список изменённого оборудования</param>
        /// <returns>Возвращает количество обновлённых объектов</returns>
        public static int UpdateProducts(in IEnumerable<Product> products)
        {
            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                };

                command.Parameters.Add("@ID", SqlDbType.Int);
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
                        command.Parameters["@ID"].Value = product.ID;
                        command.Parameters["@Name"].Value = product.Name;
                        command.Parameters["@Description"].Value = product.Description;
                        command.Parameters["@Category"].Value = product.Category;
                        command.Parameters["@Brand"].Value = product.Brand;
                        command.Parameters["@Manufacturer"].Value = product.Manufacturer;
                        command.Parameters["@Quantity"].Value = product.Quantity;
                        command.Parameters["@Price"].Value = product.Price;

                        command.CommandText =
                        "UPDATE [Product] SET " +
                        $"[Name] = @Name," +
                        $"[Description] = @Description," +
                        $"[Category] = @Category," +
                        $"[Brand] = @Brand," +
                        $"[Manufacturer] = @Manufacturer," +
                        $"[Quantity] = @Quantity," +
                        $"[Price] = @Price " +
                        $"WHERE [ID] = @ID";

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
        /// Обновляет значения клиентов <see cref="Customer"/>
        /// </summary>
        /// <param name="clients">Список изменённых клиентов</param>
        /// <returns>Возвращает количество обновлённых объектов</returns>
        public static int UpdateClients(in IEnumerable<Customer> clients)
        {
            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                };

                command.Parameters.Add("@ID", SqlDbType.Int);
                command.Parameters.Add("@FullName", SqlDbType.NVarChar, 256);
                command.Parameters.Add("@Phone", SqlDbType.NVarChar, 11);
                command.Parameters.Add("@Email", SqlDbType.NVarChar, 256);
                command.Parameters.Add("@ReqServices", SqlDbType.NVarChar);

                try
                {
                    connection.Open();

                    foreach (Customer client in clients)
                    {
                        command.Parameters["@ID"].Value = client.ID;
                        command.Parameters["@FullName"].Value = client.FullName;
                        command.Parameters["@Phone"].Value = client.Phone;
                        command.Parameters["@Email"].Value = client.Email;
                        command.Parameters["@ReqServices"].Value = client.Required_services;

                        command.CommandText =
                        "UPDATE [Customer] SET " +
                        $"[FullName] = @FullName," +
                        $"[Phone] = @Phone," +
                        $"[Email] = @Email," +
                        $"[Required services] = @ReqServices " +
                        $"WHERE [ID] = @ID";

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
        /// Обновляет значения договоров <see cref="Contract"/>
        /// </summary>
        /// <param name="contracts">Список изменённых договоров</param>
        /// <returns>Возвращает количество обновлённых объектов</returns>
        public static int UpdateContracts(in IEnumerable<Contract> contracts)
        {
            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                };

                command.Parameters.Add("@ID", SqlDbType.Int);
                command.Parameters.Add("@Agreement", SqlDbType.NVarChar);
                command.Parameters.Add("@CustomerID", SqlDbType.Int);
                command.Parameters.Add("@ProductID", SqlDbType.Int);

                try
                {
                    connection.Open();

                    foreach (Contract contract in contracts)
                    {
                        command.Parameters["@ID"].Value = contract.ID;
                        command.Parameters["@Agreement"].Value = contract.Agreement;
                        command.Parameters["@CustomerID"].Value = contract.CustomerID;
                        command.Parameters["@ProductID"].Value = contract.ProductID;

                        command.CommandText =
                        "UPDATE [Contract] SET " +
                        $"[Agreement] = @Agreement," +
                        $"[CustomerID] = @CustomerID," +
                        $"[ProductID] = @ProductID " +
                        $"WHERE [ID] = @ID";

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