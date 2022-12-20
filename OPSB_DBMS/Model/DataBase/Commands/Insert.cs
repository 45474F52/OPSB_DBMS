using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OPSB_DBMS.Model.DataBase.Commands
{
    /// <summary>
    /// Предоставляет методы для вставки значений в БД
    /// </summary>
    internal static class Insert
    {
        /// <summary>
        /// Вставляет в БД список оборудования
        /// </summary>
        /// <param name="products">Список оборудования</param>
        /// <returns>Возвращает количество добавленных в БД значений</returns>
        public static int InsertProducts(in List<Product> products)
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

                    for (int i = 0; i < products.Count; i++)
                    {
                        command.Parameters["@Name"].Value = products[i].Name;
                        command.Parameters["@Description"].Value = products[i].Description;
                        command.Parameters["@Category"].Value = products[i].Category;
                        command.Parameters["@Brand"].Value = products[i].Brand;
                        command.Parameters["@Manufacturer"].Value = products[i].Manufacturer;
                        command.Parameters["@Quantity"].Value = products[i].Quantity;
                        command.Parameters["@Price"].Value = products[i].Price;

                        command.CommandText = "INSERT INTO [Product] VALUES (@Name, @Description, @Category, @Brand, @Manufacturer, @Quantity, @Price)";

                        command.ExecuteNonQuery();
                    }

                }
                finally
                {
                    connection.Close();
                }
            }

            return products.Count;
        }
    }
}