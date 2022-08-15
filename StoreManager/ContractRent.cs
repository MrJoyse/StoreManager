using System;
using Word = Microsoft.Office.Interop.Word;


namespace StoreManager
{
    /// <summary>
    /// Договор аренды
    /// </summary>
    public class ContractRent : Contract
    {
        /// <summary>
        /// срок аренды
        /// </summary>
        public int Rental_period { get; set; }
        /// <summary>
        /// стоимость аренды за сутки
        /// </summary>
        public decimal Rental_price { get; set; }
        /// <summary>
        /// id инструмента
        /// </summary>
        public int Id_rented_instrument { get; set; }
        /// <summary>
        /// наименование инструмента
        /// </summary>
        public string Name_rented_instrument { get; set; }
        /// <summary>
        /// Конструктор договора аренды
        /// </summary>
        /// <param name="id_type_of_contract">Вид договора</param>
        /// <param name="date_of_signing">Дата подписания</param>
        /// <param name="rental_period">Срок аренды</param>
        /// <param name="prepayment">Аванс</param>
        /// <param name="rental_price">Стоимость аренды за сутки</param>
        /// <param name="id_rented_instrument">id инструмента</param>
        public ContractRent(
            int id_type_of_contract, 
            DateTime date_of_signing, 
            int rental_period, 
            decimal prepayment, 
            decimal rental_price,
            string name_rented_instrument,
            int id_rented_instrument
            )
        {
            //записываем вид договора
            Id_type_of_contract = id_type_of_contract;
            //записываем дату оформления договора
            Date_of_signing = date_of_signing;
            Rental_period = rental_period;
            Rental_price = rental_price;
            //записываем дату окончания договора
            Date_expiration = date_of_signing.AddDays(rental_period);
            //записываем аванс
            Prepayment = prepayment;
            //получаем сумму итого
            Summ_contract = rental_price * rental_period;
            //пулучить текущую задолженность
            Current_Debt = Summ_contract - Prepayment;
            Name_rented_instrument = name_rented_instrument;
            //записываем наименование инструмента
            Id_rented_instrument = id_rented_instrument;
        }

        public ContractRent() { }
        /// <summary>
        /// Конструктор печати Рассрочки
        /// </summary>
        /// <param name="contract">Рассрочка</param>
        /// <param name="document">Активный документ</param>
        /// <param name="user">Активный документ</param>
        /// <param name="shopper">Покупатель</param>
        /// <param name="settings">Настройки</param>
        public void PrintContract(ContractRent contract, Word.Document document, User user, Shopper shopper, Settings settings)
        {
            //передача данных в закладки шаблона
            document.Bookmarks[settings.Bookmarks_Number_Contract].Range.Text = contract.Id_contract.ToString();
            document.Bookmarks[settings.Bookmarks_Date_Of_Signing].Range.Text = contract.Date_of_signing.ToString("dd.MM.yyyy г. ") + DateTime.Now.ToShortTimeString();
            document.Bookmarks[settings.Bookmarks_Declension].Range.Text = user.Declension;
            document.Bookmarks[settings.Bookmarks_Documents].Range.Text = user.Documents;
            document.Bookmarks[settings.Bookmarks_Date_Documents].Range.Text = user.Date_documents.ToString("dd.MM.yyyy г. ");
            document.Bookmarks[settings.Bookmarks_Surname].Range.Text = shopper.Surname;
            document.Bookmarks[settings.Bookmarks_First_Name].Range.Text = shopper.First_name;
            document.Bookmarks[settings.Bookmarks_Last_Name].Range.Text = shopper.Last_name;
            document.Bookmarks[settings.Bookmarks_Name_Rented_Instrument].Range.Text = contract.Name_rented_instrument;           
            document.Bookmarks[settings.Bookmarks_Rental_Period].Range.Text = contract.Rental_period.ToString();
            document.Bookmarks[settings.Bookmarks_Summ_Contract].Range.Text = contract.Summ_contract.ToString("#0.00");
            document.Bookmarks[settings.Bookmarks_Rental_Price].Range.Text = contract.Rental_price.ToString("#0.00");
            document.Bookmarks[settings.Bookmarks_Surname_Signing].Range.Text = shopper.Surname;
            document.Bookmarks[settings.Bookmarks_First_Name_Signing].Range.Text = shopper.First_name;
            document.Bookmarks[settings.Bookmarks_Last_Name_Signing].Range.Text = shopper.Last_name;
            document.Bookmarks[settings.Bookmarks_Serial_Passport].Range.Text = shopper.Serial_passport;
            document.Bookmarks[settings.Bookmarks_Number_Passport].Range.Text = shopper.Number_passport;
            document.Bookmarks[settings.Bookmarks_Date_Of_Issue_Passport].Range.Text = shopper.Date_of_issue_passport.ToString("dd.MM.yyyy");
            document.Bookmarks[settings.Bookmarks_Department_Name_Passport].Range.Text = shopper.Department_name_passport;
            document.Bookmarks[settings.Bookmarks_Full_Adress_Shopper_Signing].Range.Text = shopper.Full_adress_registration;
            document.Bookmarks[settings.Bookmarks_Mobile_Phone].Range.Text = shopper.Mobile_phone;
            document.Bookmarks[settings.Bookmarks_Home_Phone].Range.Text = shopper.Home_phone;           
        }
    }
}
