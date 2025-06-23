using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab3
{
    // Абстрактний базовий клас Function
    public abstract class Function
    {
        public abstract double[] Calculate(double x);
        public virtual void PrintResult(double x, Label label)
        {
            var y = Calculate(x);
            label.Text = $"x = {x:F2}; y = {string.Join("; y = ", y)}";
        }
    }

    // Клас Ellipse: y(x) = ±3 * sqrt(1 - x^2/25)
    public class Ellipse : Function
    {
        public override double[] Calculate(double x)
        {
            double underRoot = 1 - (x * x) / 25.0;
            if (underRoot < 0)
                return new double[0]; // немає дійсних значень
            double y = 3 * Math.Sqrt(underRoot);
            return new double[] { y, -y };
        }
    }

    // Клас Hyperbola: y(x) = 4/x
    public class Hyperbola : Function
    {
        public override double[] Calculate(double x)
        {
            if (x == 0)
                return new double[0]; // немає значення
            return new double[] { 4.0 / x };
        }
    }

    public partial class Form1 : Form
    {
        private ComboBox comboBoxFunction;
        private TextBox textBoxX;
        private Button buttonCalc;
        private Label labelResult;

        public Form1()
        {
            InitializeComponent();
            InitUI();
        }

        private void InitUI()
        {
            this.Text = "Function Calculator";
            this.ClientSize = new Size(400, 200);

            comboBoxFunction = new ComboBox
            {
                Location = new Point(20, 20),
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboBoxFunction.Items.Add("Ellipse");
            comboBoxFunction.Items.Add("Hyperbola");
            comboBoxFunction.SelectedIndex = 0;

            textBoxX = new TextBox
            {
                Location = new Point(200, 20),
                Width = 100
            };

            buttonCalc = new Button
            {
                Location = new Point(320, 20),
                Text = "Calculate",
                Width = 60
            };
            buttonCalc.Click += ButtonCalc_Click;

            labelResult = new Label
            {
                Location = new Point(20, 70),
                Width = 360,
                Height = 60,
                AutoSize = false
            };

            this.Controls.Add(comboBoxFunction);
            this.Controls.Add(textBoxX);
            this.Controls.Add(buttonCalc);
            this.Controls.Add(labelResult);
        }

        private void ButtonCalc_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBoxX.Text, out double x))
            {
                labelResult.Text = "Введіть коректне число для x.";
                return;
            }

            Function func = comboBoxFunction.SelectedIndex == 0 ? (Function)new Ellipse() : new Hyperbola();
            var y = func.Calculate(x);

            if (y.Length == 0)
            {
                labelResult.Text = "Немає дійсних значень для цього x.";
            }
            else
            {
                func.PrintResult(x, labelResult);
            }
        }
    }
}