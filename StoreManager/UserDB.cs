using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager
{
    class UserDB
    {
        private static SqlConnection _sqlConnection = DBSQLServerUtils.GetDBConnection();        
        //определить информацию о пользователе
        public static User get_user_info(User user)
        {
            var command = new SqlCommand();
            _sqlConnection.Open();
            command.Connection = _sqlConnection;
            //получить значение уровня доступа для определенных логина и пароля
            command.CommandText = "Select * From User_list WHERE user_name=@user_name AND password=@password";
            command.Parameters.AddWithValue("@user_name", user.User_name);
            command.Parameters.AddWithValue("@password", user.Password);
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    if ((int)dataReader["access_level"] <= user.Max_access_level)
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
                    }
                }
                dataReader.Close();
            }
            _sqlConnection.Close();
            return user;
        }
    }
}
