using System;
using System.Globalization;
using System.Windows.Forms;

namespace EquationApp
{
    // Клас для створення вкладки Tab1 з інтерфейсом користувача
    public class Tab1
    {
        // Поля для компонентів інтерфейсу
        private TabPage tabPage;
        private TextBox inputX, inputY;
        private Button calculateButton;
        private Label resultLabel;

        // Конструктор, що ініціалізує вкладку
        public Tab1()
        {
            tabPage = new TabPage("Tab 1"); // Створення вкладки

            InitializeComponents(); // Ініціалізація компонентів вкладки
        }

        // Метод для ініціалізації всіх компонентів вкладки
        private void InitializeComponents()
        {
            // Введення значення x
            Label labelX = new Label { Left = 20, Top = 5, Text = "Enter x:", AutoSize = true };
            inputX = new TextBox { Left = 20, Top = 25, Width = 100 };

            // Введення значення y
            Label labelY = new Label { Left = 20, Top = 55, Text = "Enter y:", AutoSize = true };
            inputY = new TextBox { Left = 20, Top = 75, Width = 100 };

            // Кнопка для обчислення виразу
            calculateButton = new Button { Left = 20, Top = 110, Width = 100, Text = "Calculate" };
            calculateButton.Click += CalculateButton_Click; // Додавання події при натисканні кнопки

            // Мітка для відображення результату
            resultLabel = new Label { Left = 20, Top = 140, Width = 350, AutoSize = true, Text = "Result will appear here" };

            // Додавання всіх компонентів на вкладку
            tabPage.Controls.Add(labelX);
            tabPage.Controls.Add(inputX);
            tabPage.Controls.Add(labelY);
            tabPage.Controls.Add(inputY);
            tabPage.Controls.Add(calculateButton);
            tabPage.Controls.Add(resultLabel);
        }

        // Обробник події для кнопки обчислення
        private void CalculateButton_Click(object sender, EventArgs e)
        {
            // Перевірка введених значень x та y
            if (double.TryParse(inputX.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double x) &&
                double.TryParse(inputY.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double y))
            {
                try
                {
                    // Обчислення виразу
                    double result = CalculateExpression(x, y);
                    resultLabel.Text = $"Result: {result}"; // Виведення результату
                }
                catch (Exception ex)
                {
                    // Обробка помилок при обчисленні
                    resultLabel.Text = $"Error: {ex.Message}";
                }
            }
            else
            {
                // Повідомлення про некоректне введення
                resultLabel.Text = "Invalid input! Please enter valid numbers.";
            }
        }

        // Метод для обчислення виразу
        private double CalculateExpression(double x, double y)
        {
            // Перевірка на можливу помилку при обчисленні тангенса та котангенса
            if (Math.Tan(x) == 1)
            {
                throw new InvalidOperationException("Cannot calculate the expression due to division by zero.");
            }

            // Обчислення виразу
            double part1 = Math.Pow(1 - Math.Tan(x), 1 / Math.Tan(x)); // (1 - tan(x))^cot(x)
            double part2 = Math.Cos(x - y); // cos(x - y)

            return part1 + part2; // Результат виразу
        }

        // Метод для отримання вкладки
        public TabPage GetTab()
        {
            return tabPage;
        }
    }
}