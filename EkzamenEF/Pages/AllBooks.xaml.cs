using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EkzamenEF.Models;
using EkzamenEF.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AllBooks.xaml
    /// </summary>
    public partial class AllBooks : Page
    {
        string search;
        List<Book> books;
        Account account;    
        public AllBooks(Account account)
        {
            InitializeComponent();
            AllSet();
            this.account = account; 
        }
        public AllBooks(string search, bool isGenre, Account account)
        {
            InitializeComponent();
            this.search = search;
            this.account = account;
            if (isGenre)
            {
                GenreSet();
            }
            else
            {
                NameSet();
            }
        }
        private void AllSet()
        {
            WrapPanelBooks.Children.Clear();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.books.Load();
                foreach (var b in db.books)
                {
                    WrapPanelBooks.Children.Add(CreateBook(b));
                }
            }
        }
        private void NameSet()
        {
            WrapPanelBooks.Children.Clear();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.books.Load();
                books = db.books.Where(b => b.title.Contains(search)).ToList();
            }
            if(books.Count > 0)
            {
                foreach (var b in books)
                {
                    WrapPanelBooks.Children.Add(CreateBook(b));
                }
            }
           else
            {
                MessageBox.Show("Nothing found","Info",MessageBoxButton.OK,MessageBoxImage.Information);
                AllSet();
            }
        }
        private void GenreSet()
        {
            WrapPanelBooks.Children.Clear();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.books.Load();
                books = db.books.Where(b => b.genre == search).ToList();
            }
            foreach(var b in books)
            {
                WrapPanelBooks.Children.Add(CreateBook(b));
            }
        }
        private Button CreateBook(Book book )
        {
            var b = new Button();
            b.Style = Resources["BookButton"] as Style;
            b.Click += B_Click;
            b.Tag = book.id;
            var stack = new StackPanel();
            var img = new Image();
            var text = new TextBlock();

            img.Source = ImageConverter.ConvertByteArrayToBitmapImage(book.photo).Source;
            img.Height = 150;
            img.Stretch = System.Windows.Media.Stretch.Uniform;

            text.FontSize = 15;
            text.FontWeight = FontWeights.Regular;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.Text = book.title;

            stack.Children.Add(img);
            stack.Children.Add(text);
            b.Content = stack;

            return b;
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            Book book;
            using (ApplicationContext db = new ApplicationContext())
            {
                db.books.Load();
                  book = db.books.FirstOrDefault(q => q.id.ToString() == b.Tag.ToString());
            }

            ((MainWindow)Application.Current.MainWindow).Container.Navigate(new Buy(book, account));
        }
    }
}
