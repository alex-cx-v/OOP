using System;
using System.Linq;
using System.Windows.Forms;

namespace EquationApp
{
    // Клас для створення вкладки Tab7 з інтерфейсом користувача
    public class Tab7
    {
        // Поля для компонентів інтерфейсу
        private TabPage tabPage;
        private TextBox inputText;
        private Button processButton;
        private Label resultLabel;

        // Конструктор, що ініціалізує вкладку
        public Tab7()
        {
            tabPage = new TabPage("Tab 7"); // Створення вкладки
            InitializeComponents(); // Ініціалізація компонентів вкладки
        }

        // Метод для ініціалізації всіх компонентів вкладки
        private void InitializeComponents()
        {
            Label label = new Label { Left = 20, Top = 5, Text = "Enter text:", AutoSize = true };
            inputText = new TextBox { Left = 20, Top = 25, Width = 200 };

            processButton = new Button { Left = 20, Top = 60, Width = 100, Text = "Process" };
            processButton.Click += ProcessButton_Click;

            resultLabel = new Label { Left = 20, Top = 90, Width = 300, AutoSize = true, Text = "Result will appear here" };

            tabPage.Controls.Add(label);
            tabPage.Controls.Add(inputText);
            tabPage.Controls.Add(processButton);
            tabPage.Controls.Add(resultLabel);
        }

        // Обробник події для кнопки виконання
        private void ProcessButton_Click(object sender, EventArgs e)
        {
            string input = inputText.Text;
            string result = ProcessString(input);
            resultLabel.Text = $"Result: {result}";
        }

        // Метод для обробки вхідного рядка: подвоює всі символи, крім '*'
        private string ProcessString(string input)
        {
            return new string(input.Where(c => c != '*')
                                   .SelectMany(c => new string(c, 2))
                                   .ToArray());
        }

        // Метод для отримання вкладки
        public TabPage GetTab()
        {
            return tabPage;
        }
    }
}