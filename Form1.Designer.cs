using System.Windows.Forms;

namespace Lab1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Text = "Equation App";

            // Ініціалізація TabControl
            this.tabControl1 = new TabControl
            {
                Dock = DockStyle.Fill // Налаштування TabControl для заповнення всієї форми
            };

            this.Controls.Add(tabControl1); // Додаємо TabControl до форми
            this.Load += Form1_Load; // Обробник події для завантаження форми
        }

        #endregion
    }
}