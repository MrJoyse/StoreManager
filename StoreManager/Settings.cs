using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using NLog;

namespace StoreManager
{
    /// <summary>
    /// Настройки settings.xml
    /// </summary>
    public class Settings
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Имя файла настроек
        /// </summary>
        public static string settingsFile = Name_pc + "settings.xml";
        private string User_settingsFile;   
        public string Get_user_settingsFile()
        {
            User_settingsFile = Name_pc + "settings.xml";
            return User_settingsFile;
        }
        /// <summary>
        /// Имя компьютера - сервера БД
        /// </summary>
        public string Server_name { get; set; }

        /// <summary>
        /// Имя БД
        /// </summary>
        public string Database_name { get; set; }

        /// <summary>
        /// Логин пользователя БД
        /// </summary>
        public string User_DB_Name { get; set; }

        /// <summary>
        /// Пароль пользователя БД
        /// </summary>
        public string User_DB_Password { get; set; }

        /// <summary>
        /// Путь к базе данных архивных договоров
        /// </summary>
        public string Path_Old_DB_Contracts { get; set; }

        /// <summary>
        /// Путь к базе данных архивных заказов
        /// </summary>
        public string Path_Old_DB_Orders { get; set; }

        /// <summary>
        /// Срок исполнения договора
        /// </summary>
        public string Period_of_execution { get; set; }

        /// <summary>
        /// Минимальный процент аванса для заказов и окон без отсрочки
        /// </summary>
        public decimal Min_Prepayment_Percent_order { get; set; }

        /// <summary>
        /// Минимальный процент аванса для рассрочек и окон с отсрочкой
        /// </summary>
        public decimal Min_Prepayment_Percent_contract_deffered { get; set; }

        /// <summary>
        /// Путь к шаблону договора с отсрочкой платежа
        /// </summary>
        public string Path_template_contract { get; set; }

        /// <summary>
        /// Путь к папке договоров отсрочки без отсрочки платежа
        /// </summary>
        public string Path_save_contract { get; set; }

        /// <summary>
        /// Путь к шаблону договора установки окон без отсрочки платежа
        /// </summary>
        public string Path_template_window { get; set; }

        /// <summary>
        /// Путь к папке договоров установки окон без отсрочки платежа
        /// </summary>
        public string Path_save_window { get; set; }

        /// <summary>
        /// Папка для сохранения кредитных документов Белинвестбанка
        /// </summary>
        public string Path_save_credit_belinvest { get; set; }

        /// <summary>
        /// Путь к шаблону анкеты-заявки на кредит Белинвестбанка
        /// </summary>
        public string Path_template_questionnaire_belinvest { get; set; }

        /// <summary>
        /// Путь к шаблону согласия проверки кредитной истории для кредита Белинвестбанка
        /// </summary>
        public string Path_template_consent_story_belinvest { get; set; }

        /// <summary>
        /// Путь к шаблону согласия передачи информации третьим лицам
        /// </summary>
        public string Path_template_consent_transfer_belinvest { get; set; }

        /// <summary>
        /// Путь к шаблону согласия проверки по базе ФСЗН
        /// </summary>
        public string Path_template_consent_pension_belinvest { get; set; }

        /// <summary>
        /// Путь к шаблону договора поставки окон без отсрочки платежа
        /// </summary>
        public string Path_template_supply { get; set; }

        /// <summary>
        /// Путь к папке договоров поставки окон без отсрочки платежа
        /// </summary>
        public string Path_save_supply { get; set; }

        /// <summary>
        /// Путь к шаблону договора установки окон с отсрочкой платежа
        /// </summary>
        public string Path_template_window_with_credit { get; set; }

        /// <summary>
        /// Путь к папке договоров установки окон с отсрочкой платежа
        /// </summary>
        public string Path_save_window_with_credit { get; set; }

        /// <summary>
        /// Путь к шаблону договора поставки окон с отсрочкой платежа
        /// </summary>
        public string Path_template_supply_with_credit { get; set; }

        /// <summary>
        /// Путь к папке договоров поставки окон с отсрочкой платежа
        /// </summary>
        public string Path_save_supply_with_credit { get; set; }

        /// <summary>
        /// Путь к папке заказов
        /// </summary>
        public string Path_save_order { get; set; }

        /// <summary>
        /// Путь к шаблону бланка заказа
        /// </summary>
        public string Path_template_order { get; set; }

