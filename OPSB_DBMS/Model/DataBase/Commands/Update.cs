using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace OPSB_DBMS.Model.DataBase.Commands
{
    /// <summary>
    /// Предоставляет методы для обновления записей в БД
    /// </summary>
    internal static class Update
    {
        /// <summary>
        /// Обновляет значения оборудования <see cref="Product"/>
        /// </summary>
        /// <param name="products">Список изменённого оборудования</param>
        /// <returns>Возвращает количество обновлённых элементов</returns>
        public static int UpdateProducts(in List<Product> products)
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

                    for (int i = 0; i < products.Count; i++)
                    {
                        command.Parameters["@ID"].Value = products[i].ID;
                        command.Parameters["@Name"].Value = products[i].Name;
                        command.Parameters["@Description"].Value = products[i].Description;
                        command.Parameters["@Category"].Value = products[i].Category;
                        command.Parameters["@Brand"].Value = products[i].Brand;
                        command.Parameters["@Manufacturer"].Value = products[i].Manufacturer;
                        command.Parameters["@Quantity"].Value = products[i].Quantity;
                        command.Parameters["@Price"].Value = products[i].Price;

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

            return products.Count;
        }
    }
}