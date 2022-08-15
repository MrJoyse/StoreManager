using System.Data.SqlClient;
using System.Data;
using System;

namespace StoreManager
{
    /// <summary>
    /// Товар/Продукт
    /// </summary>
    public class Product
    {
        ///<summary>
        /// Поле указывающее был ли создан объект
        ///</summary>
        private static readonly Product instance = new Product();
        /// <summary>
        /// id товара
        /// </summary>
        public int Id_product { get; set; }
        /// <summary>
        /// Наименование товара
        /// </summary>
        public string Product_name { get; set; }
        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal Product_price { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        public decimal Product_count { get; set; }
        /// <summary>
        /// Сумма за товар - цена * кол-во
        /// </summary>
        public decimal Product_summ_price { get; set; }
        /// <summary>
        /// Поставщик товара
        /// </summary>
        public string Provider_name { get; set; }

        /// <summary>
        /// Потокобезопасный Singletone для Product
        /// </summary>
        /// <returns>Поле instance</returns>
        public static Product getInstance()
        {
            return instance;
        }

        /// <summary>
        /// Вставка во временную таблицу продуктов
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="product">товар</param>
        /// <param name="first_part_query">первая часть запроса</param>
        /// <param name="name_table">имя таблицы</param>
        public static void Insert_temp_Product(SqlConnection sqlConnection, Product product, string first_part_query, string name_table)
        {
            if(product.Provider_name == null)
            {
                product.Provider_name = "";
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = first_part_query + name_table + "(name_provider, name_product, count_product, price_product, summ_product) VALUES (@p1,@p2,@p3,@p4,@p5)"
            };
            command.Parameters.AddWithValue("@p1", product.Provider_name);
            command.Parameters.AddWithValue("@p2", product.Product_name);
            command.Parameters.AddWithValue("@p3", product.Product_count);
            if (product.Product_price > 0)
            {
                command.Parameters.AddWithValue("@p4", product.Product_price);
                command.Parameters.AddWithValue("@p5", product.Product_summ_price);
            }
            else
            {
                command.CommandText = first_part_query + name_table + "(name_provider, name_product, count_product) VALUES (@p1,@p2,@p3)";
            }
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
        /// Редактирование временной таблицы товаров
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">имя таблицы</param>
        /// <param name="first_part_query">первая часть запроса</param>
        /// <param name="product">товар</param>
        public static void Edit_Temp_Product(SqlConnection sqlConnection, string name_table, string first_part_query, Product product)
        {
            if (product.Product_price == 0)
            {
                var command = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandText = first_part_query + name_table + " SET name_product = @p1, name_provider = @p2, count_product = @p3, price_product = null, summ_product = null WHERE id_product = @p6"
                };
                command.Parameters.AddWithValue("@p1", product.Product_name);
                command.Parameters.AddWithValue("@p2", product.Provider_name);
                command.Parameters.AddWithValue("@p3", product.Product_count);
                command.Parameters.AddWithValue("@p6", product.Id_product);
                if (sqlConnection.State == ConnectionState.Open)
                {
                    command.ExecuteNonQuery();
                }
                else
                {
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                var command = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandText = first_part_query + name_table + " SET name_product = @p1, name_provider = @p2, count_product = @p3, price_product = @p4, summ_product = @p5 WHERE id_product = @p6"
                };
                command.Parameters.AddWithValue("@p1", product.Product_name);
                command.Parameters.AddWithValue("@p2", product.Provider_name);
                command.Parameters.AddWithValue("@p3", product.Product_count);
                command.Parameters.AddWithValue("@p4", product.Product_price);
                command.Parameters.AddWithValue("@p5", product.Product_summ_price);
                command.Parameters.AddWithValue("@p6", product.Id_product);
                if (sqlConnection.State == ConnectionState.Open)
                {
                    command.ExecuteNonQuery();
                }
                else
                {
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
            }
            sqlConnection.Close();
        }

        /// <summary>
        /// удаление из временной таблицы Products 
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="product">товар</param>
        /// <param name="name_table">имя таблицы</param>
        public static void Delete_temp_Product(SqlConnection sqlConnection, Product product, string first_part_query, string name_table)
        {
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = first_part_query + name_table + " WHERE id_product=" + product.Id_product
            };
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
        /// Запись данных о товарах в основную таблицу используем полученный ранее id договора
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">имя таблицы Products_</param>
        /// <param name="id_contract">id Договора</param>
        public static void Insert_Contract_to_Product(SqlConnection sqlConnection, string name_table, int id_contract)
        {
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "DBCC CHECKIDENT (Products, RESEED, 0) INSERT INTO Products(name_product, count_product, price_product, summ_product, id_contract) SELECT name_product, count_product, price_product, summ_product, id_contract = " + id_contract + "  FROM Products_" + name_table
            };
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
        /// Для редактирования данных договора
        /// Вернуть записи данных о товарах из основной таблицы во временную таблицу используем id договора
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">имя таблицы Products_</param>
        /// <param name="id_contract">id Договора</param>
        public static void Return_from_Products_Contracts_to_Products_temp_Contracts(SqlConnection sqlConnection, string name_table, int id_contract)
        {
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "INSERT INTO Products_" + name_table + "(id_contract, name_product, count_product, price_product, summ_product) " +
                "SELECT id_contract, name_product, count_product, price_product, summ_product  FROM Products WHERE id_contract = @id_contract"
            };
            command.Parameters.AddWithValue("@id_contract", id_contract);
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
        /// Для редактирования данных анкеты на кредит
        /// Вернуть записи данных о товарах из основной таблицы во временную таблицу используем id анкеты 
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">имя таблицы Products_</param>
        /// <param name="id_credit_questionnaire">id анкеты</param>
        public static void Return_from_Products_Credit_to_Products_temp_Credit(SqlConnection sqlConnection, string name_table, int id_credit_questionnaire)
        {
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "INSERT INTO Products_credit_" + name_table + "(id_credit_questionnaire, name_product, count_product, price_product, summ_product) " +
                "SELECT id_credit_questionnaire, name_product, count_product, price_product, summ_product  FROM Products WHERE id_credit_questionnaire = @id_credit_questionnaire"
            };
            command.Parameters.AddWithValue("@id_credit_questionnaire", id_credit_questionnaire);
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
        /// Для редактирования данных заказа
        /// Вернуть записи данных о товарах из основной таблицы во временную таблицу используем id договора
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">имя таблицы Products_</param>
        /// <param name="id_order">id заказа</param>
        public static void Return_from_Products_Orders_to_Products_temp_Orders(SqlConnection sqlConnection, string name_table, int id_order)
        {
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "INSERT INTO Products_order_" + name_table + "(id_order, name_provider, name_product, count_product, price_product, summ_product) " +
                "SELECT id_order, name_provider, name_product, count_product, price_product, summ_product  FROM Products WHERE Products.id_order = " + id_order
            };
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
        /// Для редактирования данных коммерческого предложения
        /// Вернуть записи данных о товарах из основной таблицы во временную таблицу используем id договора
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">имя таблицы Products_</param>
        /// <param name="id_commercial">id предложения</param>
        public static void Return_from_Products_Commercial_to_Products_temp_Commercial(SqlConnection sqlConnection, string name_table, int id_commercial)
        {
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "INSERT INTO Products_commercial_" + name_table + "(id_commercial, name_provider, name_product, count_product, price_product, summ_product) " +
                "SELECT id_commercial, name_provider, name_product, count_product, price_product, summ_product  FROM Products WHERE Products.id_commercial = " + id_commercial
            };
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
        /// Для редактирования данных заказа организации
        /// Вернуть записи данных о товарах из основной таблицы во временную таблицу используем id договора
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">имя таблицы Products_</param>
        /// <param name="id_commercial">id предложения</param>
        public static void Return_from_Products_Order_Firm_to_Products_temp_Commercial(SqlConnection sqlConnection, string name_table, int id_order_firm)
        {
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "INSERT INTO Products_commercial_" + name_table + "(id_commercial, name_provider, name_product, count_product, price_product, summ_product) " +
                "SELECT id_commercial, name_provider, name_product, count_product, price_product, summ_product  FROM Products WHERE Products.id_order_firm = " + id_order_firm
            };
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
        /// Запись данных о товарах в основную таблицу используем полученный ранее id заказа
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">имя таблицы Products_</param>
        /// <param name="id_order">id заказы</param>
        public static void Insert_Order_to_Product(SqlConnection sqlConnection, string name_table, int id_order)
        {
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "DBCC CHECKIDENT (Products, RESEED, 0) INSERT INTO Products(name_provider, name_product, count_product, price_product, summ_product, id_order) SELECT name_provider, name_product, count_product, price_product, summ_product, id_order = " + id_order + "  FROM Products_order_" + name_table
            };
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
        /// Запись данных о товарах в основную таблицу используем полученный ранее id заказа фирмы
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">имя таблицы Products_</param>
        /// <param name="id_order_firm">id заказы</param>
        public static void Insert_Order_Firms_to_Product(SqlConnection sqlConnection, string name_table, int id_order_firm)
        {
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "DBCC CHECKIDENT (Products, RESEED, 0) INSERT INTO Products(name_provider, name_product, count_product, price_product, summ_product, id_order_firm) SELECT name_provider, name_product, count_product, price_product, summ_product, id_order_firm = @id_order_firm  FROM Products_commercial_" + name_table
            };
            command.Parameters.AddWithValue("@id_order_firm", id_order_firm);
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
        /// Запись данных о товарах в основную таблицу используем полученный ранее id кредитной заявки
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">имя временной таблицы</param>
        /// <param name="id_credit_questionnaire">id кредитной заявки</param>
        public static void Insert_Credit_to_Product(SqlConnection sqlConnection, string name_table, int id_credit_questionnaire)
        {
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "DBCC CHECKIDENT (Products, RESEED, 0) INSERT INTO Products(name_product, count_product, price_product, summ_product, id_credit_questionnaire) SELECT name_product, count_product, price_product, summ_product, id_credit_questionnaire = @id_credit_questionnaire  FROM Products_credit_" + name_table
            };
            command.Parameters.AddWithValue("@id_credit_questionnaire", id_credit_questionnaire);
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
        /// Запись данных о товарах в основную таблицу используем полученный ранее id коммерческого предложения
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">постфикс имени таблицы</param>
        /// <param name="id_commercial">id предложения</param>
        public static void Insert_Commercial_to_Product(SqlConnection sqlConnection, string name_table, int id_commercial)
        {
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "DBCC CHECKIDENT (Products, RESEED, 0) INSERT INTO Products(name_product, count_product, price_product, summ_product, id_commercial) SELECT name_product, count_product, price_product, summ_product, id_commercial = " + id_commercial + "  FROM Products_commercial_" + name_table
            };
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
        /// Очистка временной таблицы товаров
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">имя таблицы</param>
        public static void Clear_temp_Product(SqlConnection sqlConnection, string name_table)
        {
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "TRUNCATE TABLE " + name_table
            };
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
        /// Вернуть количество записей во временной таблице товаров (используем для проверки ее на заполненность)
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table">имя таблицы</param>
        /// <returns>Количество записей</returns>
        public static int Select_Count_Records_From_temp_Products(SqlConnection sqlConnection, string name_table)
        {
            int count_records;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "Select COUNT(*) FROM " + name_table
            };
            try
            {
                count_records = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception)
            {
                count_records = 0;
            }            
            sqlConnection.Close();
            return count_records;
        }

        /// <summary>
        /// Определить количество записей равных нулю, во временной таблице товаров
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="name_table"></param>
        /// <returns>Количество записей</returns>
        public static int Select_Count_Null_Records_From_temp_Products(SqlConnection sqlConnection, string name_table)
        {
            int count_records;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "Select COUNT(*) FROM " + name_table + " WHERE summ_product IS NULL"
            };
            try
            {
                count_records = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception)
            {
                count_records = 0;
            }
            sqlConnection.Close();
            return count_records;
        }
    }
}
