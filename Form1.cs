using EquationApp;
using System;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        private TabControl tabControl1; // Контрол для вкладок

        public Form1()
        {
            InitializeComponent(); // Викликає автоматично згенерований метод ініціалізації компонентів
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.Clear(); // Очищення вкладок перед додаванням нових

            // Додаємо нові вкладки до TabControl
            tabControl1.TabPages.Add(new Tab1().GetTab());
            tabControl1.TabPages.Add(new Tab2().GetTab());
            tabControl1.TabPages.Add(new Tab3().GetTab());
            tabControl1.TabPages.Add(new Tab4().GetTab());
            tabControl1.TabPages.Add(new Tab5().GetTab());
            tabControl1.TabPages.Add(new Tab6().GetTab());
            tabControl1.TabPages.Add(new Tab7().GetTab());
        }
    }
}