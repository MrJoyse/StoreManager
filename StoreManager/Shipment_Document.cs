using System;

namespace StoreManager
{
    /// <summary>
    /// Товарные накладные
    /// </summary>
    class Shipment_Document
    {
        /// <summary>
        /// Номер накладной
        /// </summary>
        public string Number_document { get; set; }
        /// <summary>
        /// Дата накладной
        /// </summary>
        public DateTime Date_document { get; set; }
        /// <summary>
        /// Сумма накладной
        /// </summary>
        public decimal Summ_document { get; set; }

    }
}
