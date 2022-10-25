using EkzamenEF.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Page
    {
        int code;
        string email = string.Empty;
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void ButtonReestablish_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxLogin.Text == String.Empty)
            {
                StringError("Login empty");
                return;
            }
            if (AccountExist())
            {
                SendMail();
                ((MainWindow)Application.Current.MainWindow).Container.Navigate(new ForgotPasswordPart2(code,TextBoxLogin.Text));
            }
        }
        private void SendMail()
        {
            Random random = new Random();
              code = random.Next(10000, 100000);

            MailAddress from = new MailAddress("fremidisk@gmail.com", "MinuBook Password Reestablish");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Enter this code to the right field";
            m.Body = $"<h2>{code}</h2>";
            m.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("mail.gmx.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("edu.minu@yandex.ru", "Qwerty1337Q");
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
             smtp.Send(m);
        }
        private bool AccountExist()
        {
            using (var db = new ApplicationContext())
            {
                var tmp = db.accounts.FirstOrDefault(a => a.login == TextBoxLogin.Text);
                if (tmp == null)
                    return false;
                else
                {
                    email = tmp.email;
                    return true;

                }
            }
            return false;
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
