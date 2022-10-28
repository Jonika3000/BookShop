using EkzamenEF.Models;
using Ookii.Dialogs.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddAuthor.xaml
    /// </summary>
    public partial class AddAuthor : Page
    {
        public AddAuthor()
        {
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckErrors())
                return;
            Author author = new Author();
            author.avatar = EkzamenEF.Helpers.ImageConverter.getJPGFromImageControl(ImageBook.Source as BitmapImage);
            try
            {
                author.age = Convert.ToInt32(TextBoxAge.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            author.name = TextBoxName.Text;
            using(ApplicationContext db = new ApplicationContext())
            {
                db.authors.Add(author);
                db.SaveChanges();
            }
        }
        private bool CheckErrors()
        {
            if (TextBoxName.Text == string.Empty)
            {
                StringError("Name is empty");
                return false;
            }
            if(TextBoxAge.Text == string.Empty)
            {
                StringError("Age is empty");
                return false;
            }
            if(ImageBook.Source == null)
            {
                StringError("Image not selected");
                return false;
            }
            return true;
        }
        private void Clear()
        {
            TextBoxName.Text = string.Empty;    
            TextBoxAge.Text = string.Empty;
            ImageBook.Source = null;
            ErrorTextBox.Text = string.Empty;
        }
        private void StringError(string err)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                ErrorTextBox.Text = err;
                ErrorTextBox.Visibility = Visibility.Visible;
            }));
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

       
    }
}
