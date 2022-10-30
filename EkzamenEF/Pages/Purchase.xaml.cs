using EkzamenEF.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Purchase.xaml
    /// </summary>
    public partial class Purchase : Page
    {
        Account account;
        Book book;
        public Purchase(Account account, Book book)
        {
            InitializeComponent();
            this.account = account;
            this.book = book;
            TextBlockPrice.Text = $"Total: {book.price}";
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Container.Navigate(new NewsFeed(account));
        }

        private void ButtonBuy_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (db.books.FirstOrDefault(b => b.id == book.id).count > 0)
                {
                    db.books.Load();
                    db.books.FirstOrDefault(b => b.id == book.id).count--;

                    ((MainWindow)Application.Current.MainWindow).Container.Navigate(new NewsFeed(account));
                }
                else
                {
                    MessageBox.Show("Not available", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ((MainWindow)Application.Current.MainWindow).Container.Navigate(new NewsFeed(account));
                }
              
            }
        }
    }
}
