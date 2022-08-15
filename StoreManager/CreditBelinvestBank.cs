using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace StoreManager
{
    /// <summary>
    /// Кредит Белинвестбанка
    /// </summary>
    class CreditBelinvestBank
    {
        /// <summary>
        /// Сумма договора
        /// </summary>
        private static decimal summ_credit;
        readonly SqlConnection sqlConnection = DBSQLServerUtils.GetDBConnection();
        /// <summary>
        /// id Заявки в Базе данных
        /// </summary>
        public int Id_credit_questionnaire { get; set; }
        /// <summary>
        /// Полная стоимость товара (работы, услуги)
        /// </summary>
        public decimal Summ_price_products_credit { get; set; }
        /// <summary>
        /// Сумма запрашиваемого кредита
        /// </summary>
        public decimal Summ_credit { get; set; }
        /// <summary>
        /// Первоначальный взнос
        /// </summary>
        public decimal Prepayment { get; set; }
        /// <summary>
        /// Дата оформления заявки на кредит
        /// </summary>
        public DateTime Date_credit_questionnaire { get; set; }
        /// <summary>
        /// Номер заявки на кредит(вводится пользователем)
        /// </summary>
        public int Number_credit_questionnaire { get; set; }
        /// <summary>
        /// Вид товара/услуги
        /// </summary>
        public string Type_products { get; set; }
        /// <summary>
        /// Платеж(и) по кредиту(ам)
        /// </summary>
        public decimal Credit_payments { get; set; }
        /// <summary>
        /// Валюта платежа(ей) по кредиту(ам)
        /// </summary>
        public int Id_credit_currency { get; set; }
        /// <summary>
        /// Наличие договоров овердрафтного кредитования
        /// </summary>
        public bool Overdraft { get; set; }
        /// <summary>
        /// Общую сумму лимитов овердрафтов
        /// </summary>
        public decimal Overdraft_payments { get; set; }
        /// <summary>
        /// Наименование нанимателя (юридическое или физическое лицо, которому законодательством предоставлено право заключения и прекращения трудового договора с работником)
        /// </summary>
        public string Place_of_work_name { get; set; }
        /// <summary>
        /// Страна регистрации нанимателя
        /// </summary>
        public string Place_of_work_country { get; set; }
        /// <summary>
        /// Область регистрации нанимателя
        /// </summary>
        public string Place_of_work_region { get; set; }
        /// <summary>
        /// Район регистрации нанимателя
        /// </summary>
        public string Place_of_work_area { get; set; }
        /// <summary>
        /// Населенный пункт регистрации нанимателя
        /// </summary>
        public string Place_of_work_city { get; set; }
        /// <summary>
        /// Тип улицы регистрации нанимателя
        /// </summary>
        public string Place_of_work_street_variant { get; set; }
        /// <summary>
        /// Улица регистрации нанимателя
        /// </summary>
        public string Place_of_work_street { get; set; }
        /// <summary>
        /// Номер дома регистрации нанимателя
        /// </summary>
        public string Place_of_work_house { get; set; }
        /// <summary>
        /// Корпус дома регистрации нанимателя
        /// </summary>
        public string Place_of_work_house_building { get; set; }
        /// <summary>
        /// Номер офис регистрации нанимателя
        /// </summary>
        public string Place_of_work_office { get; set; }
        /// <summary>
        /// Почтовый код регистрации нанимателя
        /// </summary>
        public string Place_of_work_postcode { get; set; }
        /// <summary>
        /// Рабочий телефон
        /// </summary>
        public string Place_of_work_phone { get; set; }
        /// <summary>
        /// Стаж работы у данного нанимателя, лет
        /// </summary>
        public int Experience_years_last_work_place { get; set; }
        /// <summary>
        /// Стаж работы у данного нанимателя, месяцев
        /// </summary>
        public int Experience_months_last_work_place { get; set; }
        /// <summary>
        /// Стаж работы за все время
        /// </summary>
        public int Experience_years_all_time { get; set; }
        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// Заработная плата
        /// </summary>
        public decimal Salary { get; set; }
        /// <summary>
        /// id Образования
        /// </summary>
        public int Id_education_status { get; set; }
        /// <summary>
        /// id статус отношения к военной службе
        /// </summary>
        public int Id_military_status { get; set; }
        /// <summary>
        /// отсрочка к призыву
        /// </summary>
        public DateTime Millitary_postponement_date { get; set; }
        /// <summary>
        /// примечания по призывному 
        /// </summary>
        public string Millitary_note { get; set; }
        /// <summary>
        /// id отношения к Белинвестбанку
        /// </summary>
        public int Id_belinvest_status { get; set; }
        /// <summary>
        /// Иное отношение с Белинвестбанком
        /// </summary>
        public string Belinvest_note { get; set; }
        /// <summary>
        /// Уголовная ответственность
        /// </summary>
        public bool Criminal_liability { get; set; }
        /// <summary>
        /// Контактное лицо. Телефон 
        /// </summary>
        public string Contact_person_phone { get; set; }
        /// <summary>
        /// Контактное лицо. Фамилия 
        /// </summary>
        public string Contact_person_surname { get; set; }
        /// <summary>
        /// Контактное лицо. Имя 
        /// </summary>
        public string Contact_person_first_name { get; set; }
        /// <summary>
        /// Контактное лицо. Отчество 
        /// </summary>
        public string Contact_person_last_name { get; set; }
        /// <summary>
        /// Контактное лицо. Кем приходится
        /// </summary>
        public string Contact_person_status { get; set; }
        /// <summary>
        /// Статус брака
        /// </summary>
        public int Id_married_status { get; set; }
        /// <summary>
        /// Количество детей до 18 лет
        /// </summary>
        public int Amount_of_children { get; set; }
        /// <summary>
        /// Количество иждивенцев(кроме детей)
        /// </summary>
        public int Amount_of_dependent { get; set; }
        /// <summary>
        /// Примечания
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// Ответ банка
        /// </summary>
        public string Bank_response { get; set; }
        /// <summary>
        /// Дата передачи документов в банк
        /// </summary>
        public DateTime Date_transfer_of_documents { get; set; }      
        /// <summary>
        /// Путь сохранения файла анкеты. Для БД
        /// </summary>
        public string Path_save_file_questionanaire_document { get; set; }
        /// <summary>
        /// Путь сохранения файла согласия кредитного бюро. Для БД
        /// </summary>
        public string Path_save_file_consent_story_document { get; set; }
        /// <summary>
        /// Путь сохранения файла передачи информации третьим лицам. Для БД
        /// </summary>
        public string Path_save_file_consent_transfer_document { get; set; }
        /// <summary>
        /// Путь сохранения файла согласия проверки по базе ФСЗН. Для БД
        /// </summary>
        public string Path_save_file_consent_pension_document { get; set; }              

        /// <summary>
        /// Получить итоговую сумму кредита 
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="query">запрос</param>
        /// <returns>summ_contract</returns>
        public static decimal Get_Summ_Credit(SqlConnection sqlConnection, string query)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = query
            };
            try
            {
                summ_credit = Convert.ToDecimal(command.ExecuteScalar());
            }
            catch (Exception)
            {
                summ_credit = 0;
            }
            sqlConnection.Close();
            return summ_credit;
        }

        /// <summary>
        /// Вставить данные заявки на кредит в базу, получить id добавленной заявки на кредит
        /// </summary>
        /// <param name="sqlConnection">Соединение с базой</param>
        /// <param name="creditBelinvestBank">данные для заявки на кредит</param>
        /// <returns>id_credit_questionnaire номер заявки в базе данных</returns>
        public static int Insert_to_Credit_Questionnaire(
            SqlConnection sqlConnection,
            CreditBelinvestBank creditBelinvestBank,
            Shopper shopper,
            User user
            )
        {
            int id_credit_questionnaire;
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Insert_to_Credit_Questionnaire"
            };
            command.Parameters.AddWithValue("@number_credit_questionnaire", creditBelinvestBank.Number_credit_questionnaire);
            command.Parameters.AddWithValue("@summ_credit", creditBelinvestBank.Summ_credit);
            command.Parameters.AddWithValue("@prepayment", creditBelinvestBank.Prepayment);
            command.Parameters.AddWithValue("@date_credit_questionnaire", creditBelinvestBank.Date_credit_questionnaire);
            command.Parameters.AddWithValue("@id_user", user.Id_user);
            command.Parameters.AddWithValue("@id_shopper", shopper.Id);
            command.Parameters.AddWithValue("@type_products", creditBelinvestBank.Type_products);
            command.Parameters.AddWithValue("@credit_payments", creditBelinvestBank.Credit_payments);
            command.Parameters.AddWithValue("@id_credit_currency", creditBelinvestBank.Id_credit_currency);
            command.Parameters.AddWithValue("@overdraft", creditBelinvestBank.Overdraft);
            command.Parameters.AddWithValue("@overdraft_payments", creditBelinvestBank.Overdraft_payments);
            command.Parameters.AddWithValue("@place_of_work_name", creditBelinvestBank.Place_of_work_name);
            command.Parameters.AddWithValue("@place_of_work_country", creditBelinvestBank.Place_of_work_country);
            command.Parameters.AddWithValue("@place_of_work_region", creditBelinvestBank.Place_of_work_region);
            command.Parameters.AddWithValue("@place_of_work_area", creditBelinvestBank.Place_of_work_area);
            command.Parameters.AddWithValue("@place_of_work_city", creditBelinvestBank.Place_of_work_city);
            command.Parameters.AddWithValue("@place_of_work_street_variant", creditBelinvestBank.Place_of_work_street_variant);
            command.Parameters.AddWithValue("@place_of_work_street", creditBelinvestBank.Place_of_work_street);
            command.Parameters.AddWithValue("@place_of_work_house", creditBelinvestBank.Place_of_work_house);
            command.Parameters.AddWithValue("@place_of_work_house_building", creditBelinvestBank.Place_of_work_house_building);
            command.Parameters.AddWithValue("@place_of_work_office", creditBelinvestBank.Place_of_work_office);
            command.Parameters.AddWithValue("@place_of_work_postcode", creditBelinvestBank.Place_of_work_postcode);
            command.Parameters.AddWithValue("@place_of_work_phone", creditBelinvestBank.Place_of_work_phone);
            command.Parameters.AddWithValue("@experience_years_last_work_place", creditBelinvestBank.Experience_years_last_work_place);
            command.Parameters.AddWithValue("@experience_months_last_work_place", creditBelinvestBank.Experience_months_last_work_place);
            command.Parameters.AddWithValue("@experience_all_time", creditBelinvestBank.Experience_years_all_time);
            command.Parameters.AddWithValue("@position", creditBelinvestBank.Position);
            command.Parameters.AddWithValue("@salary", creditBelinvestBank.Salary);
            command.Parameters.AddWithValue("@id_education_status", creditBelinvestBank.Id_education_status);
            command.Parameters.AddWithValue("@id_military_status", creditBelinvestBank.Id_military_status);
            //если отношение к военной службе "острочка" индекс в таблице =6,  указать дату отсрочки
            if (creditBelinvestBank.Id_military_status == 6)
            {
                command.Parameters.AddWithValue("@millitary_postponement_date", creditBelinvestBank.Millitary_postponement_date);
            }          
            command.Parameters.AddWithValue("@millitary_note", creditBelinvestBank.Millitary_note);
            command.Parameters.AddWithValue("@id_belinvest_status", creditBelinvestBank.Id_belinvest_status);
            command.Parameters.AddWithValue("@belinvest_note", creditBelinvestBank.Belinvest_note);
            command.Parameters.AddWithValue("@criminal_liability", creditBelinvestBank.Criminal_liability);
            command.Parameters.AddWithValue("@contact_person_phone", creditBelinvestBank.Contact_person_phone);
            command.Parameters.AddWithValue("@contact_person_surname", creditBelinvestBank.Contact_person_surname);
            command.Parameters.AddWithValue("@contact_person_first_name", creditBelinvestBank.Contact_person_first_name);
            command.Parameters.AddWithValue("@contact_person_last_name", creditBelinvestBank.Contact_person_last_name);
            command.Parameters.AddWithValue("@contact_person_status", creditBelinvestBank.Contact_person_status);
            command.Parameters.AddWithValue("@id_married_status", creditBelinvestBank.Id_married_status);
            command.Parameters.AddWithValue("@amount_of_children", creditBelinvestBank.Amount_of_children);
            command.Parameters.AddWithValue("@amount_of_dependent", creditBelinvestBank.Amount_of_dependent);
            command.Parameters.AddWithValue("@note", creditBelinvestBank.Note);
            command.Parameters.AddWithValue("@path_save_questionanaire_document", creditBelinvestBank.Path_save_file_questionanaire_document);
            command.Parameters.AddWithValue("@path_save_consent_story_document", creditBelinvestBank.Path_save_file_consent_story_document);
            command.Parameters.AddWithValue("@path_save_consent_transfer_document", creditBelinvestBank.Path_save_file_consent_transfer_document);
            command.Parameters.AddWithValue("@path_save_consent_pension_document", creditBelinvestBank.Path_save_file_consent_pension_document);
            id_credit_questionnaire = Convert.ToInt32(command.ExecuteScalar().ToString());
            sqlConnection.Close();
            return id_credit_questionnaire;
        }

        /// <summary>
        /// Печать согласия на проверку в базе ФСЗН
        /// </summary>
        /// <param name="shopper">покупатель</param>
        /// <param name="creditBelinvestBank">данные заявки Белинвестбанка</param>
        /// <param name="document">word документ</param>
        public static void PrintConsentPension(Shopper shopper, CreditBelinvestBank creditBelinvestBank, Word.Document document)
        {         
            document.Bookmarks["number_consent"].Range.Text = creditBelinvestBank.Number_credit_questionnaire.ToString();
            document.Bookmarks["surname"].Range.Text = shopper.Surname;
            document.Bookmarks["first_name"].Range.Text = shopper.First_name;
            document.Bookmarks["last_name"].Range.Text = shopper.Last_name;
            document.Bookmarks["personal_number_passport"].Range.Text = shopper.Personal_number_passport;
            document.Bookmarks["date_consent"].Range.Text = creditBelinvestBank.Date_credit_questionnaire.ToString("«dd» MMMM yyyy г.");
            document.Bookmarks["shopper_name_signing_abbreviation"].Range.Text = shopper.Abbreviated_name;
        }

        /// <summary>
        /// Печать согласия на передачу информации третьим лицам
        /// </summary>
        /// <param name="shopper">покупатель</param>
        /// <param name="creditBelinvestBank">данные заявки Белинвестбанка</param>
        /// <param name="document">word документ</param>
        public static void PrintConsentTransfer(Shopper shopper, CreditBelinvestBank creditBelinvestBank, Word.Document document)
        {
            document.Bookmarks["surname"].Range.Text = shopper.Surname;
            document.Bookmarks["first_name"].Range.Text = shopper.First_name;
            document.Bookmarks["last_name"].Range.Text = shopper.Last_name;
            document.Bookmarks["date_consent"].Range.Text = creditBelinvestBank.Date_credit_questionnaire.ToString("«dd» MMMM yyyy г.");
            document.Bookmarks["shopper_name_signing_abbreviation"].Range.Text = shopper.Abbreviated_name;
        }

        /// <summary>
        /// Печать анкеты-заявления на кредит
        /// </summary>
        /// <param name="creditBelinvestBank">данные заявки Белинвестбанка</param>
        /// <param name="document">word документ</param>
        public static void PrintQuestionnaire(CreditBelinvestBank creditBelinvestBank, Shopper shopper, Word.Document document)
        {
            document.Bookmarks["number_credit_questionnaire"].Range.Text = creditBelinvestBank.Number_credit_questionnaire.ToString();
            document.Bookmarks["date_credit_questionnaire"].Range.Text = creditBelinvestBank.Date_credit_questionnaire.ToString("«dd» MMMM yyyy г");
            document.Bookmarks["type_products"].Range.Text = creditBelinvestBank.Type_products;
            document.Bookmarks["summ_price_products_credit"].Range.Text = creditBelinvestBank.Summ_price_products_credit.ToString("#0.00");
            document.Bookmarks["prepayment_credit"].Range.Text = creditBelinvestBank.Prepayment.ToString("#0.00");
            document.Bookmarks["summ_credit"].Range.Text = creditBelinvestBank.Summ_credit.ToString("#0.00");
            document.Bookmarks["surname"].Range.Text = shopper.Surname;           
            document.Bookmarks["first_name"].Range.Text = shopper.First_name;
            document.Bookmarks["last_name"].Range.Text = shopper.Last_name;
            document.Bookmarks["surname_old"].Range.Text = shopper.Surname_old;
            document.Bookmarks["mobile_phone"].Range.Text = shopper.Mobile_phone;
            document.Bookmarks["mail"].Range.Text = shopper.Mail;

            document.Bookmarks["postcode"].Range.Text = shopper.Postcode;
            document.Bookmarks["region"].Range.Text = shopper.Region_name;
            document.Bookmarks["area"].Range.Text = shopper.Area_name;
            document.Bookmarks["city"].Range.Text = shopper.City_name;
            document.Bookmarks["street_variant"].Range.Text = shopper.Street_variant;
            document.Bookmarks["street"].Range.Text = shopper.Street;
            document.Bookmarks["house"].Range.Text = shopper.House;
            document.Bookmarks["house_building"].Range.Text = shopper.House_Building;
            document.Bookmarks["apartment"].Range.Text = shopper.Apartment;
            document.Bookmarks["home_phone"].Range.Text = shopper.Home_phone;

            document.Bookmarks["postcode_residence"].Range.Text = shopper.Postcode_residence;
            document.Bookmarks["region_residence"].Range.Text = shopper.Region_name_residence;
            document.Bookmarks["area_residence"].Range.Text = shopper.Area_name_residence;
            document.Bookmarks["city_residence"].Range.Text = shopper.City_name_residence;
            document.Bookmarks["street_variant_residence"].Range.Text = shopper.Street_variant_residence;
            document.Bookmarks["street_residence"].Range.Text = shopper.Street_residence;
            document.Bookmarks["house_residence"].Range.Text = shopper.House_residence;
            document.Bookmarks["house_building_residence"].Range.Text = shopper.House_Building_residence;
            document.Bookmarks["apartment_residence"].Range.Text = shopper.Apartment_residence;
            document.Bookmarks["home_phone_residence"].Range.Text = shopper.Home_phone_residence;

            document.Bookmarks["date_ctredit_history"].Range.Text = creditBelinvestBank.Date_credit_questionnaire.ToString("«dd» MMMM yyyy г");
            document.Bookmarks["credit_payments"].Range.Text = creditBelinvestBank.Credit_payments.ToString("#0.00");
            //Валюта платежей по кредитам
            switch (creditBelinvestBank.Id_credit_currency)
            {
                case 1:
                    //Галочка в квадрате код символа 82. шрифт Wingdings 2
                    document.Bookmarks["currency_BYN"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 2:
                    document.Bookmarks["currency_USD"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 3:
                    document.Bookmarks["currency_EUR"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 4:
                    document.Bookmarks["currency_RUB"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                default:
                    break;
            }
            //Информация по овердрафтам
            if (creditBelinvestBank.Overdraft == true)
            {
                document.Bookmarks["overdraft_true"].Range.InsertSymbol(82, "Wingdings 2");
                document.Bookmarks["overdraft_payments"].Range.Text = creditBelinvestBank.Overdraft_payments.ToString("#0.00");
            }
            else
            {
                document.Bookmarks["overdraft_not"].Range.InsertSymbol(82, "Wingdings 2");
            }
            document.Bookmarks["place_of_work_name"].Range.Text = creditBelinvestBank.Place_of_work_name;
            document.Bookmarks["place_of_work_postcode"].Range.Text = creditBelinvestBank.Place_of_work_postcode;
            document.Bookmarks["place_of_work_region"].Range.Text = creditBelinvestBank.Place_of_work_region;
            document.Bookmarks["place_of_work_city"].Range.Text = creditBelinvestBank.Place_of_work_city;
            document.Bookmarks["place_of_work_street_variant"].Range.Text = creditBelinvestBank.Place_of_work_street_variant;
            document.Bookmarks["place_of_work_street"].Range.Text = creditBelinvestBank.Place_of_work_street;
            document.Bookmarks["place_of_work_house"].Range.Text = creditBelinvestBank.Place_of_work_house;
            document.Bookmarks["place_of_work_house_building"].Range.Text = creditBelinvestBank.Place_of_work_house_building;
            document.Bookmarks["place_of_work_office"].Range.Text = creditBelinvestBank.Place_of_work_office;
            document.Bookmarks["place_of_work_phone"].Range.Text = creditBelinvestBank.Place_of_work_phone;
            document.Bookmarks["experience_years_last_work_place"].Range.Text = creditBelinvestBank.Experience_years_last_work_place.ToString();
            document.Bookmarks["experience_months_last_work_place"].Range.Text = creditBelinvestBank.Experience_months_last_work_place.ToString();
            document.Bookmarks["experience_years_all_time"].Range.Text = creditBelinvestBank.Experience_years_all_time.ToString();
            document.Bookmarks["position"].Range.Text = creditBelinvestBank.Position;
            document.Bookmarks["salary"].Range.Text = creditBelinvestBank.Salary.ToString("#0.00");

            //Образование
            switch (creditBelinvestBank.Id_education_status)
            {
                case 1:
                    //Галочка в квадрате код символа 82. шрифт Wingdings 2
                    document.Bookmarks["education_not_average"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 2:
                    document.Bookmarks["education_average"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 3:
                    document.Bookmarks["education_average_special"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 4:
                    document.Bookmarks["education_not_higher"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 5:
                    document.Bookmarks["education_higher"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                default:
                    break;
            }

            //Отношение к воинской службе
            switch (creditBelinvestBank.Id_military_status)
            {
                case 1:
                    //Галочка в квадрате код символа 82. шрифт Wingdings 2
                    document.Bookmarks["millitary_stock"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 2:
                    document.Bookmarks["millitary_not_serve"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 3:
                    document.Bookmarks["millitary_warrior"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 4:
                    document.Bookmarks["millitary_not_warrior"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 5:
                    document.Bookmarks["millitary_conscript"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 6:
                    document.Bookmarks["millitary_postponement"].Range.InsertSymbol(82, "Wingdings 2");
                    document.Bookmarks["millitary_postponement_date"].Range.Text = creditBelinvestBank.Millitary_postponement_date.ToString("dd-MM-yyyy");
                    break;
                case 7:
                    document.Bookmarks["millitary_other"].Range.Text = creditBelinvestBank.Millitary_note;
                    break;
                default:
                    break;
            }

            //Отношение с Белинвестбанком
            switch (creditBelinvestBank.Id_belinvest_status)
            {
                case 1:
                    document.Bookmarks["belinvest_worker"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 2:
                    document.Bookmarks["belinvest_shareholder"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 3:
                    document.Bookmarks["belinvest_not"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 4:
                    document.Bookmarks["belinvest_other"].Range.Text = creditBelinvestBank.Belinvest_note;
                    break;
                default:
                    break;
            }           

            //Привлекались ли к уголовной ответственности
            if (creditBelinvestBank.Criminal_liability == true)
            {
                document.Bookmarks["criminal_liability_true"].Range.InsertSymbol(82, "Wingdings 2");
            }
            else
            {
                document.Bookmarks["criminal_liability_not"].Range.InsertSymbol(82, "Wingdings 2");
            }

            document.Bookmarks["contact_person_mobile_phone"].Range.Text = creditBelinvestBank.Contact_person_phone;
            string contact_person_full_name = creditBelinvestBank.Contact_person_surname + " " + creditBelinvestBank.Contact_person_first_name + " " + creditBelinvestBank.Contact_person_last_name;
            document.Bookmarks["contact_person_full_name"].Range.Text = contact_person_full_name;
            document.Bookmarks["contact_person_status"].Range.Text = creditBelinvestBank.Contact_person_status;

            //Семейное положение
            switch (creditBelinvestBank.Id_married_status)
            {
                case 1:
                    document.Bookmarks["married"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 2:
                    document.Bookmarks["married_not"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                case 3:
                    document.Bookmarks["married_old_status"].Range.InsertSymbol(82, "Wingdings 2");
                    break;
                default:
                    break;
            }

            document.Bookmarks["amount_of_children"].Range.Text = creditBelinvestBank.Amount_of_children.ToString();
            document.Bookmarks["amount_of_dependent"].Range.Text = creditBelinvestBank.Amount_of_dependent.ToString();
            string shopper_full_name = shopper.Surname + " " + shopper.First_name + " " + shopper.Last_name;
            document.Bookmarks["shopper_full_name"].Range.Text = shopper_full_name;
            document.Bookmarks["date_signing"].Range.Text = creditBelinvestBank.Date_credit_questionnaire.ToString("«dd» MMMM yyyy г");                     
            document.Bookmarks["shopper_name_signing_abbreviation"].Range.Text = shopper.Abbreviated_name;
            //Галочка в квадрате код символа 82. шрифт Wingdings 2
            //document.Bookmarks["number_credit_questionnaire"].Range.InsertSymbol(82, "Wingdings 2");
            //document.Bookmarks["number_credit_questionnaire"].Range.InlineShapes.AddPicture(@"d:\StoreManager\StoreManager\bin\Debug\Icon\anchor.png");
        }

        /// <summary>
        /// Печать согласия проверки в кредитном бюро
        /// </summary>
        /// <param name="shopper">покупатель</param>
        /// <param name="document">документ</param>
        /// <param name="user">пользователь</param>
        /// <param name="creditBelinvestBank">данные заявки Белинвестбанка</param>
        public static void PrintConsentStory(Shopper shopper, Word.Document document, User user, CreditBelinvestBank creditBelinvestBank)
        {
            document.Bookmarks["surname"].Range.Text = shopper.Surname;
            document.Bookmarks["first_name"].Range.Text = shopper.First_name;
            document.Bookmarks["last_name"].Range.Text = shopper.Last_name;
            document.Bookmarks["surname_old"].Range.Text = shopper.Surname_old;
            if (shopper.Sex_of_a_Person == true)
            {
                document.Bookmarks["sex_of_a_person"].Range.Text = "мужской";
            }
            else
            {
                document.Bookmarks["sex_of_a_person"].Range.Text = "женский";
            }
            document.Bookmarks["personal_number_passport"].Range.Text = shopper.Personal_number_passport;
            document.Bookmarks["birthday_date"].Range.Text = shopper.Birthday_Date.ToString("dd.MM.yyyy");
            document.Bookmarks["date_consent"].Range.Text = creditBelinvestBank.Date_credit_questionnaire.ToString("dd MMMM yyyy");
            document.Bookmarks["shopper_name_signing_abbreviation"].Range.Text = shopper.Abbreviated_name;
            document.Bookmarks["position"].Range.Text = user.Position;
            document.Bookmarks["user_short_name"].Range.Text = user.Short_name;
        }

        /// <summary>
        /// Сохранить документ Word
        /// </summary>
        /// <param name="creditBelinvestBank">заявка на кредит</param>
        /// <param name="settings">настройки</param>
        /// <param name="shopper">покупатель</param>
        /// <param name="document">word документ</param>
        /// <param name="type_document">тип документа "Анкета", "Согласие Кредитного бюро", "Согласие передачи данных", "Согласие ФСЗН" </param>
        /// <returns>path_file - путь к файлу</returns>
        public static string Save_File(CreditBelinvestBank creditBelinvestBank, Settings settings, Shopper shopper, Word.Document document, string type_document)
        {
            string path_file = "";
            path_file += settings.Path_save_credit_belinvest;
            path_file += shopper.Surname + " ";
            path_file += shopper.First_name + " ";
            path_file += shopper.Last_name + " ";           
            path_file += creditBelinvestBank.Date_credit_questionnaire.ToString("dd-MM-yyyy");

            DirectoryInfo dirInfo = new DirectoryInfo(path_file);
            if (dirInfo.Exists == false)
            {
                dirInfo.Create();
            }
            path_file += @"\" + type_document + " " + shopper.Abbreviated_name + " " + DateTime.Now.ToString("hh.mm.ss") + " " + creditBelinvestBank.Number_credit_questionnaire + ".dot";
            document.SaveAs(path_file);
            return path_file;
        }
       
        /// <summary>
        /// Вернуть путь к doc файлу по id заявки на кредит
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="id_credit_questionnaire">id заявки на кредит</param>
        /// <returns>path_file</returns>
        public static string Find_Path_File(SqlConnection sqlConnection, int id_credit_questionnaire, string field)
        {
            string path_file = "";
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            var command = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT " + field + " FROM Credit_Questionnaire WHERE id_credit_questionnaire = @id_credit_questionnaire"
            };
            command.Parameters.AddWithValue("@id_credit_questionnaire", id_credit_questionnaire);
            try
            {
                path_file = command.ExecuteScalar().ToString();
            }
            catch (Exception)
            {
            }
            sqlConnection.Close();
            return path_file;
        }

        /// <summary>
        /// Вернуть информацию заявки на кредит по id_credit_questionnaire
        /// </summary>
        /// <param name="sqlConnection">соединение</param>
        /// <param name="creditBelinvestBank">заявка на кредит</param>
        /// <returns>creditBelinvestBank</returns>
        public static CreditBelinvestBank Get_CreditBelinvestBank_Info_from_Id_credit_questionnaire(SqlConnection sqlConnection, CreditBelinvestBank creditBelinvestBank)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlCommand command = new SqlCommand
            {
                Connection = sqlConnection,
                //выбрать данные договора по его id
                CommandText = "Select * From Credit_Questionnaire WHERE id_credit_questionnaire=@id_credit_questionnaire"
            };
            command.Parameters.AddWithValue("@id_credit_questionnaire", creditBelinvestBank.Id_credit_questionnaire);
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    try
                    {
                        creditBelinvestBank.Number_credit_questionnaire = (int)dataReader["number_credit_questionnaire"];
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        creditBelinvestBank.Summ_credit = (decimal)dataReader["summ_credit"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Summ_credit = 0;
                    }
                    try
                    {
                        creditBelinvestBank.Prepayment = (decimal)dataReader["prepayment"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Prepayment = 0;
                    }
                    try
                    {
                        creditBelinvestBank.Date_credit_questionnaire = (DateTime)dataReader["date_credit_questionnaire"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Date_credit_questionnaire = DateTime.Today;
                    }
                    try
                    {
                        creditBelinvestBank.Type_products = (string)dataReader["type_products"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Type_products = "";
                    }

                    //В случае ошибки получения валюты кредиты присваиваем 1-BYN
                    try
                    {
                        creditBelinvestBank.Id_credit_currency = (int)dataReader["id_credit_currency"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Id_credit_currency = 1;
                    }

                    try
                    {
                        creditBelinvestBank.Overdraft = (bool)dataReader["overdraft"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Overdraft = false;
                    }

                    //при наличии овердрафта получить сумму
                    if (creditBelinvestBank.Overdraft == true)
                    {
                        try
                        {
                            creditBelinvestBank.Overdraft_payments = (decimal)dataReader["overdraft_payments"];
                        }
                        catch (Exception)
                        {
                            creditBelinvestBank.Overdraft_payments = 0;
                        }
                    }

                    try
                    {
                        creditBelinvestBank.Place_of_work_name = (string)dataReader["place_of_work_name"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Place_of_work_name = "";
                    }

                    try
                    {
                        creditBelinvestBank.Place_of_work_region = (string)dataReader["place_of_work_region"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Place_of_work_region = "";
                    }

                    try
                    {
                        creditBelinvestBank.Place_of_work_area = (string)dataReader["place_of_work_area"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Place_of_work_area = "";
                    }

                    try
                    {
                        creditBelinvestBank.Place_of_work_city = (string)dataReader["place_of_work_city"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Place_of_work_city = "";
                    }

                    try
                    {
                        creditBelinvestBank.Place_of_work_street_variant = (string)dataReader["place_of_work_street_variant"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Place_of_work_street_variant = "";
                    }

                    try
                    {
                        creditBelinvestBank.Place_of_work_street = (string)dataReader["place_of_work_street"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Place_of_work_street = "";
                    }

                    try
                    {
                        creditBelinvestBank.Place_of_work_house = (string)dataReader["place_of_work_house"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Place_of_work_house = "";
                    }

                    try
                    {
                        creditBelinvestBank.Place_of_work_house_building = (string)dataReader["place_of_work_house_building"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Place_of_work_house_building = "";
                    }

                    try
                    {
                        creditBelinvestBank.Place_of_work_office = (string)dataReader["place_of_work_office"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Place_of_work_office = "";
                    }

                    try
                    {
                        creditBelinvestBank.Place_of_work_postcode = (string)dataReader["place_of_work_postcode"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Place_of_work_postcode = "";
                    }

                    try
                    {
                        creditBelinvestBank.Place_of_work_phone = (string)dataReader["place_of_work_phone"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Place_of_work_phone = null;
                    }

                    try
                    {
                        creditBelinvestBank.Experience_years_last_work_place = (int)dataReader["experience_years_last_work_place"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Experience_years_last_work_place = 0;
                    }

                    try
                    {
                        creditBelinvestBank.Experience_months_last_work_place = (int)dataReader["experience_months_last_work_place"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Experience_months_last_work_place = 0;
                    }

                    try
                    {
                        creditBelinvestBank.Experience_years_all_time = (int)dataReader["experience_all_time"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Experience_years_all_time = 0;
                    }

                    try
                    {
                        creditBelinvestBank.Position = (string)dataReader["position"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Position = "";
                    }

                    try
                    {
                        creditBelinvestBank.Salary = (decimal)dataReader["salary"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Salary = 0;
                    }

                    try
                    {
                        creditBelinvestBank.Credit_payments = (decimal)dataReader["credit_payments"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Credit_payments = 0;
                    }

                    //В случае ошибки получения образования присваиваем "среднее" индекс в БД = 2
                    try
                    {
                        creditBelinvestBank.Id_education_status = (int)dataReader["id_education_status"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Id_education_status = 2;
                    }

                    try
                    {
                        creditBelinvestBank.Id_military_status = (int)dataReader["id_military_status"];
                    }
                    catch (Exception)
                    {                        
                    }

                    //если статус военной службы "отсрочка"(индекс в таблице = 6), получить дату отсрочки
                    if (creditBelinvestBank.Id_military_status == 6)
                    {
                        try
                        {
                            creditBelinvestBank.Millitary_postponement_date = (DateTime)dataReader["millitary_postponement_date"];
                        }
                        catch (Exception)
                        {
                            creditBelinvestBank.Millitary_postponement_date = DateTime.Today;
                        }
                    }

                    //если статус военной службы "иное"(индекс в таблице = 7), получить примечание
                    if (creditBelinvestBank.Id_military_status == 7)
                    {
                        try
                        {
                            creditBelinvestBank.Millitary_note = (string)dataReader["millitary_note"];
                        }
                        catch (Exception)
                        {
                            creditBelinvestBank.Millitary_note = "";
                        }
                    }

                    try
                    {
                        creditBelinvestBank.Id_belinvest_status = (int)dataReader["id_belinvest_status"];
                    }
                    catch (Exception)
                    {
                    }

                    //если статус отношения к Белинвестанку "иное" (индекс в таблице = 4), получить примечание
                    if (creditBelinvestBank.Id_belinvest_status == 4)
                    {
                        try
                        {
                            creditBelinvestBank.Belinvest_note = (string)dataReader["belinvest_note"];
                        }
                        catch (Exception)
                        {
                            creditBelinvestBank.Belinvest_note = "";
                        }                       
                    }

                    try
                    {
                        creditBelinvestBank.Criminal_liability = (bool)dataReader["criminal_liability"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Criminal_liability = false;
                    }

                    try
                    {
                        creditBelinvestBank.Contact_person_phone = (string)dataReader["contact_person_phone"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Contact_person_phone = null;
                    }

                    try
                    {
                        creditBelinvestBank.Contact_person_surname = (string)dataReader["contact_person_surname"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Contact_person_surname = "";
                    }

                    try
                    {
                        creditBelinvestBank.Contact_person_first_name = (string)dataReader["contact_person_first_name"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Contact_person_first_name = "";
                    }

                    try
                    {
                        creditBelinvestBank.Contact_person_last_name = (string)dataReader["contact_person_last_name"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Contact_person_last_name = "";
                    }

                    try
                    {
                        creditBelinvestBank.Contact_person_status = (string)dataReader["contact_person_status"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Contact_person_status = "";
                    }

                    //в случае ошибки получения "семейного положения" присвоить "Холост/не замужем"(индекс в таблице = 2)
                    try
                    {
                        creditBelinvestBank.Id_married_status = (int)dataReader["id_married_status"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Id_married_status = 2;
                    }

                    try
                    {
                        creditBelinvestBank.Amount_of_children = (int)dataReader["amount_of_children"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Amount_of_children = 0;
                    }

                    try
                    {
                        creditBelinvestBank.Amount_of_dependent = (int)dataReader["amount_of_dependent"];
                    }
                    catch (Exception)
                    {
                        creditBelinvestBank.Amount_of_dependent = 0;
                    }
                }
                dataReader.Close();
            }
            sqlConnection.Close();
            return creditBelinvestBank;
        }
    }
}
