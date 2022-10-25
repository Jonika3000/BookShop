using EkzamenEF.Models;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
        private bool CheckErrors()
        {
            string log = string.Empty;
            string pass = string.Empty;
            Application.Current.Dispatcher.Invoke(new Action(() => {
                log = TextBoxLogin.Text;
                pass = TextBoxPass.Password.ToString();
            }));
            if ( log == string.Empty)
            {
                StringError("Login empty");
                return false;
            }
            if ( pass == string.Empty)
            {
                StringError("Password empty");
                return false;
            }  
             return true;
        }
        private void StringError(string err)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                ErrorTextBox.Text = err;
            ErrorTextBox.Visibility = Visibility.Visible;
            }));
        }
        private void Search()
        {
            if (!CheckErrors())
                return;
            using (var db = new ApplicationContext())
            {
                var l = string.Empty;
                Application.Current.Dispatcher.Invoke(new Action(() => { l = TextBoxLogin.Text; }));
                var p = TextBoxPass.Password.ToString();
                var Acc = db.accounts.Where(c => c.login == l && c.password == p).FirstOrDefault();
                if (Acc == null)
                {
                    StringError("Incorrect data");
                    return;
                }
                StringError("  data");
            }
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            var t = new Task(Search);
            t.Start();
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Container.Navigate(new Register());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Container.Navigate(new ForgotPassword());
        }
    }
}
