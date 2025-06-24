using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace Lab7
{
    public partial class Form1 : Form
    {
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileMenu, newMenu, openMenu, saveMenu, exitMenu;
        private ToolStripMenuItem editMenu, fontMenu, alignLeftMenu, alignCenterMenu, alignRightMenu, insertImageMenu;
        private ToolStripMenuItem languageMenu, langEnMenu, langUkMenu;
        private ToolStripMenuItem syntaxMenu, syntaxHighlightMenu;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private FontDialog fontDialog;

        public Form1()
        {
            InitializeComponent();
            IsMdiContainer = true;
            InitializeMenus();
        }

        private void InitializeMenus()
        {
            menuStrip = new MenuStrip();

            // File menu
            fileMenu = new ToolStripMenuItem(Properties.Resources.MenuFile);
            newMenu = new ToolStripMenuItem(Properties.Resources.MenuNew, null, NewMenu_Click);
            openMenu = new ToolStripMenuItem(Properties.Resources.MenuOpen, null, OpenMenu_Click);
            saveMenu = new ToolStripMenuItem(Properties.Resources.MenuSave, null, SaveMenu_Click);
            exitMenu = new ToolStripMenuItem(Properties.Resources.MenuExit, null, (s, e) => Close());
            fileMenu.DropDownItems.AddRange(new[] { newMenu, openMenu, saveMenu, exitMenu });

            // Edit menu
            editMenu = new ToolStripMenuItem(Properties.Resources.MenuEdit);
            fontMenu = new ToolStripMenuItem(Properties.Resources.MenuFont, null, FontMenu_Click);
            alignLeftMenu = new ToolStripMenuItem(Properties.Resources.MenuAlignLeft, null, (s, e) => SetAlignment(HorizontalAlignment.Left));
            alignCenterMenu = new ToolStripMenuItem(Properties.Resources.MenuAlignCenter, null, (s, e) => SetAlignment(HorizontalAlignment.Center));
            alignRightMenu = new ToolStripMenuItem(Properties.Resources.MenuAlignRight, null, (s, e) => SetAlignment(HorizontalAlignment.Right));
            insertImageMenu = new ToolStripMenuItem(Properties.Resources.MenuInsertImage, null, InsertImageMenu_Click);
            editMenu.DropDownItems.AddRange(new[] { fontMenu, alignLeftMenu, alignCenterMenu, alignRightMenu, insertImageMenu });

            // Language menu
            languageMenu = new ToolStripMenuItem(Properties.Resources.MenuLanguage);
            langUkMenu = new ToolStripMenuItem(Properties.Resources.MenuUkrainian, null, (s, e) => ChangeLanguage("uk"));
            langEnMenu = new ToolStripMenuItem(Properties.Resources.MenuEnglish, null, (s, e) => ChangeLanguage("en"));
            languageMenu.DropDownItems.AddRange(new[] { langUkMenu, langEnMenu });

            // Syntax menu
            syntaxMenu = new ToolStripMenuItem(Properties.Resources.MenuSyntax);
            syntaxHighlightMenu = new ToolStripMenuItem(Properties.Resources.MenuSyntaxHighlight, null, SyntaxHighlightMenu_Click);
            syntaxMenu.DropDownItems.Add(syntaxHighlightMenu);

            menuStrip.Items.AddRange(new[] { fileMenu, editMenu, languageMenu, syntaxMenu });
            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);

            openFileDialog = new OpenFileDialog { Filter = "RTF files|*.rtf|Text files|*.txt" };
            saveFileDialog = new SaveFileDialog { Filter = "RTF files|*.rtf" };
            fontDialog = new FontDialog();
        }

        private void NewMenu_Click(object sender, EventArgs e)
        {
            var child = new ChildEditorForm();
            child.MdiParent = this;
            child.Text = Properties.Resources.DocTitle;
            child.Show();
        }

        private void OpenMenu_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var child = new ChildEditorForm();
                child.MdiParent = this;
                child.Text = Properties.Resources.DocTitle;
                if (openFileDialog.FileName.EndsWith(".rtf"))
                    child.Editor.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.RichText);
                else
                    child.Editor.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                child.Show();
            }
        }

        private void SaveMenu_Click(object sender, EventArgs e)
        {
            var child = ActiveMdiChild as ChildEditorForm;
            if (child == null) return;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                child.Editor.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
            }
        }

        private void FontMenu_Click(object sender, EventArgs e)
        {
            var child = ActiveMdiChild as ChildEditorForm;
            if (child == null) return;
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                child.Editor.SelectionFont = fontDialog.Font;
            }
        }

        private void SetAlignment(HorizontalAlignment alignment)
        {
            var child = ActiveMdiChild as ChildEditorForm;
            if (child == null) return;
            child.Editor.SelectionAlignment = alignment;
        }

        private void InsertImageMenu_Click(object sender, EventArgs e)
        {
            var child = ActiveMdiChild as ChildEditorForm;
            if (child == null) return;
            using (var imgDialog = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif" })
            {
                if (imgDialog.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(imgDialog.FileName);
                    Clipboard.SetImage(img);
                    child.Editor.Paste();
                }
            }
        }

        private void ChangeLanguage(string lang)
        {
            // Зберігаємо вибір мови у налаштуваннях користувача
            Properties.Settings.Default.UILanguage = lang;
            Properties.Settings.Default.Save();
            MessageBox.Show(Properties.Resources.MsgRestart);
            Application.Restart();
        }

        private void SyntaxHighlightMenu_Click(object sender, EventArgs e)
        {
            var child = ActiveMdiChild as ChildEditorForm;
            if (child == null) return;
            HighlightCSyntax(child.Editor);
        }

        // Примітивне підсвічування синтаксису C-подібних мов
        private void HighlightCSyntax(RichTextBox box)
        {
            string[] keywords = { "int", "float", "double", "char", "if", "else", "for", "while", "return", "void", "class", "public", "private", "protected", "static", "const", "struct", "break", "continue", "switch", "case", "default", "do", "enum", "typedef", "union", "unsigned", "signed", "long", "short", "bool", "true", "false", "namespace", "using", "include" };
            int selStart = box.SelectionStart;
            int selLength = box.SelectionLength;

            box.SelectAll();
            box.SelectionColor = Color.Black;

            foreach (var keyword in keywords)
            {
                int start = 0;
                while ((start = box.Text.IndexOf(keyword, start, StringComparison.InvariantCulture)) != -1)
                {
                    // Перевірка, що це окреме слово
                    bool isWord =
                        (start == 0 || !char.IsLetterOrDigit(box.Text[start - 1])) &&
                        (start + keyword.Length == box.Text.Length || !char.IsLetterOrDigit(box.Text[start + keyword.Length]));
                    if (isWord)
                    {
                        box.Select(start, keyword.Length);
                        box.SelectionColor = Color.Blue;
                    }
                    start += keyword.Length;
                }
            }
            box.Select(selStart, selLength);
            box.SelectionColor = Color.Black;
        }
    }

    // Дочірня форма редактора
    public class ChildEditorForm : Form
    {
        public RichTextBox Editor { get; private set; }
        public ChildEditorForm()
        {
            Text = Properties.Resources.DocTitle;
            Editor = new RichTextBox { Dock = DockStyle.Fill, AcceptsTab = true, HideSelection = false };
            Controls.Add(Editor);
        }
    }
}