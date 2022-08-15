using System;
using Word = Microsoft.Office.Interop.Word;
using System.Data;
using System.IO;
using NLog;
using System.Data.SqlClient;

namespace StoreManager
{
    /// <summary>
    /// Печать договора
    /// </summary>
    public class PrintWordContract
    {
        private Shopper shopper = Shopper.getInstance();
        private static Object missingObj;
        private static Object trueObj;
        private static Object falseObj;
        private static Word.Application application;
        private static Word.Document document;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Запуск Word и открытие документа
        /// </summary>
        /// <param name="path_template">Путь к шаблону</param>
        /// <returns>документ Word</returns>
        public static Word.Document StartWord(string path_template)
        {
            missingObj = System.Reflection.Missing.Value;
            trueObj = true;
            falseObj = false;
            document = null;
            //создаем обьект приложения word
            application = new Word.Application();
            // создаем путь к файлу
            Object templatePathObj = path_template;
            // открываем документ Word если вылетим не этом этапе, приложение останется открытым
            try
            {
                document = application.Documents.Add(ref templatePathObj, ref missingObj, ref missingObj, ref missingObj);
            }
            catch (Exception error)
            {                 
                System.Windows.Forms.MessageBox.Show("Ошибка открытия шаблона!","Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                application.Quit(ref missingObj, ref missingObj, ref missingObj);
                try
                {
                    document.Close(ref falseObj, ref missingObj, ref missingObj);
                    document = null;
                }
                catch (Exception er)
                {
                    logger.Error(er.Message);
                }
                logger.Error(error.Message);
                application = null;
            }
            application.Visible = true;
            return document;
        }
       
        /// <summary>
        /// Сохранение документа Word 
        /// </summary>
        /// <param name="path_save_documents">Префикс пути сохранения</param>        
        /// <param name="shopper">Покупатель</param>
        /// <param name="date_of_signing">Дата оформления</param>
        /// <param name="id_contract">id договора</param>
        /// <param name="document">Документ</param>
        /// <returns>path_save_documents - путь сохранения</returns>
        public static string SaveDocuments(string path_save_documents, Shopper shopper, DateTime date_of_signing, int id_contract, Word.Document document)
        {       
            path_save_documents += 
                shopper.Surname + 
                " " +
                shopper.First_name + 
                " " +
                shopper.Last_name + 
                " " +
                date_of_signing.ToString("dd.MM.yyyy") + 
                " " +
                id_contract.ToString() + 
                ".dot";
        linkStart:;
            try
            {
                //Защита от редактирования файла Word
                document.Protect(Word.WdProtectionType.wdAllowOnlyReading,true,"125479",missingObj, missingObj);
                document.SaveAs(path_save_documents);
            }
            catch (Exception error)
            {
                System.Windows.Forms.DialogResult dialogResult =
                System.Windows.Forms.MessageBox.Show("Ошибка сохранения!", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                if (dialogResult == System.Windows.Forms.DialogResult.Retry)
                {
                    logger.Error(error.Message);
                    goto linkStart;
                }
                else
                {
                    logger.Error(error.Message);
                    goto linkExit;
                }
            }
        linkExit:;
            return path_save_documents;       
        }
        /// <summary>
        /// Сохранение документа коммерческого предложения
        /// </summary>
        /// <param name="path_save_documents">Префикс пути сохранения</param>
        /// <param name="firm">Организация</param>
        /// <param name="date_of_issue">Дата предложения</param>
        /// <param name="id">id коммерческого предложения</param>
        /// <param name="document">документ Word</param>
        /// <returns>path_save_documents - путь сохранения</returns>
        public static string SaveDocumentsCommercial(string path_save_documents, Firm firm, DateTime date_of_issue, string type, int id, Word.Document document)
        {
            firm.Firm_name = firm.Firm_name.Replace("\"","'");
            path_save_documents +=
                firm.Firm_name + " " +
                date_of_issue.ToString("dd.MM.yyyy") + " " + type + " " +
                id.ToString() + ".dot";
        linkStart:;
            try
            {
                //Защита от редактирования файла Word
                document.Protect(Word.WdProtectionType.wdAllowOnlyReading, true, "125479", missingObj, missingObj);
                document.SaveAs(path_save_documents);
            }
            catch (Exception error)
            {
                System.Windows.Forms.DialogResult dialogResult = 
                System.Windows.Forms.MessageBox.Show("Ошибка сохранения! " + error, "Ошибка", System.Windows.Forms.MessageBoxButtons.RetryCancel, System.Windows.Forms.MessageBoxIcon.Error);
                if (dialogResult == System.Windows.Forms.DialogResult.Retry)
                {
                    logger.Error(error.Message);
                    goto linkStart;
                }
                else
                {
                    logger.Error(error.Message);
                    goto linkExit;
                }               
            }
        linkExit:;
            return path_save_documents;
        }

        public static string EditDocumentsName(string path_save_documents, string cause)
        {
            string path_new_documents = path_save_documents + "-" + cause;
        linkStart:;
            try
            {
                File.Copy(path_save_documents, path_new_documents);
            }
            catch (Exception error)
            {
                System.Windows.Forms.DialogResult dialogResult =
                System.Windows.Forms.MessageBox.Show("Ошибка сохранения новой копии файла! " + error, "Ошибка", System.Windows.Forms.MessageBoxButtons.RetryCancel, System.Windows.Forms.MessageBoxIcon.Error);
                if (dialogResult == System.Windows.Forms.DialogResult.Retry)
                {
                    logger.Error(error.Message);
                    goto linkStart;
                }
                else
                {
                    logger.Error(error.Message);
                    goto linkExit;
                }
            }           
        linkExit:;
            return path_new_documents;
        }
    }
}
