using NLog;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.OleDb;

namespace StoreManager
{
    public class DBSQLServerUtils
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Settings _settings = Settings.GetSettings();
       
        /// <summary>
        /// Установка соединения с сервером
        /// </summary>
        /// <returns>SqlConnection connnection или null</returns>
        public static SqlConnection GetDBConnection()
        {
            try
            {
                SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
                connectionStringBuilder.DataSource = _settings.Server_name;
                connectionStringBuilder.InitialCatalog = _settings.Database_name;
                connectionStringBuilder.PersistSecurityInfo = true;
                connectionStringBuilder.UserID = _settings.User_DB_Name;
                connectionStringBuilder.Password = _settings.User_DB_Password;
                connectionStringBuilder.ConnectTimeout = 15;
                SqlConnection connnection = new SqlConnection(connectionStringBuilder.ConnectionString);
                return connnection;
            }
            catch (Exception error)
            {
                logger.Error("Ошибка в DBSQLServerUtils.cs ---> " + error);
                MessageBox.Show("Ошибка установки соединения проверьте файл settings.xml", "Ошибка", MessageBoxButtons.OK,MessageBoxIcon.Error);               
            }
            return null;
        }

        /// <summary>
        /// Установка соединения со старой базой данных договоров
        /// </summary>
        /// <returns>OleDbConnection connnection или null</returns>
        public static OleDbConnection GetDBConnection_Old_Contracts()
        {
            try
            {
                OleDbConnectionStringBuilder connectionStringBuilder = new OleDbConnectionStringBuilder();
                connectionStringBuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
                connectionStringBuilder.DataSource = _settings.Path_Old_DB_Contracts;                
                OleDbConnection connnection = new OleDbConnection(connectionStringBuilder.ConnectionString);
                return connnection;
            }
            catch (Exception error)
            {
                logger.Error("Ошибка в DBSQLServerUtils.cs ---> " + error);
                MessageBox.Show("Ошибка установки соединения проверьте файл settings.xml", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        /// <summary>
        /// Установка соединения со старой базой данных заказов
        /// </summary>
        /// <returns>OleDbConnection connnection или null</returns>
        public static OleDbConnection GetDBConnection_Old_Orders()
        {
            try
            {
                OleDbConnectionStringBuilder connectionStringBuilder = new OleDbConnectionStringBuilder();
                connectionStringBuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
                connectionStringBuilder.DataSource = _settings.Path_Old_DB_Orders;
                OleDbConnection connnection = new OleDbConnection(connectionStringBuilder.ConnectionString);
                return connnection;
            }
            catch (Exception error)
            {
                logger.Error("Ошибка в DBSQLServerUtils.cs ---> " + error);
                MessageBox.Show("Ошибка установки соединения проверьте файл settings.xml", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
    }
}
