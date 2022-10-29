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
    /// Логика взаимодействия для Buy.xaml
    /// </summary>
    public partial class Buy : Page
    {
        Book book;
        public Buy( )
        {
            InitializeComponent();
        }
        public Buy(Book book)
        {
            InitializeComponent();
            this.book = book;
            SetData();
        }
        private void SetData()
        {
            TextBlockName.Text = book.title;
            TextBlockGenre.Text = book.genre;
            TextBlockAuthor.Text = book.author.name;
            TextBlockAgeA.Text = book.author.age.ToString();
            TextBlockContinuation.Text = book.continuation;
            TextBlockDate.Text = book.date.Date.ToString();
            TextBlockDescriptionH.Text = book.publishingHouse.descriptions;
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

        }
    }
}
