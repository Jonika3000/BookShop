using EkzamenEF.Models;
using Ookii.Dialogs.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPublishingHouses.xaml
    /// </summary>
    public partial class AddPublishingHouses : Page
    {
        public AddPublishingHouses()
        {
            InitializeComponent();
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
        private void StringError(string err)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                ErrorTextBox.Text = err;
                ErrorTextBox.Visibility = Visibility.Visible;
            }));
        }
        private bool CheckErrors()
        {
            if(TextBoxName.Text == string.Empty)
            {
                StringError("Name is empty");
                return false;
            }
            if(TextBoxDescription.Text == string.Empty)
            {
                StringError("Description is empty");
                return false;
            }
            if(ImageBook.Source == null)
            {
                StringError("Image is empty");
                return false;
            }
            return true;

        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckErrors())
                return;
            using (ApplicationContext db = new ApplicationContext())
            {
                PublishingHouse house = new PublishingHouse();  
                house.photo = EkzamenEF.Helpers.ImageConverter.getJPGFromImageControl(ImageBook.Source as BitmapImage);
                house.descriptions = TextBoxDescription.Text;
                house.name = TextBoxName.Text;
                db.publishingHouses.Add(house);
                db.SaveChanges();
            }
            Clear();
        }
        private void Clear()
        {
            TextBoxName.Text = string.Empty;
            TextBoxDescription.Text = string.Empty;
            ImageBook.Source = null;
            ErrorTextBox.Text = string.Empty;

        }
    }
}
