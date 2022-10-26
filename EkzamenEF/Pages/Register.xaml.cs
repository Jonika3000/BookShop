using EkzamenEF.Models;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls; 

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Register()
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

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckErrors())
                return;
            var a = new Account();
            a.email = TextBoxEmail.Text;
            a.password = TextBoxPass.Password.ToString();
            a.login = TextBoxLogin.Text;
            a.admin = false;
            using (var db = new ApplicationContext())
            {
                var tmp = db.accounts.Where(q => q.login == TextBoxLogin.Text).FirstOrDefault();
                if (tmp == null)
                {
                    db.accounts.Add(a);
                    db.SaveChanges();
                    ((MainWindow)Application.Current.MainWindow).Container.Navigate(new Login());
                }
                else
                    StringError("An account with the same name already exists");
            }

        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Container.Navigate(new Login());
        }
        private bool CheckErrors()
        {
            if (TextBoxLogin.Text == string.Empty)
            {
                StringError("Login empty");
                return false;
            }
            if (TextBoxPass.Password.ToString() == string.Empty)
            {
                StringError("Password empty");
                return false;
            }
            if (TextBoxEmail.Text == string.Empty)
            {
                StringError("Email empty");
                return false;
            }
            if (TextBoxLogin.Text.Length < 3)
            {
                StringError("Login is too small (min 3 chars)");
                return false;
            }
            if (TextBoxPass.Password.ToString().Length < 6)
            {
                StringError("Password is too small (min 3 chars)");
                return false;
            }
            var email = TextBoxEmail.Text;
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(email);
            if (!match.Success)
            {
                StringError("Email filled out incorrectly");
                return false;
            }
            return true;
        }
        private void StringError(string err)
        {
            ErrorTextBox.Text = err;
            ErrorTextBox.Visibility = Visibility.Visible;
        }


    }
}
