using System;
using System.Drawing;
using System.Windows.Forms;

namespace StoreManager
{
    public class InteractionControl
    {
        /// <summary>
        /// Фильтр ввода в TextBox, ComboBox позволяющий ввод только десятичных чисел с плавающей точкой 
        /// </summary>
        /// <param name="e"></param>
        public static void Control_Filter_Decimal_Only(KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            //e.KeyChar <= 47 || e.KeyChar >= 58 - цифры
            //number != 44 - запятая
            //number != 46 - точка
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != (char)Keys.Back && number != 44 && number != 46 && e.KeyChar != (char)Keys.Left && e.KeyChar != (char)Keys.Right && e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true;
            }

            //заменить точку на запятую
            if (e.KeyChar == 46)
            {
                e.KeyChar = (char)44;
            }
        }

        /// <summary>
        /// Фильтр ввода в TextBox, ComboBox позволяющий ввод только целых чисел 
        /// </summary>
        /// <param name="e"></param>
        public static void Control_Filter_Integer_Only(KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            //e.KeyChar <= 47 || e.KeyChar >= 58 - цифры
            //number != 44 - запятая
            //number != 46 - точка
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != (char)Keys.Back && e.KeyChar != (char)Keys.Left && e.KeyChar != (char)Keys.Right && e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Фильтр ввода в TextBox, ComboBox позволяющий ввод только прописных букв латинского алфавита
        /// </summary>
        /// <param name="e"></param>
        public static void Control_Filter_Latin_Only(KeyPressEventArgs e)
        {
            //number != 8 - Backspace
            if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') ||         
                e.KeyChar == (char)Keys.Back)
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Поиск в таблицах dataGridView
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="find_text">искомый текст</param>
        public static void Search_dataGridView(DataGridView dataGridView, string find_text)
        {
            dataGridView.ClearSelection();
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                dataGridView.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView.ColumnCount; j++)
                    if (dataGridView.Rows[i].Cells[j].Value != null)
                        if (dataGridView.Rows[i].Cells[j].Value.ToString().ToLower().Contains(find_text))
                        {
                            dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
                            break;
                        }
                        else
                        {
                            dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                        }
            }
        }


        public static void DataGridView_Mouse_Right_Click(DataGridView dataGridView, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var h = dataGridView.HitTest(e.X, e.Y);
                if (h.Type == DataGridViewHitTestType.Cell)
                {
                    dataGridView.Rows[h.RowIndex].Selected = true;
                    dataGridView.CurrentCell = dataGridView.Rows[h.RowIndex].Cells[h.ColumnIndex];
                }

            }
        }

        public static string Control_Visual_Decimal(string line_in)
        {
            string line_out = "";
            decimal decimal_digital;
            try
            {
                decimal_digital = Convert.ToDecimal(line_in);
                line_out = decimal_digital.ToString("#0.00");
            }
            catch (Exception)
            {
            }
            return line_out;
        }
        /// <summary>
        /// Заменить недупустимые символы для запроса sql
        /// </summary>
        /// <param name="input_string"></param>
        /// <returns></returns>
        public static string Replace(string input_string)
        {
            input_string = input_string.Replace("\\"," ");
            input_string = input_string.Replace("%"," ");
            input_string = input_string.Replace("/", " ");
            input_string = input_string.Replace("\\\\", " ");
            input_string = input_string.Replace("'", "\"");
            input_string = input_string.Replace("%", " ");
            input_string = input_string.Replace("$", " ");
            input_string = input_string.Replace("<", " ");
            input_string = input_string.Replace(">", " ");
            return input_string;
        }

        /*public static void Click_Right_Mouse_Button_dataGriedView(DataGridView dataGridView, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var h = dataGridView.HitTest(e.X, e.Y);
                if (h.Type == DataGridViewHitTestType.Cell)
                {
                    dataGridView.Rows[h.RowIndex].Selected = true;
                }
            }
        }*/
    }
}
