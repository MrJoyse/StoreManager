using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager
{
    /// <summary>
    /// Сотрудник фирмы
    /// </summary>
    class Firm_member
    {
        /// <summary>
        /// id сотрудника
        /// </summary>
        public int Id_member { get; set; }
        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string First_Name { get; set; }
        /// <summary>
        /// Отчество сотрудника
        /// </summary>
        public string Last_Name { get; set; }
        /// <summary>
        /// Рабочий телефон сотрудника
        /// </summary>
        public string Work_Phone { get; set; }
        /// <summary>
        /// Мобильный телефон сотрудника
        /// </summary>
        public string Mobile_Phone { get; set; }
        /// <summary>
        /// Электронный адрес сотрудника
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// Заметка о сотруднике
        /// </summary>
        public string Note { get; set; }
    }
}
