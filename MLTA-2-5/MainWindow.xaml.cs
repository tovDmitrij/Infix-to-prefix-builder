using System.Windows;
using System.Windows.Controls;
namespace MLTA_2_5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Нажатие кнопок на панели
        /// </summary>
        private void ButtonPress(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Content)
            {
                case "Очистить":
                    textBoxInfix.Text = "";
                    return;
                case "BackSpace":
                    try
                    {
                        textBoxInfix.Text = textBoxInfix.Text.Remove(textBoxInfix.Text.Length - 1);
                        return;
                    }
                    catch
                    {
                        return;
                    }
                default:
                    textBoxInfix.Text += ((Button)sender).Content;
                    return;
            }
        }
        /// <summary>
        /// Подтверждение
        /// </summary>
        private void Accept(object sender, RoutedEventArgs e)
        {
            textBoxPrefix.Text = new InfixToPrefix(textBoxInfix.Text).Run();
        }
    }
}