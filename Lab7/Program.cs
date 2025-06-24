using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace Lab7
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Встановлюємо культуру з налаштувань
            string lang = Properties.Settings.Default.UILanguage;
            if (!string.IsNullOrEmpty(lang))
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}