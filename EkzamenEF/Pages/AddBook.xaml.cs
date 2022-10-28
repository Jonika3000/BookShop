using EkzamenEF.Models;
using Microsoft.EntityFrameworkCore;
using Ookii.Dialogs.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddBook.xaml
    /// </summary>
    public partial class AddBook : Page
    {
        public AddBook()
        {
            InitializeComponent();
            using(ApplicationContext db = new ApplicationContext())
            {
                db.publishingHouses.Load();
                db.authors.Load();
                var a = db.authors.Select(a => a.name).ToList(); 
                var p = db.publishingHouses.Select(a => a.name).ToList();
                CBAuthor.ItemsSource  = a;
                CBHouse.ItemsSource = p;
            }
        }

        private void ButtonSelectImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaOpenFileDialog { Filter = @"JPEG Files|*.jpg;*.jpeg;*.jpe" };
            var showDialog = dialog.ShowDialog(Application.Current.MainWindow);
            if (showDialog != null && (bool)showDialog)
            {
                ImageBook.Source = new BitmapImage(new Uri(dialog.FileName, UriKind.RelativeOrAbsolute));
            }

        }
        private void Clear()
        {
            TextBoxContinuation.Text = string.Empty;
            TextBoxCount.Text = string.Empty;
            TextBoxGenre.Text = string.Empty;
            TextBoxName.Text = string.Empty;
            TextBoxPageCount.Text = string.Empty;
            TextBoxPrice.Text = string.Empty;
            TextBoxPriceSell.Text = string.Empty;
            CBAuthor.SelectedItem = null;
            CBHouse.SelectedItem = null;
            DPDate.SelectedDate = null;
            ImageBook.Source = null;
            ErrorTextBox.Text = string.Empty;

        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!CheackErrors())
                return;
            try
            {
                using (ApplicationContext db =  new ApplicationContext())
                {
                    var book = new Book();
                    string nameAuthor = CBAuthor.SelectedItem.ToString();
                    book.author = db.authors.FirstOrDefault(a => a.name == nameAuthor);
                    book.date = Convert.ToDateTime(DPDate.SelectedDate);
                    book.price = Convert.ToDouble(TextBoxPrice.Text);
                    book.photo = EkzamenEF.Helpers.ImageConverter.getJPGFromImageControl(ImageBook.Source as BitmapImage);
                    book.title = TextBoxName.Text;
                    book.continuation = TextBoxContinuation.Text;
                    book.count = Convert.ToInt32(TextBoxCount.Text);
                    book.priceForSale = Convert.ToDouble(TextBoxPriceSell.Text);
                    string nameHouse = CBHouse.SelectedItem.ToString();
                    book.publishingHouse = db.publishingHouses.Where(a => a.name == nameHouse).FirstOrDefault();
                    book.genre = TextBoxGenre.Text;
                    book.pagesCount = Convert.ToInt32(TextBoxPageCount.Text);
                    db.books.Add(book); 
                    db.authors.Load();
                    db.authors.FirstOrDefault(a => a.name == nameAuthor).books.Add(book);
                    db.publishingHouses.FirstOrDefault(a => a.name == nameHouse).books.Add(book);
                    db.SaveChanges();
                    Clear();
                }
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private bool CheackErrors()
        {
            if(TextBoxContinuation.Text == string.Empty)
            {
                TextBoxContinuation.Text = "none";
            }
            if(TextBoxCount.Text == string.Empty)
            {
                StringError("Count is empty");
                return false;
            }
            if(TextBoxGenre.Text == string.Empty)
            {
                StringError("Genre is empty");
                return false;
            }
            if(TextBoxName.Text == string.Empty)
            {
                StringError("Name is empty");
                return false;
            }
            if(TextBoxPageCount.Text == string.Empty)
            {
                StringError("Page count is empty");
                return false;
            }
            if(TextBoxPrice.Text == string.Empty)
            {
                StringError("Genre is empty");
                return false;
            }
            if(TextBoxPriceSell.Text == string.Empty)
            {
                StringError("Price sell is empty");
                return false;
            }
            if(CBHouse.SelectedItem == null)
            {
                StringError("Publishing House is not selected");
                return false;
            }
            if(DPDate.SelectedDate == null)
            {
                StringError("Date is not selected");
                return false;
            }
            if (CBAuthor.SelectedItem == null)
            {
                StringError("Author is not selected");
                return false;
            }
            if (ImageBook.Source == null)
            {
                StringError("Image not selected");
                return false;
            }
             return true;
        }
        private void StringError(string err)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                ErrorTextBox.Text = err;
                ErrorTextBox.Visibility = Visibility.Visible;
            }));
        }
    }
}
