using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace Lab8
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }

        private RichTextBox CurrentEditor
        {
            get
            {
                if (tabControl.SelectedItem is TabItem tab && tab.Content is RichTextBox rtb)
                    return rtb;
                return null;
            }
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var rtb = sender as RichTextBox;
            object temp = rtb.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && temp.Equals(FontWeights.Bold);
            temp = rtb.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && temp.Equals(FontStyles.Italic);
            temp = rtb.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && temp.Equals(TextDecorations.Underline);
            temp = rtb.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;
            temp = rtb.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmbFontSize.Text = temp.ToString();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var rtb = CurrentEditor;
            if (rtb == null) return;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(dlg.FileName, FileMode.Open))
                {
                    TextRange range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                    range.Load(fs, DataFormats.Rtf);
                }
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var rtb = CurrentEditor;
            if (rtb == null) return;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(dlg.FileName, FileMode.Create))
                {
                    TextRange range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                    range.Save(fs, DataFormats.Rtf);
                }
            }
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var rtb = CurrentEditor;
            if (rtb != null && cmbFontFamily.SelectedItem != null)
                rtb.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
        }

        private void cmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var rtb = CurrentEditor;
            if (rtb != null && cmbFontSize.SelectedItem != null)
                rtb.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.SelectedItem);
        }

        private void cmbFontSize_Loaded(object sender, RoutedEventArgs e)
        {
            if (cmbFontSize.Template.FindName("PART_EditableTextBox", cmbFontSize) is TextBox tb)
                tb.TextChanged += cmbFontSize_TextBox_TextChanged;
        }

        private void cmbFontSize_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var rtb = CurrentEditor;
            if (rtb != null && double.TryParse(cmbFontSize.Text, out double size))
                rtb.Selection.ApplyPropertyValue(Inline.FontSizeProperty, size);
        }

        private StackPanel CreateTabHeader(string title)
        {
            var headerPanel = new StackPanel { Orientation = Orientation.Horizontal };
            headerPanel.Children.Add(new TextBlock { Text = title });
            var closeBtn = new Button
            {
                Content = "×",
                Width = 20,
                Height = 20,
                Margin = new Thickness(5, 0, 0, 0),
                Padding = new Thickness(0)
            };
            closeBtn.Click += CloseTab_Click;
            headerPanel.Children.Add(closeBtn);
            return headerPanel;
        }

        private void AddTab_Click(object sender, RoutedEventArgs e)
        {
            int count = tabControl.Items.Count + 1;
            var rtb = new RichTextBox();
            rtb.SelectionChanged += rtbEditor_SelectionChanged;
            var tab = new TabItem
            {
                Header = CreateTabHeader($"Документ {count}"),
                Content = rtb
            };
            tabControl.Items.Add(tab);
            tabControl.SelectedItem = tab;
        }

        private void CloseTab_Click(object sender, RoutedEventArgs e)
        {
            // Знаходимо TabItem, у якому натиснули кнопку
            foreach (TabItem tab in tabControl.Items.OfType<TabItem>().ToList())
            {
                if (tab.Header is StackPanel panel && panel.Children.OfType<Button>().FirstOrDefault() == sender)
                {
                    tabControl.Items.Remove(tab);
                    break;
                }
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var rtb = CurrentEditor;
            if (rtb != null)
                rtbEditor_SelectionChanged(rtb, null);
        }

        private void InsertImage_Click(object sender, RoutedEventArgs e)
        {
            var rtb = CurrentEditor;
            if (rtb == null) return;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";
            if (dlg.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(dlg.FileName));
                Image img = new Image { Source = bitmap, Width = 100 };
                var container = new InlineUIContainer(img, rtb.CaretPosition);
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, Open_Executed));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, Save_Executed));
        }
    }
}