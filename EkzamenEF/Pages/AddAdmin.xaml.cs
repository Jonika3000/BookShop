using EkzamenEF.Models;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddAdmin.xaml
    /// </summary>
    public partial class AddAdmin : Page
    {
        public AddAdmin()
        {
            InitializeComponent();
        }
        private void StringError(string err)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                ErrorTextBox.Text = err;
                ErrorTextBox.Visibility = Visibility.Visible;
            }));
        }
        private void Clear()
        {
            TextBoxEmail.Text = string.Empty;
            TextBoxLogin.Text = string.Empty;
            TextBoxPassword.Text = string.Empty;
        }
        private bool CheckErrors()
        {
            if (TextBoxLogin.Text == string.Empty)
            {
                StringError("Login empty");
                return false;
            }
            if (TextBoxPassword.Text == string.Empty)
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
            if (TextBoxPassword.Text.Length < 6)
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
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckErrors())
                return;
            using (ApplicationContext db = new ApplicationContext())
            {
                var account = new Account();
                account.login = TextBoxLogin.Text;
                account.password = TextBoxPassword.Text;
                account.admin = true;
                account.email = TextBoxEmail.Text;
                var bm = new BitmapImage();
                bm.UriSource = new Uri("/Resources/icons8_account_144px.png", UriKind.RelativeOrAbsolute);
                account.avatar = EkzamenEF.Helpers.ImageConverter.getJPGFromImageControl(bm);
                db.accounts.Add(account);
                db.SaveChanges();
                Clear();
            }
        }
    }
}
