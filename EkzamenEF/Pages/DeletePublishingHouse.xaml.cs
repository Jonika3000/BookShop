using EkzamenEF.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EkzamenEF.Pages
{
    /// <summary>
    /// Логика взаимодействия для DeletePublishingHouse.xaml
    /// </summary>
    public partial class DeletePublishingHouse : Page
    {
        public DeletePublishingHouse()
        {
            InitializeComponent();
            SetComboBoxItems();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CBBook.SelectedItem != null)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.publishingHouses.Load();
                    db.publishingHouses.Remove(
                        db.publishingHouses.FirstOrDefault(b => b.name == CBBook.SelectedItem.ToString()));
                    db.SaveChanges();
                    CBBook.SelectedItem = null;
                    SetComboBoxItems();
                }
            }
        }
        private void SetComboBoxItems()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var a = db.publishingHouses.Select(a => a.name).ToList();
                CBBook.ItemsSource = a;
            }
        }
    }
}
