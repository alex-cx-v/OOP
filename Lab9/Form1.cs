using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Lab9
{
    public partial class Form1 : Form
    {
        private PictureBox pictureBox1;
        private Label labelA;
        private TextBox textBoxA;
        private Label labelScale;
        private TrackBar trackBarScale;
        private Label labelScaleValue;
        private Button buttonDraw;

        private int currentScale = 80;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // PictureBox для графіка
            pictureBox1 = new PictureBox();
            pictureBox1.Location = new Point(20, 50);
            pictureBox1.Size = new Size(760, 380);
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pictureBox1);

            // Label для коефіцієнта a
            labelA = new Label();
            labelA.Text = "a =";
            labelA.Location = new Point(20, 15);
            labelA.AutoSize = true;
            this.Controls.Add(labelA);

            // TextBox для введення a
            textBoxA = new TextBox();
            textBoxA.Location = new Point(45, 12);
            textBoxA.Size = new Size(60, 20);
            textBoxA.Text = "1";
            this.Controls.Add(textBoxA);

            // Label для масштабу
            labelScale = new Label();
            labelScale.Text = "Масштаб:";
            labelScale.Location = new Point(120, 15);
            labelScale.AutoSize = true;
            this.Controls.Add(labelScale);

            // TrackBar для масштабу
            trackBarScale = new TrackBar();
            trackBarScale.Location = new Point(190, 8);
            trackBarScale.Size = new Size(120, 40);
            trackBarScale.Minimum = 20;
            trackBarScale.Maximum = 200;
            trackBarScale.Value = currentScale;
            trackBarScale.TickFrequency = 20;
            trackBarScale.SmallChange = 5;
            trackBarScale.LargeChange = 20;
            trackBarScale.Scroll += TrackBarScale_Scroll;
            this.Controls.Add(trackBarScale);

            // Label для відображення значення масштабу
            labelScaleValue = new Label();
            labelScaleValue.Text = currentScale.ToString();
            labelScaleValue.Location = new Point(320, 15);
            labelScaleValue.AutoSize = true;
            this.Controls.Add(labelScaleValue);

            // Button для побудови графіка
            buttonDraw = new Button();
            buttonDraw.Text = "Побудувати";
            buttonDraw.Location = new Point(370, 10);
            buttonDraw.Size = new Size(90, 23);
            buttonDraw.Click += buttonDraw_Click;
            this.Controls.Add(buttonDraw);
        }

        private void TrackBarScale_Scroll(object sender, EventArgs e)
        {
            currentScale = trackBarScale.Value;
            labelScaleValue.Text = currentScale.ToString();
            // Автоматично перемальовуємо графік при зміні масштабу, якщо введення коректне
            if (double.TryParse(textBoxA.Text.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out double a))
            {
                DrawCurve(a, currentScale);
            }
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBoxA.Text.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out double a))
            {
                MessageBox.Show("Введіть коректне число для a.");
                return;
            }
            DrawCurve(a, currentScale);
        }

        private void DrawCurve(double a, int scale)
        {
            int w = pictureBox1.Width;
            int h = pictureBox1.Height;
            Bitmap bmp = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            // Малюємо осі
            Pen axisPen = new Pen(Color.Black, 1);
            g.DrawLine(axisPen, w / 2, 0, w / 2, h); // Y
            g.DrawLine(axisPen, 0, h / 2, w, h / 2); // X

            // Підписи осей
            Font font = new Font("Arial", 10);
            // Y - зліва від осі
            g.DrawString("Y", font, Brushes.Black, w / 2 - 20, 5);
            // X - зверху від осі
            g.DrawString("X", font, Brushes.Black, w - 25, h / 2 - 25);

            // Динамічні підписи на осях
            int maxX = (w / 2) / scale;
            int maxY = (h / 2) / scale;

            // X-вісь
            for (int i = 1; i <= maxX; i++)
            {
                int xRight = w / 2 + i * scale;
                int xLeft = w / 2 - i * scale;
                if (xRight < w)
                {
                    g.DrawLine(Pens.Gray, xRight, h / 2 - 5, xRight, h / 2 + 5);
                    g.DrawString(i.ToString(), font, Brushes.Black, xRight - 10, h / 2 + 8);
                }
                if (xLeft > 0)
                {
                    g.DrawLine(Pens.Gray, xLeft, h / 2 - 5, xLeft, h / 2 + 5);
                    g.DrawString((-i).ToString(), font, Brushes.Black, xLeft - 10, h / 2 + 8);
                }
            }

            // Y-вісь
            for (int i = 1; i <= maxY; i++)
            {
                int yUp = h / 2 - i * scale;
                int yDown = h / 2 + i * scale;
                if (yUp > 0)
                {
                    g.DrawLine(Pens.Gray, w / 2 - 5, yUp, w / 2 + 5, yUp);
                    g.DrawString(i.ToString(), font, Brushes.Black, w / 2 + 8, yUp - 10);
                }
                if (yDown < h)
                {
                    g.DrawLine(Pens.Gray, w / 2 - 5, yDown, w / 2 + 5, yDown);
                    g.DrawString((-i).ToString(), font, Brushes.Black, w / 2 + 8, yDown - 10);
                }
            }

            // Малюємо криву
            Pen curvePen = new Pen(Color.Red, 2);
            double tMin = -Math.PI / 2 + 0.05;
            double tMax = Math.PI / 2 - 0.05;
            double step = 0.01;
            PointF? prev = null;

            for (double t = tMin; t <= tMax; t += step)
            {
                double x = a * Math.Tan(t);
                double y = a * Math.Pow(Math.Cos(t), 2);

                // Масштабування
                float px = (float)(w / 2 + x * scale);
                float py = (float)(h / 2 - y * scale);

                if (prev != null)
                {
                    // Перевірка на вихід за межі області малювання
                    if (px >= 0 && px < w && py >= 0 && py < h && prev.Value.X >= 0 && prev.Value.X < w && prev.Value.Y >= 0 && prev.Value.Y < h)
                        g.DrawLine(curvePen, prev.Value, new PointF(px, py));
                }
                prev = new PointF(px, py);
            }

            pictureBox1.Image = bmp;
        }
    }
}