        /// <summary>
        /// Путь к шаблону бланка заказа организации
        /// </summary>
        public string Path_template_order_firm { get; set; }
        
        /// <summary>
        /// Путь к шаблону договора аренды
        /// </summary>
        public string Path_template_rent { get; set; }

        /// <summary>
        /// Путь к папке договоров аренды
        /// </summary>
        public string Path_save_rent { get; set; }

        /// <summary>
        /// Путь к шаблону коммерческого предложения
        /// </summary>
        public string Path_template_commercial { get; set; }


        /// <summary>
        /// Путь к папке коммерческих предложений
        /// </summary>
        public string Path_save_commercial { get; set; }

        /// <summary>
        /// Путь к файлу справки
        /// </summary>
        public string Path_help_document { get; set; }

        /// <summary>
        /// Имя таблицы БД
        /// </summary>
        public string Name_table { get; set; }

        /// <summary>
        /// Имя компьютера
        /// </summary>
        public static string Name_pc { get { return Name_pc = Environment.MachineName; } set { } }

        /// <summary>
        /// Срок поставки
        /// </summary>
        public string Delivery_terms { get; set; }

        /// <summary>
        /// Страна по умолчанию
        /// </summary>
        public string Country_Default { get; set; }

        /// <summary>
        /// Область по умолчанию
        /// </summary>
        public string Region_Default { get; set; }

        /// <summary>
        /// Район по умолчанию
        /// </summary>
        public string Area_Default { get; set; }

        /// <summary>
        /// Населенный пункт по умолчанию
        /// </summary>
        public string City_Default { get; set; }

        /// <summary>
        /// Код города по умолчанию
        /// </summary>
        public string Home_Phone_Code_Default { get; set; }

        /// <summary>
        /// Тип улицы по умолчанию
        /// </summary>
        public string Street_variant_Default { get; set; }

        /// <summary>
        /// Закладка номер договора
        /// </summary>
        public string Bookmarks_Number_Contract { get; set; }

        /// <summary>
        /// Закладка дата оформления
        /// </summary>
        public string Bookmarks_Date_Of_Signing { get; set; }

        /// <summary>
        /// Закладка склонение ФИО
        /// </summary>
        public string Bookmarks_Declension { get; set; }

        /// <summary>
        /// Закладка доверенность
        /// </summary>
        public string Bookmarks_Documents { get; set; }

        /// <summary>
        /// Закладка дата доверенности
        /// </summary>
        public string Bookmarks_Date_Documents { get; set; }

        /// <summary>
        /// Закладка фамилия
        /// </summary>
        public string Bookmarks_Surname { get; set; }

        /// <summary>
        /// Закладка имя
        /// </summary>
        public string Bookmarks_First_Name { get; set; }

        /// <summary>
        /// Закладка отчество
        /// </summary>
        public string Bookmarks_Last_Name { get; set; }

        /// <summary>
        /// Закладка полный адрес прописки
        /// </summary>
        public string Bookmarks_Full_Adress { get; set; }

        /// <summary>
        /// Закладка фамилия покупателя для реквизитов в договоре
        /// </summary>
        public string Bookmarks_Surname_Signing { get; set; }

        /// <summary>
        /// Закладка имя покупателя для реквизитов в договоре
        /// </summary>
        public string Bookmarks_First_Name_Signing { get; set; }

        /// <summary>
        /// Закладка отчество покупателя для реквизитов в договоре
        /// </summary>
        public string Bookmarks_Last_Name_Signing { get; set; }

        /// <summary>
        /// Закладка серия паспорта
        /// </summary>
        public string Bookmarks_Serial_Passport { get; set; }

        /// <summary>
        /// Закладка номер паспорта
        /// </summary>
        public string Bookmarks_Number_Passport { get; set; }

        /// <summary>
        /// Закладка полный адрес прописки для реквизитов в договоре
        /// </summary>
        public string Bookmarks_Full_Adress_Shopper_Signing { get; set; }

        /// <summary>
        /// Закладка дата выдачи паспорта
        /// </summary>
        public string Bookmarks_Date_Of_Issue_Passport { get; set; }

        /// <summary>
        /// Закладка орган выдавший паспорт
        /// </summary>
        public string Bookmarks_Department_Name_Passport { get; set; }

        /// <summary>
        /// Закладка номер мобильного телефона
        /// </summary>
        public string Bookmarks_Mobile_Phone { get; set; }

