using System;
using System.Globalization;
using System.Windows.Forms;

namespace EquationApp
{
    // Клас для створення вкладки Tab2 з інтерфейсом користувача
    public class Tab2
    {
        // Поля для компонентів інтерфейсу
        private TabPage tabPage;
        private TextBox inputA;
        private Button checkButton;
        private Label resultLabel;

        // Конструктор, що ініціалізує вкладку
        public Tab2()
        {
            tabPage = new TabPage("Tab 2"); // Створення вкладки

            InitializeComponents(); // Ініціалізація компонентів вкладки
        }

        // Метод для ініціалізації всіх компонентів вкладки
        private void InitializeComponents()
        {
            // Введення значення a
            Label labelA = new Label { Left = 20, Top = 5, Text = "Enter a:", AutoSize = true };
            inputA = new TextBox { Left = 20, Top = 25, Width = 100 };

            // Кнопка для перевірки виразу
            checkButton = new Button { Left = 20, Top = 60, Width = 100, Text = "Check" };
            checkButton.Click += CheckButton_Click; // Додавання події при натисканні кнопки

            // Мітка для відображення результату
            resultLabel = new Label { Left = 20, Top = 90, Width = 350, AutoSize = true, Text = "Result will appear here" };

            // Додавання всіх компонентів на вкладку
            tabPage.Controls.Add(labelA);
            tabPage.Controls.Add(inputA);
            tabPage.Controls.Add(checkButton);
            tabPage.Controls.Add(resultLabel);
        }

        // Обробник події для кнопки обчислення
        private void CheckButton_Click(object sender, EventArgs e)
        {
            // Перевірка введеного значення a з підтримкою коми та крапки
            if (double.TryParse(inputA.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double a))
            {
                try
                {
                    // Обчислення a^8 за три операції
                    double a2 = a * a;  // a^2
                    double a4 = a2 * a2;  // a^4
                    double a8 = a4 * a4;  // a^8

                    // Обчислення a^10 за чотири операції
                    double a10 = a8 * a2;  // a^10

                    // Виведення результатів
                    resultLabel.Text = $"a^8 = {a8}, a^10 = {a10}";
                }
                catch (Exception ex)
                {
                    // Обробка помилок при перевірці виразу
                    resultLabel.Text = $"Error: {ex.Message}";
                }
            }
            else
            {
                // Повідомлення про некоректне введення
                resultLabel.Text = "Invalid input! Please enter a valid number.";
            }
        }

        // Метод для отримання вкладки
        public TabPage GetTab()
        {
            return tabPage;
        }
    }
}