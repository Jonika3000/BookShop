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
    /// Логика взаимодействия для ForgotPasswordPart2.xaml
    /// </summary>
    public partial class ForgotPasswordPart2 : Page
    {
        int code;
        string login;
        public ForgotPasswordPart2(int code, string login)
        {
            InitializeComponent();
            this.code = code;
            this.login = login;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Container.Navigate(new Login());
        }

        private void ButtonCheck_Click(object sender, RoutedEventArgs e)
        {
            if(TextBoxCode.Text == string.Empty)
            {
                StringError("Code is empty");
                return;
            }
            if(TextBoxCode.Text != code.ToString())
            {
                StringError("Code is wrong");
                return;
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).Container.Navigate(new ForgotPasswordPart3(login));
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
    }
}
