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
using Microsoft.EntityFrameworkCore;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Buy.xaml
    /// </summary>
    public partial class Buy : Page
    {
        Book book;
        Account account;
        public Buy( )
        {
            InitializeComponent();
        }
        public Buy(Book book,Account account)
        {
            InitializeComponent();
            this.book = book;
            this.account = account;
            SetData();
        }
        private void SetData()
        { 
            using (ApplicationContext db = new ApplicationContext())
            {
                db.books.Load(); 
                book = db.books.Include("author").Include("publishingHouse").Where(q => q.id == book.id).FirstOrDefault();
            }
            TextBlockName.Text = book.title;
            TextBlockGenre.Text = book.genre;
            TextBlockAuthor.Text = book.author.name;
            TextBlockAgeA.Text = book.author.age.ToString()+" age";
            TextBlockContinuation.Text = book.continuation;
            TextBlockDate.Text = $"{book.date.Day}.{book.date.Month}.{book.date.Year}";
            TextBlockDescriptionH.Text = book.publishingHouse.descriptions;
            PublishingHouseTextBlock.Text = book.publishingHouse.name;
            TextBlockGenre.Text = book.genre;
            TextBlockNameA.Text = book.author.name;
            TextBlockNameH.Text = book.publishingHouse.name;
            TextBlockPages.Text = book.pagesCount.ToString();
            TextBlockPrice.Text = book.priceForSale.ToString();

            ImageAuthor.Source = ImageConverter.ConvertByteArrayToBitmapImage(book.author.avatar).Source;
            ImageBook.Source = ImageConverter.ConvertByteArrayToBitmapImage(book.photo).Source;
            ImageHouse.Source = ImageConverter.ConvertByteArrayToBitmapImage(book.publishingHouse.photo).Source;
        }

        private void ButtonBuy_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Container.Navigate(new Purchase(account,book ));
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (account.admin)
            {
                ((MainWindow)Application.Current.MainWindow).Container.Navigate(new Admin(account));
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).Container.Navigate(new NewsFeed(account));
            }
        }
    }
}