        /// <summary>
        /// Закладка номер домашнего телефона
        /// </summary>
        public string Bookmarks_Home_Phone { get; set; }

        /// <summary>
        /// Закладка аванс
        /// </summary>
        public string Bookmarks_Prepayment { get; set; }

        /// <summary>
        /// Закладка период поставки
        /// </summary>
        public string Bookmarks_Period_Of_Execution { get; set; }

        /// <summary>
        /// Закладка ФИО сокращенное
        /// </summary>
        public string Bookmarks_Name_Shopper_Signing_Abbreviated { get; set; }

        /// <summary>
        /// Закладка полный адрес проживания
        /// </summary>
        public string Bookmarks_Full_Adress_Residence { get; set; }

        /// <summary>
        /// Закладка срок аренды
        /// </summary>
        public string Bookmarks_Rental_Period { get; set; }

        /// <summary>
        /// Закладка стоимость аренды/сутки
        /// </summary>
        public string Bookmarks_Rental_Price { get; set; }

        /// <summary>
        /// Закладка наименование арендуемого инструмента
        /// </summary>
        public string Bookmarks_Name_Rented_Instrument { get; set; }

        /// <summary>
        /// Закладка сумма заказа
        /// </summary>
        public string Bookmarks_Summ_Order { get; set; }

        /// <summary>
        /// Форма оплаты заказа организации
        /// </summary>
        public string Bookmarks_Form_of_Payment { get { return Bookmarks_Form_of_Payment = "form_of_payment"; } set { } }

        /// <summary>
        /// Закладка копия для покупателя
        /// </summary>
        public string Bookmarks_Copy { get; set; }

        /// <summary>
        /// Закладка сумма коммерческого предложения
        /// </summary>
        public string Bookmarks_Summ_Сommercial { get; set; }

        /// <summary>
        /// Закладка сумма договора
        /// </summary>
        public string Bookmarks_Summ_Contract { get; set; }

        /// <summary>
        /// Имя пользователя в подписи
        /// </summary>
        public string Bookmarks_Name_User_Signing_Abbreviated { get { return Bookmarks_Number_Order_Firm = "name_user_signing_abbreviated"; } set { } }

        ///****************************************Коммерческое предложение Заказ************************************************

        /// <summary>
        /// Номер заказа организации
        /// </summary>
        public string Bookmarks_Number_Order_Firm { get { return Bookmarks_Number_Order_Firm = "number_order_firm"; } set { } }

        /// <summary>
        /// Дата заказа организации
        /// </summary>
        public string Bookmarks_Date_Order_Firm { get { return Bookmarks_Date_Order_Firm = "date_order_firm"; } set { } }

        /// <summary>
        /// Наименование организации
        /// </summary>
        public string Bookmarks_Firm_Name { get { return Bookmarks_Firm_Name = "firm_name"; } set { } }

        /// <summary>
        /// Фамилия сотрудника организации
        /// </summary>
        public string Bookmarks_Surname_Employee { get { return Bookmarks_Surname_Employee = "surname_employee"; } set { } }

        /// <summary>
        /// Имя сотрудника организации
        /// </summary>
        public string Bookmarks_First_Name_Employee { get { return Bookmarks_First_Name_Employee = "first_name_employee"; } set { } }

        /// <summary>
        /// Отчество сотрудника организации
        /// </summary>
        public string Bookmarks_Last_Name_Employee { get { return Bookmarks_First_Name_Employee = "last_name_employee"; } set { } }

        /// <summary>
        /// Номер мобильного телефона сотрудника
        /// </summary>
        public string Bookmarks_Mobile_Phone_Employee { get { return Bookmarks_Mobile_Phone_Employee = "mobile_phone_employee"; } set { } }

        /// <summary>
        /// Номер факса фирмы
        /// </summary>
        public string Bookmarks_Fax_Number { get { return Bookmarks_Fax_Number = "fax"; } set { } }

        /// <summary>
        /// Номер телефона по месту работы
        /// </summary>
        public string Bookmarks_Work_Phone { get { return Bookmarks_Work_Phone = "work_phone"; } set { } }

        /// <summary>
        /// Аванс организации
        /// </summary>
        public string Bookmarks_Prepayment_Firm { get { return Bookmarks_Work_Phone = "prepayment_firm"; } set { } }

        /// <summary>
        /// Закладка сумма заказа организации
        /// </summary>
        public string Bookmarks_Summ_Order_Firm { get { return Bookmarks_Work_Phone = "summ_order_firm"; } set { } }

