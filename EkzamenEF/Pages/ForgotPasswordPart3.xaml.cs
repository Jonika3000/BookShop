using EkzamenEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для ForgotPasswordPart3.xaml
    /// </summary>
    public partial class ForgotPasswordPart3 : Page
    {
        string login = string.Empty;
        public ForgotPasswordPart3(string login)
        {
            InitializeComponent();
            this.login = login;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxCode.Text == string.Empty)
            {
                StringError("Password is empty");
                return;
            }
            if (TextBoxCode.Text.Length < 6)
            {
                StringError("Password is too small (min 3 chars)");
                return;
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                db.accounts.FirstOrDefault(a => a.login == login).password = TextBoxCode.Text;
                db.SaveChanges();
                ((MainWindow)Application.Current.MainWindow).Container.Navigate(new Login());
            }
        }
        private void StringError(string err)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                ErrorTextBox.Text = err;
                ErrorTextBox.Visibility = Visibility.Visible;
            }));
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Container.Navigate(new Login());
        }
    }
}
