using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Lab10
{
    public partial class Form1 : Form
    {
        private Panel panelOutput1;
        private Panel panelOutput2;
        private RichTextBox richTextBoxOutput3;
        private Button btnStart1, btnStop1;
        private Button btnStart2, btnStop2;
        private Button btnStart3, btnStop3;
        private Button btnStartAll, btnStopAll;

        Thread thread1;
        Thread thread2;
        Thread thread3;

        public Form1()
        {
            InitializeComponent();
            InitializeUI();

            thread1 = new Thread(draw_rect) { IsBackground = true };
            thread2 = new Thread(draw_eclips) { IsBackground = true };
            thread3 = new Thread(Rnd_num) { IsBackground = true };

            this.FormClosed += Form1_FormClosed;
        }

        private void InitializeUI()
        {
            panelOutput1 = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(10, 10),
                Size = new Size(200, 200),
                BackColor = Color.White
            };
            panelOutput2 = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(230, 10),
                Size = new Size(200, 200),
                BackColor = Color.White
            };
            richTextBoxOutput3 = new RichTextBox
            {
                Location = new Point(450, 10),
                Size = new Size(200, 200),
                BackColor = Color.White,
                ReadOnly = true
            };

            btnStart1 = new Button
            {
                Text = "Запустити 1 потік",
                Location = new Point(10, 220),
                Size = new Size(200, 30),
                BackColor = Color.Gainsboro
            };
            btnStop1 = new Button
            {
                Text = "Зупинити 1 потік",
                Location = new Point(10, 260),
                Size = new Size(200, 30),
                BackColor = Color.White,
                ForeColor = Color.Red
            };

            btnStart2 = new Button
            {
                Text = "Запустити 2 потік",
                Location = new Point(230, 220),
                Size = new Size(200, 30),
                BackColor = Color.Gainsboro
            };
            btnStop2 = new Button
            {
                Text = "Зупинити 2 потік",
                Location = new Point(230, 260),
                Size = new Size(200, 30),
                BackColor = Color.White,
                ForeColor = Color.Red
            };

            btnStart3 = new Button
            {
                Text = "Запустити 3 потік",
                Location = new Point(450, 220),
                Size = new Size(200, 30),
                BackColor = Color.Gainsboro
            };
            btnStop3 = new Button
            {
                Text = "Зупинити 3 потік",
                Location = new Point(450, 260),
                Size = new Size(200, 30),
                BackColor = Color.White,
                ForeColor = Color.Red
            };

            btnStartAll = new Button
            {
                Text = "Запустити всі потоки",
                Location = new Point(10, 310),
                Size = new Size(640, 30),
                BackColor = Color.Gainsboro
            };
            btnStopAll = new Button
            {
                Text = "Зупинити всі потоки",
                Location = new Point(10, 350),
                Size = new Size(640, 30),
                BackColor = Color.White,
                ForeColor = Color.Red
            };

            btnStart1.Click += btnStart1_Click;
            btnStop1.Click += btnStop1_Click;
            btnStart2.Click += btnStart2_Click;
            btnStop2.Click += btnStop2_Click;
            btnStart3.Click += btnStart3_Click;
            btnStop3.Click += btnStop3_Click;
            btnStartAll.Click += btnStartAll_Click;
            btnStopAll.Click += btnStopAll_Click;

            this.Controls.Add(panelOutput1);
            this.Controls.Add(panelOutput2);
            this.Controls.Add(richTextBoxOutput3);
            this.Controls.Add(btnStart1);
            this.Controls.Add(btnStop1);
            this.Controls.Add(btnStart2);
            this.Controls.Add(btnStop2);
            this.Controls.Add(btnStart3);
            this.Controls.Add(btnStop3);
            this.Controls.Add(btnStartAll);
            this.Controls.Add(btnStopAll);
        }

        private void draw_rect()
        {
            try
            {
                Random rnd = new Random();
                while (true)
                {
                    Thread.Sleep(40);
                    using (Graphics g = panelOutput1.CreateGraphics())
                    {
                        int w = rnd.Next(10, panelOutput1.Width);
                        int h = rnd.Next(10, panelOutput1.Height);
                        g.Clear(panelOutput1.BackColor);
                        g.DrawRectangle(Pens.Pink, 0, 0, w, h);
                    }
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception) { }
        }

        private void draw_eclips()
        {
            try
            {
                Random rnd = new Random();
                while (true)
                {
                    Thread.Sleep(40);
                    using (Graphics g = panelOutput2.CreateGraphics())
                    {
                        int w = rnd.Next(10, panelOutput2.Width);
                        int h = rnd.Next(10, panelOutput2.Height);
                        g.Clear(panelOutput2.BackColor);
                        g.DrawEllipse(Pens.Pink, 0, 0, w, h);
                    }
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception) { }
        }

        private void Rnd_num()
        {
            try
            {
                Random rnd = new Random();
                for (int i = 0; i < 500; i++)
                {
                    Thread.Sleep(10);
                    richTextBoxOutput3.Invoke((MethodInvoker)delegate ()
                    {
                        string num = "";
                        for (int j = 0; j < 10; j++)
                            num += rnd.Next(0, 10).ToString();
                        richTextBoxOutput3.AppendText(num + Environment.NewLine);
                    });
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try { if (thread1 != null && thread1.IsAlive) thread1.Abort(); } catch { }
            try { if (thread2 != null && thread2.IsAlive) thread2.Abort(); } catch { }
            try { if (thread3 != null && thread3.IsAlive) thread3.Abort(); } catch { }
        }

        private void btnStart1_Click(object sender, EventArgs e)
        {
            try
            {
                if (thread1 != null && thread1.IsAlive)
                {
                    MessageBox.Show("Потік 1 вже запущений!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    thread1 = new Thread(draw_rect) { IsBackground = true };
                    thread1.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Виникла помилка: " + ex.Message, "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnStop1_Click(object sender, EventArgs e)
        {
            try
            {
                if (thread1 != null && thread1.IsAlive)
                {
                    thread1.Abort();
                }
                else
                {
                    MessageBox.Show("Потік 1 вже зупинений!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (ThreadStateException)
            {
                MessageBox.Show("Потік 1 вже зупинений!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Виникла помилка: " + ex.Message, "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnStart2_Click(object sender, EventArgs e)
        {
            try
            {
                if (thread2 != null && thread2.IsAlive)
                {
                    MessageBox.Show("Потік 2 вже запущений!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    thread2 = new Thread(draw_eclips) { IsBackground = true };
                    thread2.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Виникла помилка: " + ex.Message, "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnStop2_Click(object sender, EventArgs e)
        {
            try
            {
                if (thread2 != null && thread2.IsAlive)
                {
                    thread2.Abort();
                }
                else
                {
                    MessageBox.Show("Потік 2 вже зупинений!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (ThreadStateException)
            {
                MessageBox.Show("Потік 2 вже зупинений!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Виникла помилка: " + ex.Message, "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnStart3_Click(object sender, EventArgs e)
        {
            try
            {
                if (thread3 != null && thread3.IsAlive)
                {
                    MessageBox.Show("Потік 3 вже запущений!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    thread3 = new Thread(Rnd_num) { IsBackground = true };
                    thread3.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Виникла помилка: " + ex.Message, "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnStop3_Click(object sender, EventArgs e)
        {
            try
            {
                if (thread3 != null && thread3.IsAlive)
                {
                    thread3.Abort();
                }
                else
                {
                    MessageBox.Show("Потік 3 вже зупинений!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (ThreadStateException)
            {
                MessageBox.Show("Потік 3 вже зупинений!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Виникла помилка: " + ex.Message, "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnStartAll_Click(object sender, EventArgs e)
        {
            btnStart1_Click(sender, e);
            btnStart2_Click(sender, e);
            btnStart3_Click(sender, e);
        }

        private void btnStopAll_Click(object sender, EventArgs e)
        {
            btnStop1_Click(sender, e);
            btnStop2_Click(sender, e);
            btnStop3_Click(sender, e);
        }
    }
}