        /// <summary>
        /// Форма оплаты заказа организации
        /// </summary>
        public string Bookmarks_Form_of_Payment_Firm { get { return Bookmarks_Form_of_Payment_Firm = "form_of_payment_firm"; } set { } }

        /// <summary>
        /// Имя пользователя принявшего заказ
        /// </summary>
        public string Bookmarks_User_Name_Order_Firm { get { return Bookmarks_User_Name_Order_Firm = "user_name_order_firm"; } set { } }

        /// <summary>
        /// Имя сотрудника организации для заказа
        /// </summary>
        public string Bookmarks_Name_Employee_Order_Firm { get { return Bookmarks_Name_Employee_Order_Firm = "employee_firm_name"; } set { } }

        ///*****************************************Кредит Белинвестбанка********************************************************
        /// <summary>
        /// Номер заявки на кредит
        /// </summary>
        public string Bookmarks_Number_Credit_Questionnaire { get { return Bookmarks_Number_Credit_Questionnaire = "number_credit_questionnaire"; } set { } }

        /// <summary>
        /// Дата заявки на кредит
        /// </summary>
        public string Bookmarks_Date_Credit_Questionnaire { get; set; }

        /// <summary>
        /// Наименование товара (работы, услуги)
        /// </summary>
        public string Bookmarks_Type_Products { get; set; }

        /// <summary>
        /// Полная стоимость товара (работы, услуги) белорусских рублей
        /// </summary>
        public string Bookmarks_Summ_Price_Products_Credit { get; set; }

        /// <summary>
        /// Участие собственными средствами белорусских рублей
        /// </summary>
        public string Bookmarks_Prepayment_Credit { get; set; }

        /// <summary>
        /// Сумма кредита белорусских рублей
        /// </summary>
        public string Bookmarks_Summ_Credit { get; set; }

        /// <summary>
        /// Индекс
        /// </summary>
        public string Bookmarks_Postcode { get; set; }

        /// <summary>
        /// Место жительства.
        /// </summary>
        public string Bookmarks_Region { get; set; }
        public string Bookmarks_Area { get; set; }
        public string Bookmarks_City { get; set; }
        public string Bookmarks_Street_variant { get; set; }
        public string Bookmarks_Street { get; set; }
        public string Bookmarks_House { get; set; }
        public string Bookmarks_House_building { get; set; }
        public string Bookmarks_Apartment { get; set; }
        public string Bookmarks_Postcode_Residence { get; set; }
        public string Bookmarks_Region_Residence { get; set; }
        public string Bookmarks_Area_Residence { get; set; }
        public string Bookmarks_City_Residence { get; set; }
        public string Bookmarks_Street_variant_Residence { get; set; }
        public string Bookmarks_Street_Residence { get; set; }
        public string Bookmarks_House_Residence { get; set; }
        public string Bookmarks_House_building_Residence { get; set; }
        public string Bookmarks_Apartment_Residence { get; set; }
        public string Bookmarks_Home_Phone_Residence { get; set; }
        public string Bookmarks_Date_Credit_History { get; set; }
        public string Bookmarks_Credit_Payments { get; set; }
        public string Bookmarks_Credit_Currency_BYN { get; set; }
        public string Bookmarks_Credit_Currency_USD { get; set; }
        public string Bookmarks_Credit_Currency_EUR { get; set; }
        public string Bookmarks_Credit_Currency_RUB { get; set; }
        public string Bookmarks_Overdraft_True { get; set; }
        public string Bookmarks_Overdraft_Not { get; set; }
        public string Bookmarks_Overdraft_Payments { get; set; }
        public string Bookmarks_Contact_Person_Mobile_Phone { get; set; }
        public string Bookmarks_Contact_Person_Full_Name { get; set; }
        public string Bookmarks_Contact_Person_Status { get; set; }



        /// <summary>
        /// Составить имя временной таблицы
        /// </summary>
        /// <returns>Name_table</returns>
        public string Get_name_temp_table()
        {           
            User user = User.getInstance();
            //получаем имя компьютера
            Name_pc = Environment.MachineName;
            logger.Info("Имя компьютера: " + Name_pc);
            //Name_pc.Replace("-", "_") заменяется недопустимый символ в имени компьютера
            //имя таблицы включает имя компьютера и имя пользователя базы данных
            Name_table = "temp_" + Name_pc.Replace("-", "_") + "_" + user.User_name;
            logger.Info("Имя таблицы: " + Name_table);
            return Name_table;
        }

