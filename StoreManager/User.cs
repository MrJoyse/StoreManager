using System;
using System.Data;
using System.Data.SqlClient;

namespace StoreManager
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        ///<summary>
        ///поле указывающее был ли создан объект
        ///</summary>        
        private static readonly User instance = new User();
        private static SqlConnection sqlConnection = DBSQLServerUtils.GetDBConnection();
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string User_name { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Уровень доступа
        /// 1-гость;
        /// 2-менеджер;
        /// 3-администратор.
        /// </summary>
        public int Access_level { get; set; }
        /// <summary>
        /// Доверенность
        /// </summary>
        public string Documents { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string First_name { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string Last_name { get; set; }
        /// <summary>
        /// Дата доверенности
        /// </summary>
        public DateTime Date_documents { get; set; }
        /// <summary>
        /// Склонение ФИО для доверенности
        /// </summary>
        public string Declension { get; set; }
        /// <summary>
        /// Сокращенное ФИО
        /// </summary>
        public string Short_name { get; set; }
        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// id пользователя
        /// </summary>
        public int Id_user { get; set; }

        /// <summary>
        /// Потокобезопасный Singletone для User
        /// </summary>
        /// <returns>состояние</returns>
        public static User getInstance()
        {
            return instance;
        }

        public static User Copy(User user, User user_change)
        {
            user.User_name = user_change.User_name;
            user.Password = user_change.Password;
            user.Access_level = user_change.Access_level;
            user.Documents = user_change.Documents;
            user.Surname = user_change.Surname;
            user.First_name = user_change.First_name;
            user.Last_name = user_change.Last_name;
            user.Date_documents = user_change.Date_documents;
            user.Declension = user_change.Declension;
            user.Short_name = user_change.Short_name;
            user.Position = user_change.Position;
            user.Id_user = user_change.Id_user;
            return user;
        }

        /// <summary>
        /// Получить информацию о пользователе
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <returns>user пользователь</returns>
        public static User get_user_info(User user)
        {
            var command = new SqlCommand();
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }            
            command.Connection = sqlConnection;
            //получить значение уровня доступа для определенных логина и пароля
            command.CommandText = "Select * From User_list WHERE user_name=@user_name AND password=@password";
            command.Parameters.AddWithValue("@user_name", user.User_name);
            command.Parameters.AddWithValue("@password", user.Password);
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    user.Id_user = (int)dataReader["id_user"];
                    user.Access_level = (int)dataReader["access_level"];
                    user.Surname = (string)dataReader["surname"];
                    user.First_name = (string)dataReader["first_name"];
                    user.Last_name = (string)dataReader["last_name"];
                    user.Documents = (string)dataReader["documents"];
                    user.Date_documents = (DateTime)dataReader["date_documents"];
                    user.Declension = (string)dataReader["declension"];
                    user.Short_name = (string)dataReader["short_name"];
                    user.Position = (string)dataReader["position"];
                }
                dataReader.Close();
            }
            sqlConnection.Close();
            return user;
        }

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <param name="procedure_name">имя процедуры</param>
        public static void Insert_to_User_List(           
            User user,
            string procedure_name
            )
        {
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = procedure_name
            };
            command.Parameters.AddWithValue("@user_name", user.User_name);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@access_level", user.Access_level);
            command.Parameters.AddWithValue("@documents", user.Documents);
            command.Parameters.AddWithValue("@surname", user.Surname);
            command.Parameters.AddWithValue("@first_name", user.First_name);
            command.Parameters.AddWithValue("@last_name", user.Last_name);
            command.Parameters.AddWithValue("@date_documents", user.Date_documents);
            command.Parameters.AddWithValue("@declension", user.Declension);
            command.Parameters.AddWithValue("@short_name", user.Short_name);
            if (sqlConnection.State == ConnectionState.Open)
            {
                command.ExecuteNonQuery();
            }
            else
            {
                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
            sqlConnection.Close();
        }

        /// <summary>
        /// Изменить пользователя
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <param name="procedure_name">имя процедуры</param>
        public static void Edit_User(
            User user,
            string procedure_name
            )
        {
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = procedure_name
            };
            command.Parameters.AddWithValue("@id_user", user.Id_user);
            command.Parameters.AddWithValue("@user_name", user.User_name);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@access_level", user.Access_level);
            command.Parameters.AddWithValue("@documents", user.Documents);
            command.Parameters.AddWithValue("@surname", user.Surname);
            command.Parameters.AddWithValue("@first_name", user.First_name);
            command.Parameters.AddWithValue("@last_name", user.Last_name);
            command.Parameters.AddWithValue("@date_documents", user.Date_documents);
            command.Parameters.AddWithValue("@declension", user.Declension);
            command.Parameters.AddWithValue("@short_name", user.Short_name);        
            if (sqlConnection.State == ConnectionState.Open)
            {
                command.ExecuteNonQuery();
            }
            else
            {
                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
            sqlConnection.Close();
        }
    }
}
