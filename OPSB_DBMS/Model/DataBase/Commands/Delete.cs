using System.Collections.Generic;
using System.Data.SqlClient;

namespace OPSB_DBMS.Model.DataBase.Commands
{
    /// <summary>
    /// Предоставляет методы для удаления записей из БД
    /// </summary>
    internal static class Delete
    {
        /// <summary>
        /// Удаляет записи об оборудовании по их идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы оборудований</param>
        /// <returns>Возвращает количество удалённых объектов</returns>
        public static int DeleteProducts(in List<int> ids)
        {
            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection
                };

                connection.Open();

                for (int i = 0; i < ids.Count; i++)
                {
                    command.CommandText = $"DELETE FROM [Product] WHERE [ID] = {ids[i]}";

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            return ids.Count;
        }
    }
}