        public Settings()
        {            
        }

        /// <summary>
        /// Считать настройки
        /// </summary>
        /// <returns></returns>
        public static Settings GetSettings()
        {            
            Settings settings = null;
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                logger.Info("Попытка считать настройки");
                settings = GetUserSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неудачная попытка загрузки файла текущих настроек. \nПытаемся загрузить настройки по умолчанию...\n" + ex.Message, "Ошибка");
                settings = GetDefaultSettings();
            }
            return settings;
        }

        /// <summary>
        /// Получить настройки из файла settings.xml
        /// </summary>
        /// <returns>settings</returns>
        public static Settings GetDefaultSettings()
        {            
            Settings settings = null;
            if (File.Exists(settingsFile))
            {
                using FileStream fs = new FileStream(settingsFile, FileMode.Open);
                XmlSerializer xml = new XmlSerializer(typeof(Settings));
                settings = (Settings)xml.Deserialize(fs);                
                fs.Close();
            }
            else
            {
                settings = new Settings();
            }
            return settings;
        }

        /// <summary>
        /// Настройки на текущем компьютере
        /// </summary>
        /// <returns></returns>
        public static Settings GetUserSettings()
        {
            Settings settings = null;
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Info("Загрузка настроек пользователя : " + settingsFile);
            if (File.Exists(settingsFile))
            {               
                using FileStream fs = new FileStream(settingsFile, FileMode.Open);
                XmlSerializer xml = new XmlSerializer(typeof(Settings));
                settings = (Settings)xml.Deserialize(fs);
                fs.Close();
                logger.Info("Файл настроек считан и закрыт");
            }
            else
            {
                logger.Error("Файл настроек пользователя не найден");
                settings = new Settings();
            }
            return settings;
        }

        /// <summary>
        /// Сохранить настройки
        /// </summary>
        public void Save()
        {
            if (File.Exists(settingsFile))
            {
                File.Delete(settingsFile);
            }
            using FileStream fs = new FileStream(settingsFile, FileMode.Create);
            XmlSerializer xml = new XmlSerializer(typeof(Settings));
            xml.Serialize(fs, this);
            fs.Close();
        }

        /// <summary>
        /// Проверить существование папок для сохранения
        /// </summary>
        /// <returns></returns>
        public static bool InitFolder()
        {
            int error = 0;
            bool result = true;
            string message_folder = "";
            string message_template = "";
            Settings settings = Settings.GetSettings();
            ArrayList path_save_array = new ArrayList();
            path_save_array.Add(settings.Path_save_contract);
            path_save_array.Add(settings.Path_save_window);
            path_save_array.Add(settings.Path_save_window_with_credit);
            path_save_array.Add(settings.Path_save_supply);
            path_save_array.Add(settings.Path_save_supply_with_credit);
            path_save_array.Add(settings.Path_save_order);
            path_save_array.Add(settings.Path_save_rent);
            path_save_array.Add(settings.Path_save_commercial);

            foreach (var path_save in path_save_array)
            {
                if (!Directory.Exists(path_save.ToString()))
                {
                    message_folder += "Ошибка сохранения документов. Нет доступа к папке " + path_save.ToString() + "\n";
                    error++;
                }
            }
                        
            ArrayList path_template_array = new ArrayList();
            path_template_array.Add(settings.Path_template_contract);
            path_template_array.Add(settings.Path_template_window);
            path_template_array.Add(settings.Path_template_window_with_credit);
            path_template_array.Add(settings.Path_template_supply);
            path_template_array.Add(settings.Path_template_supply_with_credit);
            path_template_array.Add(settings.Path_template_order);
            path_template_array.Add(settings.Path_template_rent);
            path_template_array.Add(settings.Path_template_commercial);

            foreach (var path_template in path_template_array)
            {
                if (!File.Exists(path_template.ToString()))
                {
                    message_template += "Ошибка проверки существования шаблона. Нет доступа к шаблону " + path_template.ToString() + "\n";                  
                    
                    error++;
                }
            }
            if (error > 0)
            {
                if (message_folder != "")
                {
                    MessageBox.Show(message_folder, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (message_template != "")
                {
                    MessageBox.Show(message_template, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                result = false;
            }         
            return result;
        }
    }
}

