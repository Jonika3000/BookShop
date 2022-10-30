using EkzamenEF.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using EkzamenEF.Helpers;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewsFeed.xaml
    /// </summary>
    public partial class NewsFeed : Page
    {
        List<string> genres = new List<string>();
        Account account;
        public NewsFeed(Account account)
        {
            InitializeComponent();
            LoadGenre();
            SetStackGenre();
            this.account = account;
            SetAvatar();
            Container.Navigate(new AllBooks(account));
        }
        private void SetAvatar()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.accounts.Load();
                this.account = db.accounts.FirstOrDefault(a => a.id == account.id);
                db.SaveChanges();
            }
            if (account.avatar == null)
            {
                ImageAvatar.Source = new BitmapImage(new System.Uri("/Resources/icons8_account_144px.png"
                    , System.UriKind.RelativeOrAbsolute));
            }
            else
            {
                ImageAvatar.Source = ImageConverter.ConvertByteArrayToBitmapImage(account.avatar).Source;
            }
        }
        private void LoadGenre()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.books.Load();
                genres = db.books.Select(b => b.genre).ToList();
            }
        }
        private void SetStackGenre()
        {
            foreach(var genre in genres)
            {
                var b = new Button();
                b.Click += B_Click;
                b.Style = Resources["GenreButton"] as Style;
                b.Content = genre;
                StackPanelGenre.Children.Add(b);
            }
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            Container.Navigate(new AllBooks(b.Content.ToString(),true,account));
        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            Container.Navigate(new AllBooks(account));
            SearchTermTextBox.Text = string.Empty;
            SetAvatar();
            StackPanelGenre.Visibility = Visibility.Visible;
            BorderGenre.Visibility = Visibility.Visible;

        }

        private void SearchTermTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Container.Navigate(new AllBooks(SearchTermTextBox.Text,false, account));
                SetAvatar();
                StackPanelGenre.Visibility = Visibility.Visible;
                BorderGenre.Visibility = Visibility.Visible;
            }

        }

        private void ButtonAvatar_Click(object sender, RoutedEventArgs e)
        {
            Container.Navigate(new ProfileUser(account));
            StackPanelGenre.Visibility = Visibility.Hidden;
            BorderGenre.Visibility = Visibility.Hidden;
        }

        private void SearchTermTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(SearchTermTextBox.Text == string.Empty)
            {
                Container.Navigate(new AllBooks(account));
            }
        }
    }
}
