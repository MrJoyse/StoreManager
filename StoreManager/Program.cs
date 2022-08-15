using System;
using System.Windows.Forms;
using NLog;

namespace StoreManager
{
    static class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //для предотвращения запуска копии приложения 82106f54-d6f1-48b9-8146-29208f5c3ee8 --> GUID
        static System.Threading.Mutex mutex = new System.Threading.Mutex(true, "{82106f54-d6f1-48b9-8146-29208f5c3ee8}");
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*logger.Trace("trace message");
            logger.Debug("debug message");
            logger.Info("info message");
            logger.Warn("warn message");
            logger.Error("error message");
            logger.Fatal("fatal message");*/
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                logger.Info("Программа запущена");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormLogin());
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("Приложение уже запущено","Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                logger.Info("Попытка запуска дубля программы");
                return;
            }           
        }
    }
}
