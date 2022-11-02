using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using EkzamenEF.Models;
using EkzamenEF.Helpers;
using Ookii.Dialogs.Wpf;
using Microsoft.EntityFrameworkCore;

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
            using (ApplicationContext db = new ApplicationContext())
            {
                db.accounts.Load();
                this.account = db.accounts.FirstOrDefault(a=>a.id == account.id);
                db.SaveChanges();
            }
            
            SetData();
        }
        private void SetData()
        {
            if(account.avatar == null || account.avatar.Length == 0)
            {
                ImageAvatar.Source = new BitmapImage(new System.Uri("/Resources/icons8_account_144px.png"
                    , System.UriKind.RelativeOrAbsolute));
            }
           else
            {
                ImageAvatar.Source = ImageConverter.ConvertByteArrayToBitmapImage(account.avatar).Source;
            }
            TextBlockLogin.Text = account.login;
            TextBlockEmail.Text = account.email;
        }

        private void ButtonAvatar_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaOpenFileDialog { Filter = @"JPEG Files|*.jpg;*.jpeg;*.jpe" };
            var showDialog = dialog.ShowDialog(Application.Current.MainWindow);
            if (showDialog != null && (bool)showDialog)
            {
                ImageAvatar.Source = new BitmapImage(new Uri(dialog.FileName, UriKind.RelativeOrAbsolute));
                using(ApplicationContext db =new ApplicationContext())
                {
                    db.accounts.Load();
                    db.accounts.FirstOrDefault(a => a.id == account.id).avatar = ImageConverter.getJPGFromImageControl(ImageAvatar.Source as BitmapImage);
                    db.SaveChanges();
                }
            }
        }

        private void ButtonChangePass_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Container.Navigate(new ForgotPassword());
        }

        private void ButtonSignOut_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Container.Navigate(new Login());
        }
    }
}
