using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace OPSB_DBMS.Model.DataBase.Commands
{
    /// <summary>
    /// Предоставляет методы для удаления записей из БД
    /// </summary>
    internal static class Delete
    {
        /// <summary>
        /// Выбирает соответствующий метод удаления записей, исходя из типа объектов, которые нужно удалить
        /// </summary>
        /// <param name="handledData">Тип объектов, которые нужно удалить</param>
        /// <param name="removedIDs">Список идентификаторов объектов</param>
        /// <returns>Возвращает количество удалённых объектов</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static int DeleteHandledData(in NameOfData handledData, in IEnumerable<int> removedIDs)
        {
            switch (handledData)
            {
                case NameOfData.Product:
                    return DeleteProducts(removedIDs);
                case NameOfData.Customer:
                    return DeleteClients(removedIDs);
                case NameOfData.Contract:
                    return DeleteContracts(removedIDs);
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Удаляет записи об оборудовании по их идентификаторам
        /// </summary>
        /// <param name="removedIDs">Идентификаторы оборудований</param>
        /// <returns>Возвращает количество удалённых объектов</returns>
        public static int DeleteProducts(in IEnumerable<int> removedIDs)
        {
            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection
                };

                connection.Open();

                foreach (int id in removedIDs)
                {
                    command.CommandText = $"DELETE FROM [Product] WHERE [ID] = {id}";

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            return removedIDs.Count();
        }

        /// <summary>
        /// Удаляет записи о клиентах по их идентификаторам
        /// </summary>
        /// <param name="removedIDs">Идентификаторы клиентов</param>
        /// <returns>Возвращает количество удалённых объектов</returns>
        public static int DeleteClients(in IEnumerable<int> removedIDs)
        {
            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection
                };

                connection.Open();

                foreach (int id in removedIDs)
                {
                    command.CommandText = $"DELETE FROM [Customer] WHERE [ID] = {id}";

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            return removedIDs.Count();
        }

        /// <summary>
        /// Удаляет записи о договорах по их идентификаторам
        /// </summary>
        /// <param name="removedIDs">Идентификаторы договоров</param>
        /// <returns>Возвращает количество удалённых объектов</returns>
        public static int DeleteContracts(in IEnumerable<int> removedIDs)
        {
            using (SqlConnection connection = new SqlConnection(App.ConnectionString))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection
                };

                connection.Open();

                foreach (int id in removedIDs)
                {
                    command.CommandText = $"DELETE FROM [Contract] WHERE [ID] = {id}";

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            return removedIDs.Count();
        }
    }
}