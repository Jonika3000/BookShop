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
using EkzamenEF.Models;
using EkzamenEF.Helpers;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfileUser.xaml
    /// </summary>
    public partial class ProfileUser : Page
    {
        Account account;
        public ProfileUser(Account account)
        {
            InitializeComponent();
            this.account = account;
            SetData();
        }
        private void SetData()
        {
            ImageAvatar.Source = ImageConverter.ConvertByteArrayToBitmapImage(account.avatar).Source;
            TextBlockLogin.Text = account.login;
            TextBlockEmail.Text = account.email;
        }

        private void ButtonAvatar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonChangePass_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
