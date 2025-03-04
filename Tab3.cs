using System;
using System.Globalization;
using System.Windows.Forms;

namespace EquationApp
{
    // Клас для створення вкладки Tab3 з інтерфейсом користувача
    public class Tab3
    {
        // Поля для компонентів інтерфейсу
        private TabPage tabPage;
        private TextBox inputA, inputB, inputC;
        private Button checkButton;
        private Label resultLabel;

        // Об'єкт для перевірки висловлення
        private ExpressionChecker checker;

        // Конструктор, що ініціалізує вкладку
        public Tab3()
        {
            checker = new ExpressionChecker(); // Ініціалізація перевірки висловлення
            tabPage = new TabPage("Tab 3"); // Створення вкладки

            InitializeComponents(); // Ініціалізація компонентів вкладки
        }

        // Метод для ініціалізації всіх компонентів вкладки
        private void InitializeComponents()
        {
            // Введення значення a
            Label labelA = new Label { Left = 20, Top = 5, Text = "Enter a:", AutoSize = true };
            inputA = new TextBox { Left = 20, Top = 25, Width = 100 };

            // Введення значення b
            Label labelB = new Label { Left = 20, Top = 55, Text = "Enter b:", AutoSize = true };
            inputB = new TextBox { Left = 20, Top = 75, Width = 100 };

            // Введення значення c
            Label labelC = new Label { Left = 20, Top = 105, Text = "Enter c:", AutoSize = true };
            inputC = new TextBox { Left = 20, Top = 125, Width = 100 };

            // Кнопка для перевірки виразу
            checkButton = new Button { Left = 20, Top = 160, Width = 100, Text = "Check" };
            checkButton.Click += CheckButton_Click; // Додавання події при натисканні кнопки

            // Мітка для відображення результату
            resultLabel = new Label { Left = 20, Top = 190, Width = 350, AutoSize = true, Text = "Result will appear here" };

            // Додавання всіх компонентів на вкладку
            tabPage.Controls.Add(labelA);
            tabPage.Controls.Add(inputA);
            tabPage.Controls.Add(labelB);
            tabPage.Controls.Add(inputB);
            tabPage.Controls.Add(labelC);
            tabPage.Controls.Add(inputC);
            tabPage.Controls.Add(checkButton);
            tabPage.Controls.Add(resultLabel);
        }

        // Обробник події для кнопки перевірки
        private void CheckButton_Click(object sender, EventArgs e)
        {
            // Перевірка введених значень a, b та c з підтримкою коми та крапки
            if (double.TryParse(inputA.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double a) &&
                double.TryParse(inputB.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double b) &&
                double.TryParse(inputC.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double c))
            {
                try
                {
                    // Перевірка висловлення і відображення результату
                    bool result = checker.Check(a, b, c);
                    resultLabel.Text = result ? "Result: True" : "Result: False"; // Виведення результату
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
                resultLabel.Text = "Invalid input! Please enter valid numbers.";
            }
        }

        // Клас для обчислення висловлення на основі умови
        public class ExpressionChecker
        {
            // Метод для перевірки умови "Серед чисел a, b, c є хоча б одна пара взаємно протилежних"
            public bool Check(double a, double b, double c)
            {
                // Перевірка, чи є хоча б одна пара взаємно протилежних чисел
                return (a == -b) || (a == -c) || (b == -c);
            }
        }

        // Метод для отримання вкладки
        public TabPage GetTab()
        {
            return tabPage;
        }
    }
}