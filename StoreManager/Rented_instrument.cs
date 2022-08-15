using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager
{
    public class Rented_instrument
    {
        public int Id_rented_instrument { get; set; }
        public string Rented_instrument_name { get; set; }
        public decimal Rented_instrument_price { get; set; }

        private static readonly Rented_instrument instance = new Rented_instrument();

        //потокобезопасный Singletone для Rented_instrument
        public static Rented_instrument getInstance()
        {
            return instance;
        }

        //
    }
}
