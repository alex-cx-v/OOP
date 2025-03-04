using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace EquationApp
{
    // Клас для створення вкладки Tab6 з інтерфейсом користувача
    public class Tab6
    {
        // Поля для компонентів інтерфейсу
        private TabPage tabPage;
        private TextBox inputSequence;
        private Button processButton;
        private Label resultLabel;

        // Конструктор, що ініціалізує вкладку
        public Tab6()
        {
            tabPage = new TabPage("Tab 6"); // Створення вкладки
            InitializeComponents(); // Ініціалізація компонентів вкладки
        }

        // Метод для ініціалізації всіх компонентів вкладки
        private void InitializeComponents()
        {
            Label labelSeq = new Label { Left = 20, Top = 5, Text = "Enter sequence (comma-separated):", AutoSize = true };
            inputSequence = new TextBox { Left = 20, Top = 25, Width = 250 };

            processButton = new Button { Left = 20, Top = 60, Width = 100, Text = "Process" };
            processButton.Click += ProcessButton_Click;

            resultLabel = new Label { Left = 20, Top = 100, Width = 350, AutoSize = true, Text = "Result will appear here" };

            tabPage.Controls.Add(labelSeq);
            tabPage.Controls.Add(inputSequence);
            tabPage.Controls.Add(processButton);
            tabPage.Controls.Add(resultLabel);
        }

        // Обробник події для кнопки виконання
        private void ProcessButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Розділяємо введений рядок на числа, парсимо і конвертуємо в масив
                double[] sequence = inputSequence.Text.Split(',')
                    .Select(s => double.Parse(s.Trim(), CultureInfo.InvariantCulture))
                    .ToArray();

                if (sequence.Length == 0)
                {
                    resultLabel.Text = "Error: Empty sequence.";
                    return;
                }

                // Знаходимо мінімальне і максимальне значення у послідовності
                double min = sequence.Min();
                double max = sequence.Max();

                // Обчислюємо квадрати мінімального і максимального значень
                double minSquared = min * min;
                double maxSquared = max * max;

                // Модифікуємо послідовність за умовою: додатні числа множимо на квадрат мінімуму, від’ємні — на квадрат максимуму
                double[] modifiedSequence = sequence
                    .Select(x => x >= 0 ? x * minSquared : x * maxSquared)
                    .ToArray();

                // Виводимо змінену послідовність
                resultLabel.Text = "Modified sequence: " + string.Join(", ", modifiedSequence.Select(x => x.ToString(CultureInfo.InvariantCulture)));
            }
            catch (Exception ex)
            {
                // Вивід помилки у разі виникнення виключення
                resultLabel.Text = $"Error: {ex.Message}";
            }
        }

        // Метод для отримання вкладки
        public TabPage GetTab()
        {
            return tabPage;
        }
    }
}