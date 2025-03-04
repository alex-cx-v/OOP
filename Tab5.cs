using System;
using System.Globalization;
using System.Windows.Forms;

namespace EquationApp
{
    // Клас для створення вкладки Tab5 з інтерфейсом користувача
    public class Tab5
    {
        // Поля для компонентів інтерфейсу
        private TabPage tabPage;
        private TextBox inputDay;
        private Button checkButton;
        private Label resultLabel;
        private CheckBox leapYearCheckBox; // Поле для CheckBox

        // Конструктор, що ініціалізує вкладку
        public Tab5()
        {
            tabPage = new TabPage("Tab 5"); // Створення вкладки
            InitializeComponents(); // Ініціалізація компонентів вкладки
        }

        // Метод для ініціалізації всіх компонентів вкладки
        private void InitializeComponents()
        {
            // CheckBox для вибору високосного року
            leapYearCheckBox = new CheckBox { Left = 20, Top = 5, Text = "Leap Year", AutoSize = true };

            // Введення значення дня
            Label labelDay = new Label { Left = 20, Top = 35, Text = "Enter day number:", AutoSize = true };
            inputDay = new TextBox { Left = 20, Top = 55, Width = 100 };

            // Кнопка для перевірки
            checkButton = new Button { Left = 20, Top = 90, Width = 100, Text = "Check" };
            checkButton.Click += CheckButton_Click; // Додавання події при натисканні кнопки

            // Мітка для відображення результату
            resultLabel = new Label { Left = 20, Top = 130, Width = 350, AutoSize = true, Text = "Result will appear here" };

            // Додавання всіх компонентів на вкладку
            tabPage.Controls.Add(leapYearCheckBox); // Додавання CheckBox на початок
            tabPage.Controls.Add(labelDay);
            tabPage.Controls.Add(inputDay);
            tabPage.Controls.Add(checkButton);
            tabPage.Controls.Add(resultLabel);
        }

        // Обробник події для кнопки перевірки
        private void CheckButton_Click(object sender, EventArgs e)
        {
            // Перевірка введеного дня
            if (int.TryParse(inputDay.Text, out int dayNumber) && dayNumber >= 1 && dayNumber <= 366)
            {
                try
                {
                    // Визначення, чи високосний рік
                    bool isLeapYear = leapYearCheckBox.Checked;
                    // Визначення місяця і числа
                    string result = GetMonthDayFromDayNumber(dayNumber, isLeapYear);
                    resultLabel.Text = result; // Виведення результату
                }
                catch (Exception ex)
                {
                    // Обробка помилок
                    resultLabel.Text = $"Error: {ex.Message}";
                }
            }
            else
            {
                // Повідомлення про некоректне введення
                resultLabel.Text = "Invalid input! Please enter a valid day number (1-366).";
            }
        }

        // Метод для отримання місяця і числа з номеру дня
        private string GetMonthDayFromDayNumber(int dayNumber, bool isLeapYear)
        {
            // Масив кількості днів у кожному місяці для невисокосного року
            int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            // Якщо рік високосний, лютому додається ще один день
            if (isLeapYear)
            {
                daysInMonth[1] = 29;
            }

            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            int month = 0;
            while (dayNumber > daysInMonth[month])
            {
                dayNumber -= daysInMonth[month];
                month++;
            }

            return $"{months[month]} {dayNumber}"; // Форматований результат (місяць спочатку, потім число)
        }

        // Метод для отримання вкладки
        public TabPage GetTab()
        {
            return tabPage;
        }
    }
}