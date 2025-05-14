using System;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {
        private ListBox listBox1;
        private ListBox listBox2;
        private ListBox listBoxResult;
        private Button btnConcat;
        private Button btnMerge;
        private Button btnPrintAll;
        private Label label1;
        private Label label2;
        private Label labelResult;

        private FixedStringArray arr1;
        private FixedStringArray arr2;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomLayout();
        }

        private void InitializeCustomLayout()
        {
            // ListBox для першого масиву
            listBox1 = new ListBox
            {
                Location = new System.Drawing.Point(20, 40),
                Size = new System.Drawing.Size(120, 100)
            };
            this.Controls.Add(listBox1);

            // ListBox для другого масиву
            listBox2 = new ListBox
            {
                Location = new System.Drawing.Point(160, 40),
                Size = new System.Drawing.Size(120, 100)
            };
            this.Controls.Add(listBox2);

            // ListBox для результату
            listBoxResult = new ListBox
            {
                Location = new System.Drawing.Point(300, 40),
                Size = new System.Drawing.Size(150, 100)
            };
            this.Controls.Add(listBoxResult);

            // Кнопка поелементного зчеплення
            btnConcat = new Button
            {
                Text = "Concat Elementwise",
                Location = new System.Drawing.Point(20, 160),
                Size = new System.Drawing.Size(120, 30)
            };
            btnConcat.Click += BtnConcat_Click;
            this.Controls.Add(btnConcat);

            // Кнопка злиття без повторів
            btnMerge = new Button
            {
                Text = "Merge Unique",
                Location = new System.Drawing.Point(160, 160),
                Size = new System.Drawing.Size(120, 30)
            };
            btnMerge.Click += BtnMerge_Click;
            this.Controls.Add(btnMerge);

            // Кнопка друку всіх елементів
            btnPrintAll = new Button
            {
                Text = "Print All",
                Location = new System.Drawing.Point(300, 160),
                Size = new System.Drawing.Size(120, 30)
            };
            btnPrintAll.Click += BtnPrintAll_Click;
            this.Controls.Add(btnPrintAll);

            // Підписи
            label1 = new Label
            {
                Text = "Array 1",
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(label1);

            label2 = new Label
            {
                Text = "Array 2",
                Location = new System.Drawing.Point(160, 20),
                AutoSize = true
            };
            this.Controls.Add(label2);

            labelResult = new Label
            {
                Text = "Result",
                Location = new System.Drawing.Point(300, 20),
                AutoSize = true
            };
            this.Controls.Add(labelResult);

            // Ініціалізація масивів
            arr1 = new FixedStringArray(new[] { "one", "two", "three" });
            arr2 = new FixedStringArray(new[] { "A", "B", "three" });

            UpdateListBoxes();
        }

        private void UpdateListBoxes()
        {
            listBox1.Items.Clear();
            foreach (var s in arr1.ToArray())
                listBox1.Items.Add(s);

            listBox2.Items.Clear();
            foreach (var s in arr2.ToArray())
                listBox2.Items.Add(s);

            listBoxResult.Items.Clear();
        }

        private void BtnConcat_Click(object sender, EventArgs e)
        {
            var result = FixedStringArray.ConcatElementwise(arr1, arr2);
            listBoxResult.Items.Clear();
            foreach (var s in result.ToArray())
                listBoxResult.Items.Add(s);
        }

        private void BtnMerge_Click(object sender, EventArgs e)
        {
            var result = FixedStringArray.MergeUnique(arr1, arr2);
            listBoxResult.Items.Clear();
            foreach (var s in result.ToArray())
                listBoxResult.Items.Add(s);
        }

        private void BtnPrintAll_Click(object sender, EventArgs e)
        {
            listBoxResult.Items.Clear();
            foreach (var s in arr1.ToArray())
                listBoxResult.Items.Add("Array1: " + s);
            foreach (var s in arr2.ToArray())
                listBoxResult.Items.Add("Array2: " + s);
        }
    }
}