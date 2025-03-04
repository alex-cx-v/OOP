using System;
using System.Globalization;
using System.Windows.Forms;

namespace EquationApp
{
    // Клас для створення вкладки Tab4 з інтерфейсом користувача
    public class Tab4
    {
        // Поля для компонентів інтерфейсу
        private TabPage tabPage;
        private TextBox inputA, inputB, inputC, inputD;
        private Button checkButton;
        private Label resultLabel;

        // Конструктор, що ініціалізує вкладку
        public Tab4()
        {
            tabPage = new TabPage("Tab 4"); // Створення вкладки

            InitializeComponents(); // Ініціалізація компонентів вкладки
        }

        // Метод для ініціалізації всіх компонентів вкладки
        private void InitializeComponents()
        {
            // Введення значення a
            Label labelA = new Label { Left = 20, Top = 5, Text = "Enter a1:", AutoSize = true };
            inputA = new TextBox { Left = 20, Top = 25, Width = 100 };

            // Введення значення b
            Label labelB = new Label { Left = 20, Top = 55, Text = "Enter a2:", AutoSize = true };
            inputB = new TextBox { Left = 20, Top = 75, Width = 100 };

            // Введення значення c
            Label labelC = new Label { Left = 20, Top = 105, Text = "Enter a3:", AutoSize = true };
            inputC = new TextBox { Left = 20, Top = 125, Width = 100 };

            // Введення значення d
            Label labelD = new Label { Left = 20, Top = 155, Text = "Enter a4:", AutoSize = true };
            inputD = new TextBox { Left = 20, Top = 175, Width = 100 };

            // Кнопка для перевірки виразу
            checkButton = new Button { Left = 20, Top = 210, Width = 100, Text = "Check" };
            checkButton.Click += CheckButton_Click; // Додавання події при натисканні кнопки

            // Мітка для відображення результату
            resultLabel = new Label { Left = 20, Top = 240, Width = 350, AutoSize = true, Text = "Result will appear here" };

            // Додавання всіх компонентів на вкладку
            tabPage.Controls.Add(labelA);
            tabPage.Controls.Add(inputA);
            tabPage.Controls.Add(labelB);
            tabPage.Controls.Add(inputB);
            tabPage.Controls.Add(labelC);
            tabPage.Controls.Add(inputC);
            tabPage.Controls.Add(labelD);
            tabPage.Controls.Add(inputD);
            tabPage.Controls.Add(checkButton);
            tabPage.Controls.Add(resultLabel);
        }

        // Обробник події для кнопки перевірки
        private void CheckButton_Click(object sender, EventArgs e)
        {
            // Перевірка введених значень a1, a2, a3 і a4 з підтримкою коми та крапки
            if (double.TryParse(inputA.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double a1) &&
                double.TryParse(inputB.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double a2) &&
                double.TryParse(inputC.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double a3) &&
                double.TryParse(inputD.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double a4))
            {
                // Логіка для визначення числа, яке відрізняється
                int n = -1; // Індекс змінної, що відрізняється (за замовчуванням значення -1 означає, що не знайдено відмінне число)
                if (a1 == a2 && a1 == a3 && a1 != a4) n = 4; // a4 відрізняється
                else if (a1 == a2 && a1 == a4 && a1 != a3) n = 3; // a3 відрізняється
                else if (a1 == a3 && a1 == a4 && a1 != a2) n = 2; // a2 відрізняється
                else if (a2 == a3 && a2 == a4 && a2 != a1) n = 1; // a1 відрізняється

                // Виведення результату
                if (n != -1)
                    resultLabel.Text = $"The number that differs is a{n}.";
                else
                    resultLabel.Text = "No number differs.";
            }
            else
            {
                // Повідомлення про некоректне введення
                resultLabel.Text = "Invalid input! Please enter valid numbers.";
            }
        }

        // Метод для отримання вкладки
        public TabPage GetTab()
        {
            return tabPage;
        }
    }
}