using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }
        private void CheckBox_Changed(object sender, RoutedEventArgs e)
        {
            TextBoxPass.Visibility = System.Windows.Visibility.Collapsed;
            MyTextBox.Visibility = System.Windows.Visibility.Visible;

            MyTextBox.Focus();
            MyTextBox.Select(MyTextBox.Text.Length, 0);
        }

        private void revealModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBoxPass.Visibility = System.Windows.Visibility.Visible;
            MyTextBox.Visibility = System.Windows.Visibility.Collapsed;

            SetSelection(TextBoxPass, 2, 0);
            TextBoxPass.Focus();
        }

        private void TextBoxPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            MyTextBox.Text = TextBoxPass.Password.ToString();
            SetSelection(TextBoxPass, TextBoxPass.Password.Length, 0);
            TextBoxPass.Focus();
        }
        private void SetSelection(PasswordBox passwordBox, int start, int length)
        {
            passwordBox.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(passwordBox, new object[] { start, length });
        }
        private void MyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBoxPass.Password = MyTextBox.Text;
        }

    }
}
