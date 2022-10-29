using EkzamenEF.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        Account account;
        public Admin(Account account)
        {
            InitializeComponent();
            this.account = account;
        }
         

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAuthor_Click(object sender, RoutedEventArgs e)
        {
            Container.Navigate(new AddAuthor());
        }

        private void ButtonAddBook_Click(object sender, RoutedEventArgs e)
        {
            Container.Navigate(new AddBook());
        }

        private void AddHouse_Click(object sender, RoutedEventArgs e)
        {
            Container.Navigate(new AddPublishingHouses());
        }

        private void AddAdmin_Click(object sender, RoutedEventArgs e)
        {
            Container.Navigate(new AddAdmin());
        }

        private void ButtonDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            Container.Navigate(new DeleteBook());
        }

        private void ButtonEditBook_Click(object sender, RoutedEventArgs e)
        {
            Container.Navigate(new EditBook());
        }

        private void DeletePublishingHouse_Click(object sender, RoutedEventArgs e)
        {
            Container.Navigate(new DeletePublishingHouse());
        }

        private void DeleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            Container.Navigate(new DeleteAuthor());
        }

        private void ButtonAvatar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
